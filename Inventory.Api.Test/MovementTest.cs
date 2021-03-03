using Inventory.Api.Test.Common;
using Inventory.Api.Test.Fixture;
using Inventory.Application.DTOs;
using Inventory.Application.HttpErrors;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Inventory.Api.Test
{
    [Collection("Movement Controller")]
    public class MovementTest : IClassFixture<TestFixture<Startup>>
    {
        private readonly ITestOutputHelper _output;
        private readonly HttpClient _client;

        public MovementTest(TestFixture<Startup> fixture, ITestOutputHelper output)
        {
            _client = fixture.Client;
            _output = output ?? throw new ArgumentNullException(nameof(output));
        }

        [Fact]
        public async Task When_Valid_Movement_Fluent_Validator()
        {
            //Arrange
            var movementDto = new MovementDto
            {
                MovementDate = DateTime.MinValue,
                Quantity = 0,
                Price = 0
            };
            //Act
            var response = await _client.PostAsync(ApiRoutes.Movement.LoadWarehouse, ContentHelper.GetStringContent(movementDto));
            var jsonFromResponse = await response.Content.ReadAsStringAsync();

            var singleResponse = JsonConvert.DeserializeObject<HttpError>(jsonFromResponse);

            //Assert
            Assert.True((int)response.StatusCode == StatusCodes.Status400BadRequest);
            Assert.NotNull(singleResponse);
            Assert.True(singleResponse.ValidationErrors[0].Equals("'Price' no debería estar vacío."));
        }

        [Fact]
        public async Task When_Load_Movement_Ok()
        {
            //Arrange
            var movementDto = new MovementDto
            {
                Type = true,
                MovementDate = DateTime.MinValue,
                Quantity = 35,
                Price = 32.56M,
                ProductId = Guid.NewGuid(),
                WarehouseId = Guid.NewGuid()
            };
            //Act
            var response = await _client.PostAsync(ApiRoutes.Movement.LoadWarehouse, ContentHelper.GetStringContent(movementDto));
            var jsonFromResponse = await response.Content.ReadAsStringAsync();

            var singleResponse = JsonConvert.DeserializeObject<HttpError>(jsonFromResponse);

            //Assert
            Assert.True((int)response.StatusCode == StatusCodes.Status400BadRequest);
            Assert.NotNull(singleResponse);
            Assert.True(singleResponse.Message[0].Equals("Product doesn't exist"));
        }
    }
}