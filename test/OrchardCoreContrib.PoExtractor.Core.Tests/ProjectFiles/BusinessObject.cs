namespace OrchardCoreContrib.PoExtractor.Tests.Files;

public class BusinessObject
{
    public void SomeOperation()
    {
        Validation.IsTrue(false, "The condition must be true.");
        Validation.Throw("An error occurred in some operation.");
    }
}