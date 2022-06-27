﻿

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

            var errors = ValidateModel(correctAddress);
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

            var errors = ValidateModel(correctAddress);
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

            var errors = ValidateModel(correctAddress);
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

            var errors = ValidateModel(correctAddress);
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

            var errors = ValidateModel(correctAddress);
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

            var errors = ValidateModel(appData);
            Assert.True(errors.Count == 0);
        }

        // Billing information

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

            var errors = ValidateModel(billingInformation);
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

            var errors = ValidateModel(billingInformation);
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

            var errors = ValidateModel(billingInformation);
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

            var errors = ValidateModel(billingInformation);
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

            var errors = ValidateModel(billingInformation);
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

            var errors = ValidateModel(billingInformation);
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

            var errors = ValidateModel(billingInformation);
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

            var errors = ValidateModel(billingInformation);
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

            var errors = ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }
    }
}