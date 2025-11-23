using OrchardCoreContrib.PoExtractor.DotNet;
using OrchardCoreContrib.PoExtractor.Tests.Fakes;
using System.Linq;

namespace OrchardCoreContrib.PoExtractor.Tests;

public class TranslatableMethodArgumentTests
{
    private readonly FakeCSharpProjectProcessor _fakeCSharpProjectProcessor = new();

    [Fact]
    public void ExtractLocalizedStringsMethodArgumentsMarkedWithTranslatableAttribute()
    {
        // Arrange
        var localizableStringCollection = new LocalizableStringCollection();
        MethodInvokerExtractor.MethodArgumentPositions["Validation.Throw"] = 0;
        MethodInvokerExtractor.MethodArgumentPositions["Validation.IsTrue"] = 1;

        // Act
        _fakeCSharpProjectProcessor.Process(string.Empty, string.Empty, localizableStringCollection);

        // Assert
        var localizedStrings = localizableStringCollection.Values
            .Select(s => s.Text)
            .ToList();

        Assert.NotEmpty(localizedStrings);
        Assert.Equal(9, localizedStrings.Count);
        Assert.Contains(localizedStrings, s => s == "The condition must be true.");
        Assert.Contains(localizedStrings, s => s == "An error occurred in some operation.");
        
    }
}
