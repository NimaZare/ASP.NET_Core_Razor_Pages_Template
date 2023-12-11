using System.ComponentModel.DataAnnotations;

namespace Domain.Seedwork;

public static class ValidationHelper
{
	public static bool IsValid(object entity)
	{
		var validationContext = new ValidationContext(instance: entity);
		var validationResults = new List<ValidationResult>();
		var isValid = Validator.TryValidateObject(instance: entity, validationContext: validationContext,
			validationResults: validationResults, validateAllProperties: true);

		return isValid;
	}

	public static IList<ValidationResult> GetValidationResults(object entity)
	{
		var validationContext = new ValidationContext(instance: entity);
		var validationResults = new List<ValidationResult>();

		Validator.TryValidateObject(instance: entity, validationContext: validationContext,
			validationResults: validationResults, validateAllProperties: true);

		return validationResults;
	}
}
