using Inventory.Api.Test.Common;
using Inventory.Api.Test.Fixture;
using Inventory.Application.DTOs;
using Inventory.Application.HttpErrors;
using Inventory.Application.Wrappers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Inventory.Api.Test
{
    [Collection("Warehouse Controller")]
    public class WarehouseTest : IClassFixture<TestFixture<Startup>>
    {
        private readonly ITestOutputHelper _output;
        private readonly HttpClient _client;

        public WarehouseTest(TestFixture<Startup> fixture, ITestOutputHelper output)
        {
            _client = fixture.Client;
            _output = output ?? throw new ArgumentNullException(nameof(output));
        }

        [Fact]
        public async Task When_Get_All_Warehouse()
        {
            //Arrange

            //Act
            var response = await _client.GetAsync(ApiRoutes.Warehouse.GetWarehouses);
            var jsonFromResponse = await response.Content.ReadAsStringAsync();

            var singleResponse = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<WarehouseDto>>>(jsonFromResponse);

            //Assert

            Assert.True((int)response.StatusCode == StatusCodes.Status200OK);
            Assert.NotNull(singleResponse);
        }

        [Fact]
        public async Task When_Valid_Warehouse_Fluent_Validator()
        {
            //Arrange
            var WarehouseDto = new WarehouseDto
            {
                Name = 0,
                Description = "string",
                Location = "string",
                MaximumCapacity = 0
            };
            //Act
            var response = await _client.PostAsync(ApiRoutes.Warehouse.CreateWarehouse, ContentHelper.GetStringContent(WarehouseDto));
            var jsonFromResponse = await response.Content.ReadAsStringAsync();

            var singleResponse = JsonConvert.DeserializeObject<HttpError>(jsonFromResponse);

            //Assert
            Assert.True((int)response.StatusCode == StatusCodes.Status400BadRequest);
            Assert.NotNull(singleResponse);
            Assert.True(singleResponse.ValidationErrors[0].Equals("'Name' no debería estar vacío."));
        }

        [Theory]
        [InlineData("4d8830e2-465c-4a54-ad02-f875073c85dc")]
        public async Task When_Get_Warehouse_By_Id(string id)
        {
            //Arrange

            //Act
            var response = await _client.GetAsync(string.Format(ApiRoutes.Warehouse.ExistWarehouseById, id));
            var jsonFromResponse = await response.Content.ReadAsStringAsync();

            var singleResponse = JsonConvert.DeserializeObject<HttpError>(jsonFromResponse);

            //Assert
            Assert.True((int)response.StatusCode == StatusCodes.Status400BadRequest);
            Assert.NotNull(singleResponse);
            Assert.True(singleResponse.Message[0].ToLower().Equals("warehouse doesn't exist"));
        }
    }
}