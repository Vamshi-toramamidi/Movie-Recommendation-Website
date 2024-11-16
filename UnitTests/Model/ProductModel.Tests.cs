using ContosoCrafts.WebSite.Models;
namespace UnitTests.Model;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

[TestFixture]
public class ProductModelTests
{
    [Test]
    public void Should_Have_Title_With_Length_Between_1_And_33()
    {
        // Arrange
        var product = new ProductModel { Title = "Pulp Fiction" };

        // Act
        var validationResults = ValidateModel(product);

        // Assert
        Assert.That(validationResults, Is.Empty);
    }

    [Test]
    public void Title_Should_Fail_Validation_When_Length_Is_Too_Short()
    {
        // Arrange
        var product = new ProductModel { Title = "" };

        // Act
        var validationResults = ValidateModel(product);

        // Assert
        Assert.That(validationResults, Has.Count.EqualTo(1));
        Assert.That(validationResults[0].ErrorMessage, Is.EqualTo("The Title should have a length of more than 1 and less than 33"));
    }

    [Test]
    public void Title_Should_Fail_Validation_When_Length_Exceeds_Maximum()
    {
        // Arrange
        var product = new ProductModel { Title = new string('A', 34) };

        // Act
        var validationResults = ValidateModel(product);

        // Assert
        Assert.That(validationResults, Has.Count.EqualTo(1));
        Assert.That(validationResults[0].ErrorMessage, Is.EqualTo("The Title should have a length of more than 1 and less than 33"));
    }

    // This helper method validates a ProductModel object based on its data annotations
    // It checks all properties of the model, including nested objects, and returns a list of validation errors (if any)
    private static List<ValidationResult> ValidateModel(ProductModel model)
    {
        var context = new ValidationContext(model, null, null);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(model, context, results, true);
        return results;
    }
}