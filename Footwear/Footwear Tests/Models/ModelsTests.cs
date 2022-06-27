

namespace Footwear_Tests.Models
{
    using Footwear.Data.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Xunit;

    public class ModelsTests
    {
        //Unit Test DataAnnotations method
        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void Test_Correct_Address_Model_Validations()
        {
            var correctAddress = new Address
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Test street 22B",
                Country = "Bulgaria",
                ZipCode = "1000"
            };

            var errors = ValidateModel(correctAddress);
            Assert.True(errors.Count == 0);
        }
    }
}
