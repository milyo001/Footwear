namespace Footwear_Tests.Models
{
    using Footwear.Data.Models;
    using Xunit;

    public class BillingInformationValidators
    {
        [Fact]
        public void Test_With_Correct_Data_BillingInfo_Model()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Salmon Sushi Street 22",
                Country = "Bolivia",
                ZipCode = "1000",
                FirstName = "Miro",
                LastName = "Ilyovski",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 0);
        }


        [Fact]
        public void Test_BillingInfo_Model_When_State_IsNull()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = null,
                City = "Sofia",
                Street = "Salmon Sushi Street 22",
                Country = "Bolivia",
                ZipCode = "1000",
                FirstName = "Miro",
                LastName = "Ilyovski",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_BillingInfo_Model_When_State_Is_More_Than_50_Chars()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "SofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofia" +
                "SofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofia",
                City = "Sofia",
                Street = "Salmon Sushi Street 22",
                Country = "Bolivia",
                ZipCode = "1000",
                FirstName = "Miro",
                LastName = "Ilyovski",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_BillingInfo_Model_When_City_IsNull()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = null,
                Street = "Salmon Sushi Street 22",
                Country = "Bolivia",
                ZipCode = "1000",
                FirstName = "Miro",
                LastName = "Ilyovski",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_BillingInfo_Model_When_City_Is_More_Than_50_Chars()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "SofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofia" +
                "SofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofia",
                Street = "Salmon Sushi Street 22",
                Country = "Bolivia",
                ZipCode = "1000",
                FirstName = "Miro",
                LastName = "Ilyovski",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_BillingInfo_Model_When_Street_IsNull()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = null,
                Country = "Bolivia",
                ZipCode = "1000",
                FirstName = "Miro",
                LastName = "Ilyovski",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_BillingInfo_Model_When_Street_Is_More_Than_50_Chars()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Salmon Sushi Street 22Salmon Sushi Street 22Salmon Sushi Street " +
                "22Salmon Sushi Street 22Salmon Sushi Street 22Salmon Sushi Street 22Salmon Sushi Street 22" +
                "Salmon Sushi Street 22Salmon Sushi Street 22Salmon Sushi Street 22Salmon Sushi Street 22Salmon Sushi Street 22Salmon Sushi Street 22",
                Country = "Bolivia",
                ZipCode = "1000",
                FirstName = "Miro",
                LastName = "Ilyovski",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_BillingInfo_Model_When_Country_IsNull()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Bolivia",
                Country = null,
                ZipCode = "1000",
                FirstName = "Miro",
                LastName = "Ilyovski",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_BillingInfo_Model_When_Country_Is_More_Than_50_Chars()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Boncho Str 11",
                Country = "BulgariaBulgariaBulgariaBulgariaBulgariaBulgariaBulgariaBulgariaBulgariaBu" +
                "lgariaBulgariaBulgariaBulgariaBulgariaBulgariaBulgaria",
                ZipCode = "1000",
                FirstName = "Miro",
                LastName = "Ilyovski",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }


        [Fact]
        public void Test_BillingInfo_Model_When_ZipCode_IsNull()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Bolivia",
                Country = "bsad",
                ZipCode = null,
                FirstName = "Miro",
                LastName = "Ilyovski",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_BillingInfo_Model_When_ZipCode_Is_More_Than_20_Chars()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Boncho Str 11",
                Country = "Bulgaria",
                ZipCode = "1000000000000000000000000000000000000000000000000",
                FirstName = "Miro",
                LastName = "Ilyovski",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }


        [Fact]
        public void Test_BillingInfo_Model_When_FirstName_IsNull()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Bolivia",
                Country = "bsad",
                ZipCode = "1000",
                FirstName = null,
                LastName = "Ilyovski",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_BillingInfo_Model_When_FirstName_Is_More_Than_100_Chars()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Boncho Str 11",
                Country = "Bulgaria",
                ZipCode = "1000",
                FirstName = "Mirooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo" +
                "oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo",
                LastName = "Ilyovski",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_BillingInfo_Model_When_LastName_IsNull()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Bolivia",
                Country = "bsad",
                ZipCode = "1000",
                FirstName = "sadsad",
                LastName = null,
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_BillingInfo_Model_When_LastName_Is_More_Than_100_Chars()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Boncho Str 11",
                Country = "Bulgaria",
                ZipCode = "1000",
                FirstName = "Miroo",
                LastName = "Ilyovskiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii" +
                "iiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_BillingInfo_Model_When_Phone_IsNull()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Bolivia",
                Country = "bsad",
                ZipCode = "1000",
                FirstName = "Miro",
                LastName = "Ilyovski",
                Phone = null
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_BillingInfo_Model_When_Phone_Is_NotOnlyNumbers()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Boncho Str 11",
                Country = "Bulgaria",
                ZipCode = "1000",
                FirstName = "Miro",
                LastName = "Ilyovski",
                Phone = "asdasdasdsda"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }
    }
}
