﻿using lab.Controllers;
using lab.DAL.Data;
using lab.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace lab.Tests
{
    public class ProductControllerTests
    {
        DbContextOptions<ApplicationDbContext> _options;
        public ProductControllerTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "testDb").Options;
        }

        [Theory]
        [MemberData(nameof(TestData.Params), MemberType = typeof(TestData))]
        public void ControllerGetsProperPage(int page, int qty, int id)
        {
            // Arrange
            //Контекст контроллера
            var controllerContext = new ControllerContext();
            
            // Макет HttpContext
            var moqHttpContext = new Mock<HttpContext>(); 
            
            moqHttpContext.Setup(c => c.Request.Headers).Returns(new HeaderDictionary());
            controllerContext.HttpContext = moqHttpContext.Object;

            //заполнить DB данными
            using (var context = new ApplicationDbContext(_options))
            {
                TestData.FillContext(context);
            }

            using (var context = new ApplicationDbContext(_options))
            {
                //создать объект класса контроллера
                var controller = new ProductController(context)

                { ControllerContext = controllerContext };

                // Act
                var result = controller.Index(null, page) as ViewResult;
                var model = result?.Model as List<MusInstrument>;

                // Assert
                Assert.NotNull(model);
                Assert.Equal(qty, model.Count);
                Assert.Equal(id, model[0].Id);
            }

            //удалить базу данных из памяти
            using (var context = new ApplicationDbContext(_options))
            {
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void ControllerSelectsGroup()
        {
            // arrange
            //Контекст контроллера
            var controllerContext = new ControllerContext(); 
            
            // Макет HttpContext
            var moqHttpContext = new Mock<HttpContext>(); 
            
            moqHttpContext.Setup(c => c.Request.Headers).Returns(new HeaderDictionary()); 
            controllerContext.HttpContext = moqHttpContext.Object;

            //заполнить DB данными
            using (var context = new ApplicationDbContext(_options))
            {
                TestData.FillContext(context);
            }

            using (var context = new ApplicationDbContext(_options))
            {
                var controller = new ProductController(context){ ControllerContext = controllerContext };
                var comparer = Comparer<MusInstrument>.GetComparer((i1, i2) => i1.Id.Equals(i2.Id));

                // act
                var result = controller.Index(2) as ViewResult; var model = result.Model as List<MusInstrument>;

                // assert
                Assert.Equal(2, model.Count);
                Assert.Equal(context.MusInstruments.ToArrayAsync().GetAwaiter().GetResult()[2], model[0], comparer);
            }

            //удалить базу данных из памяти
            using (var context = new ApplicationDbContext(_options))
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
