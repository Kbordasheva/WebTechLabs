using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using WebApplication_Bordasheva.Controllers;
using WebLab4.Data;
using WebLab4.Entities;
using Xunit;

namespace WebApplication_Bordasheva.Tests
{
    public class TestData
    {
        public static List<Phone> GetPhonesList()
        {
            return new List<Phone>
                {
                    new Phone{ PhoneId=1, PhoneGroupId=1},
                    new Phone{ PhoneId=2, PhoneGroupId=2},
                    new Phone{ PhoneId=3, PhoneGroupId=3},
                    new Phone{ PhoneId=4, PhoneGroupId=4},
                    new Phone{ PhoneId=5, PhoneGroupId=5}
                };
        }
        public static IEnumerable<object[]> Params()
        {
            // 1-я страница, кол. объектов 3, id первого объекта 1 
            yield return new object[] { 1, 3, 1 };
            // 2-я страница, кол. объектов 2, id первого объекта 4 
            yield return new object[] { 2, 2, 4 };
        }
        public static void FillContext(ApplicationDbContext context)
        {
            context.PhoneGroups.Add(new PhoneGroup
            { GroupName = "fake group" });

            context.AddRange(new List<Phone>
                {
                    new Phone{ PhoneId=1, PhoneGroupId=1},
                    new Phone{ PhoneId=2, PhoneGroupId=1},
                    new Phone{ PhoneId=3, PhoneGroupId=2},
                    new Phone{ PhoneId=4, PhoneGroupId=2},
                    new Phone{ PhoneId=5, PhoneGroupId=3}
                });
            context.SaveChanges();
        }

    }

    public class ProductControllerTests
    {
        DbContextOptions<ApplicationDbContext> _options;

        public ProductControllerTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "testDb")
                .Options;
        }
        [Theory]
        [MemberData(nameof(TestData.Params), MemberType = typeof(TestData))]

        public void ControllerGetsProperPage(int page, int qty, int id)
        {
            // Контекст контроллера 
            var controllerContext = new ControllerContext();
            // Макет HttpContext 
            var moqHttpContext = new Mock<HttpContext>();
            moqHttpContext.Setup(c => c.Request.Headers)
            .Returns(new HeaderDictionary());

            controllerContext.HttpContext = moqHttpContext.Object;

            //заполнить DB данными  
            using (var context = new ApplicationDbContext(_options))
            {
                TestData.FillContext(context);
            }
            using (var context = new ApplicationDbContext(_options))
            {
                // создать объект класса контроллера 
                var controller = new ProductController(context)
                { ControllerContext = controllerContext };


                // Act 
                var result = controller.Index(pageNo: page, group: null) as ViewResult;
                var model = result?.Model as List<Phone>;

                // Assert 
                Assert.NotNull(model);
                Assert.Equal(qty, model.Count);
                Assert.Equal(id, model[0].PhoneId);
            }
            // удалить базу данных из памяти 
            using (var context = new ApplicationDbContext(_options))
            {
                context.Database.EnsureDeleted();
            }
        }



        [Fact]
        public void ControllerSelectsGroup()
        {
            // Контекст контроллера 
            var controllerContext = new ControllerContext();
            // Макет HttpContext 
            var moqHttpContext = new Mock<HttpContext>();
            moqHttpContext.Setup(c => c.Request.Headers)
                .Returns(new HeaderDictionary());

            controllerContext.HttpContext = moqHttpContext.Object;

            //заполнить DB данными  
            using (var context = new ApplicationDbContext(_options))
            {
                TestData.FillContext(context);
            }

            using (var context = new ApplicationDbContext(_options))
            {
                var controller = new ProductController(context)
                { ControllerContext = controllerContext };

                var comparer = Comparer<Phone>
            .GetComparer((d1, d2) => d1.PhoneId.Equals(d2.PhoneId));

                // act 
                var result = controller.Index(2) as ViewResult;
                var model = result.Model as List<Phone>;

                // assert 
                Assert.Equal(2, model.Count);
                Assert.Equal(context.Phones
                    .ToArrayAsync()
                    .GetAwaiter()
                    .GetResult()[2], model[0], comparer);
            }

        }

    }
}
