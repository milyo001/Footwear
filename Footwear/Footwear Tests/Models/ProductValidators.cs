

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
        public void Test_Product_Model__Name_Property_Throw_Error_When_Null()
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
    }
}
