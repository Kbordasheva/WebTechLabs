using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using WebApplication_Bordasheva.Controllers;
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
    }

    public class ProductControllerTests
    {
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

            // Arrange             
            var controller = new ProductController() {ControllerContext=controllerContext};

            controller._phones = TestData.GetPhonesList();

            // Act 
            var result = controller.Index(pageNo:page, group:null) as ViewResult;
            var model = result?.Model as List<Phone>;

            // Assert 
            Assert.NotNull(model);
            Assert.Equal(qty, model.Count);
            Assert.Equal(id, model[0].PhoneId);
        }

        [Fact]
        public void ControllerSelectsGroup()
        {
            // arrange 
            var controller = new ProductController();
            var data = TestData.GetPhonesList();
            controller._phones = data;

            var comparer = Comparer<Phone>
            .GetComparer((d1, d2) => d1.PhoneId.Equals(d2.PhoneId));

            // act 
            var result = controller.Index(2) as ViewResult;
            var model = result.Model as List<Phone>;

            // assert 
            Assert.Equal(1, model.Count);
            Assert.Equal(data[1], model[0], comparer);
        }


    }

}
