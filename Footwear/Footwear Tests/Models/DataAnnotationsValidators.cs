namespace Footwear_Tests.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    internal class DataAnnotationsValidators
    {
        /// <summary>
        // Validates the model object which uses validation attributes.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        internal static IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}
