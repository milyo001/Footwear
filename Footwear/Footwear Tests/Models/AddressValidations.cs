
namespace Footwear_Tests.Models
{
    using Footwear.Data.Models;
    using Xunit;
    
    public class AddressValidations
    {
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

            var errors = DataAnnotationsValidators.ValidateModel(correctAddress);
            Assert.True(errors.Count == 0);
        }

        [Fact]
        public void Test_State_Validator_Address_Model()
        {
            var correctAddress = new Address
            {
                Id = 1,
                State = null,
                City = "Sofia",
                Street = "Test street 22B",
                Country = "Bulgaria",
                ZipCode = "1000"
            };

            var errors = DataAnnotationsValidators.ValidateModel(correctAddress);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_City_Validator_Address_Model()
        {
            var correctAddress = new Address
            {
                Id = 1,
                State = "Sofia",
                City = null,
                Street = "Test street 22B",
                Country = "Bulgaria",
                ZipCode = "1000"
            };

            var errors = DataAnnotationsValidators.ValidateModel(correctAddress);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_Street_Validator_Address_Model()
        {
            var correctAddress = new Address
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = null,
                Country = "Bulgaria",
                ZipCode = "1000"
            };

            var errors = DataAnnotationsValidators.ValidateModel(correctAddress);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_Country_Validator_Address_Model()
        {
            var correctAddress = new Address
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Salmon Sushi Street 22",
                Country = null,
                ZipCode = "1000"
            };

            var errors = DataAnnotationsValidators.ValidateModel(correctAddress);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_ZipCode_Validator_Address_Model()
        {
            var correctAddress = new Address
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Salmon Sushi Street 22",
                Country = "Bulgaria",
                ZipCode = null
            };

            var errors = DataAnnotationsValidators.ValidateModel(correctAddress);
            Assert.True(errors.Count == 1);
        }

        // App data tests
        [Fact]
        public void Test_With_Correct_Data_AppData_Model()
        {
            var appData = new AppData
            {
                DeliveryPrice = 2.5m,
                MaxDelivery = 2,
                MinDelivery = 1
            };

            var errors = DataAnnotationsValidators.ValidateModel(appData);
            Assert.True(errors.Count == 0);
        }
    }
}
