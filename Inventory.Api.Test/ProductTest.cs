using Inventory.Api.Test.Common;
using Inventory.Api.Test.Fixture;
using Inventory.Application.DTOs;
using Inventory.Application.HttpErrors;
using Inventory.Application.Wrappers;
using Inventory.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Inventory.Api.Test
{
    [Collection("Product Controller")]
    public class ProductTest : IClassFixture<TestFixture<Startup>>
    {
        private readonly ITestOutputHelper _output;
        private readonly HttpClient _client;

        public ProductTest(TestFixture<Startup> fixture, ITestOutputHelper output)
        {
            _client = fixture.Client;
            _output = output ?? throw new ArgumentNullException(nameof(output));
            var builder = new DbContextOptionsBuilder<InventoryContext>();
            builder.UseInMemoryDatabase("Test");
        }

        [Fact]
        public async Task When_Get_All_Product()
        {
            //Arrange

            //Act
            var response = await _client.GetAsync(ApiRoutes.Product.GetAllProducts);
            var jsonFromResponse = await response.Content.ReadAsStringAsync();

            var singleResponse = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<ProductDto>>>(jsonFromResponse);

            //Assert

            Assert.True((int)response.StatusCode == StatusCodes.Status200OK);
            Assert.NotNull(singleResponse);
        }

        [Fact]
        public async Task When_Valid_Product_Fluent_Validator()
        {
            //Arrange
            var productDto = new ProductDto
            {
                Name = "string",
                Description = "string",
                Sku = "string",
                Price = 0,
                MinimunStock = 0,
                MaximumStock = 0,
                Stock = null
            };
            //Act
            var response = await _client.PostAsync(ApiRoutes.Product.CreateProduct, ContentHelper.GetStringContent(productDto));
            var jsonFromResponse = await response.Content.ReadAsStringAsync();

            var singleResponse = JsonConvert.DeserializeObject<HttpError>(jsonFromResponse);

            //Assert
            Assert.True((int)response.StatusCode == StatusCodes.Status400BadRequest);
            Assert.NotNull(singleResponse);
            Assert.True(singleResponse.ValidationErrors[0].Equals("'Sku' no debería ser igual a 'string'."));
        }

        [Theory]
        [InlineData("4d8830e2-465c-4a54-ad02-f875073c85dc")]
        public async Task When_Get_Product_By_Id(string id)
        {
            //Arrange

            //Act
            var response = await _client.GetAsync(string.Format(ApiRoutes.Product.GetProductById, id));
            var jsonFromResponse = await response.Content.ReadAsStringAsync();

            var singleResponse = JsonConvert.DeserializeObject<HttpError>(jsonFromResponse);

            //Assert
            Assert.True((int)response.StatusCode == StatusCodes.Status400BadRequest);
            Assert.NotNull(singleResponse);
            Assert.True(singleResponse.Message[0].Equals("Product doesn't exist"));
        }        
    }
}