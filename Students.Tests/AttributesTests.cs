using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Students.Common.Attributes;
using Xunit;

namespace Students.Tests;

public class AttributesTests
{

    [Theory]
    [InlineData("Jan Kowalski")]
    [InlineData("Szymon Michalek")]
    public void IsValid_ValidInputName_ReturnsSuccess(string inputValue)
    {
        // Arrange
        var validator = new NameSurname(); 
        var validationContext = new ValidationContext(new {Value = inputValue });

        // Act
        var result = validator.GetValidationResult(inputValue, validationContext);

        // Assert
        Assert.Equal(ValidationResult.Success, result);
    }

    [Theory]
    [InlineData("jankowalski")] 
    [InlineData("Szymon231")]
    public void IsValid_InvalidInputName_ReturnsErrorMessage(string inputValue)
    {
        // Arrange
        var validator = new NameSurname();
        var validationContext = new ValidationContext(new { Value = inputValue });

        // Act
        var result = validator.GetValidationResult(inputValue, validationContext);

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(ValidationResult.Success, result);
    }

    [Theory]
    [InlineData("12-345")] 
    [InlineData("11-111")]
    public void IsValid_ValidInputPostalCode_ReturnsSuccess(string inputValue)
    {
        // Arrange
        var validator = new ValidPostalCode();
        var validationContext = new ValidationContext(new { Value = inputValue });

        // Act
        var result = validator.GetValidationResult(inputValue, validationContext);

        // Assert         
        Assert.Equal(ValidationResult.Success, result);
    }

    [Theory]
    [InlineData("12345")]
    [InlineData("111-111213")]
    public void IsNotValid_ValidInputPostalCode_ReturnsErrorMessage(string notValidinputValue)
    {
        // Arrange
        var validator = new ValidPostalCode();
        var validationContext = new ValidationContext(new { Value = notValidinputValue });

        // Act
        var result = validator.GetValidationResult(notValidinputValue, validationContext);

        // Assert         
        Assert.NotEqual(ValidationResult.Success, result);
    }



}
