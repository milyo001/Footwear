

namespace Footwear_Tests.Models
{
    using Footwear.Data.Models;
    using Xunit;

    public class ProductValidators
    {
        [Fact]
        public void Test_Product_Model_Works_As_Expected()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Nice shoe 22",
                Details = "Runn as fast as bullet",
                Gender = Footwear.Data.Models.Enums.Gender.Men,
                ImageId = 1,
                Price = 231.22,
                ProductImage = new ProductImage(),
                ProductType = Footwear.Data.Models.Enums.ProductType.Running
            };

            var errors = DataAnnotationsValidators.ValidateModel(product);
            Assert.True(errors.Count == 0);
        }

        [Fact]
        public void Test_Product_Model_Name_Property_Throw_Error_When_Null()
        {
            var product = new Product
            {
                Id = 1,
                Name = null,
                Details = "Runn as fast as bullet",
                Gender = Footwear.Data.Models.Enums.Gender.Men,
                ImageId = 1,
                Price = 231.22,
                ProductImage = new ProductImage(),
                ProductType = Footwear.Data.Models.Enums.ProductType.Running
            };

            var errors = DataAnnotationsValidators.ValidateModel(product);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_Product_Model_Price_Property_Throw_Error_When_OutOf_Min_Range()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Waksjdk",
                Details = "Runn as fast as bullet",
                Gender = Footwear.Data.Models.Enums.Gender.Men,
                ImageId = 1,
                Price = -22.22,
                ProductImage = new ProductImage(),
                ProductType = Footwear.Data.Models.Enums.ProductType.Running
            };

            var errors = DataAnnotationsValidators.ValidateModel(product);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_Product_Model_Price_Property_Throw_Error_When_OutOf_Max_Range()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Waksjdk",
                Details = "Runn as fast as bullet",
                Gender = Footwear.Data.Models.Enums.Gender.Men,
                ImageId = 1,
                Price = 2312222222222.22,
                ProductImage = new ProductImage(),
                ProductType = Footwear.Data.Models.Enums.ProductType.Running
            };

            var errors = DataAnnotationsValidators.ValidateModel(product);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_Product_Model_Details_Property_Throw_Error_When_Null()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Waksjdk",
                Details = null,
                Gender = Footwear.Data.Models.Enums.Gender.Men,
                ImageId = 1,
                Price = 222.11,
                ProductImage = new ProductImage(),
                ProductType = Footwear.Data.Models.Enums.ProductType.Running
            };

            var errors = DataAnnotationsValidators.ValidateModel(product);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_Product_Model_Details_Property_Throw_Error_When_Value_Too_Long()
        {
            var loopedDetails = string.Empty;
            var sb = new System.Text.StringBuilder();
            
            for (int i = 0; i < 100; i++)
            {
                sb.Append("lorem");
                sb.Append(' ');
            }
            
            var product = new Product
            {
                Id = 1,
                Name = "Waksjdk",
                Details = loopedDetails.ToString(),
                Gender = Footwear.Data.Models.Enums.Gender.Men,
                ImageId = 1,
                Price = 231.22,
                ProductImage = new ProductImage(),
                ProductType = Footwear.Data.Models.Enums.ProductType.Running
            };

            var errors = DataAnnotationsValidators.ValidateModel(product);
            Assert.True(errors.Count == 1);
        }
    }
}
