using lab.Controllers;
using lab.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

namespace lab.Tests
{
    public class ProductControllerTests
    {
        [Theory]
        [MemberData(nameof(TestData.Params), MemberType = typeof(TestData))]
        public void ControllerGetsProperPage(int page, int qty, int id)
        {
            // Arrange
            var controller = new ProductController();

            controller._instruments = TestData.GetInstrumentsList();

            // Act
            var result = controller.Index(null, page) as ViewResult; 
            var model = result?.Model as List<MusInstrument>;

            // Assert
            Assert.NotNull(model);
            Assert.Equal(qty, model.Count);
            Assert.Equal(id, model[0].Id);
        }

        [Fact]
        public void ControllerSelectsGroup()
        {
            // arrange
            var controller = new ProductController(); 
            var data = TestData.GetInstrumentsList(); 
            controller._instruments = data;

            var comparer = Comparer<MusInstrument>.GetComparer((i1, i2) => i1.Id.Equals(i2.Id));

            // act
            var result = controller.Index(2) as ViewResult;
            var model = result.Model as List<MusInstrument>;

            // assert
            Assert.Equal(2, model.Count);
            Assert.Equal(data[2], model[0], comparer);
        }
    }
}
