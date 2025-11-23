using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;

namespace OrchardCoreContrib.PoExtractor.DotNet;

/// <summary>
/// Extracts localizable string from fields marked with [Translatable] Description property.
/// </summary>
public class TranslatableAttributeStringExtractor(IMetadataProvider<SyntaxNode> metadataProvider)
    : LocalizableStringExtractor<SyntaxNode>(metadataProvider)
{
    public override bool TryExtract(SyntaxNode node, out LocalizableStringOccurence result)
    {
        if (node is AttributeSyntax attr
            && attr.Name.ToString() == "Translatable")
        {
            // get the parent field declaration
            if (attr.Parent?.Parent is FieldDeclarationSyntax fieldDeclaration)
            {
                foreach (var variable in fieldDeclaration.Declaration.Variables)
                {
                    if (variable.Initializer.Value is LiteralExpressionSyntax literal)
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
