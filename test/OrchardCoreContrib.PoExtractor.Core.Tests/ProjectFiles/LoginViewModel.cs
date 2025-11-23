using System.ComponentModel.DataAnnotations;

namespace OrchardCoreContrib.PoExtractor.Tests.Files;

public class LoginViewModel
{
    [Translatable]
    const string ConstantMessage = "The password is required.";

    [Required(ErrorMessage = "The username is required.")]
    public string UserName { get; set; }

    [Required(ErrorMessage = ConstantMessage)]
    public string Password { get; set; }
}

public class TranslatableAttribute : Attribute
{
}