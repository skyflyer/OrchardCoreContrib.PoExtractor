using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;

namespace OrchardCoreContrib.PoExtractor.DotNet;

/// <summary>
/// Extracts localizable string from method invocation
/// </summary>
public class MethodInvokerExtractor(IMetadataProvider<SyntaxNode> metadataProvider)
    : LocalizableStringExtractor<SyntaxNode>(metadataProvider)
{
    const string MethodParameterName = "formatMessage";

    public static Dictionary<string, int> MethodArgumentPositions = new() { };

    public override bool TryExtract(SyntaxNode node, out LocalizableStringOccurence result)
    {
        if (node is InvocationExpressionSyntax invocationExpressionSyntax)
        {
            var expressionString = invocationExpressionSyntax.Expression.ToString();
            if (MethodArgumentPositions.ContainsKey(expressionString))
            {
                var argumentList = invocationExpressionSyntax.ArgumentList;
                if (argumentList.Arguments.Count > MethodArgumentPositions[expressionString])
                {
                    var argument = argumentList.Arguments[MethodArgumentPositions[expressionString]];
                    if (argument.Expression is LiteralExpressionSyntax literal && literal.IsKind(Microsoft.CodeAnalysis.CSharp.SyntaxKind.StringLiteralExpression))
                    {
                        result = CreateLocalizedString(literal.Token.ValueText, null, node);
                        return true;
                    }
                }
            }
        }
        result = null;
        return false;
    }
}
