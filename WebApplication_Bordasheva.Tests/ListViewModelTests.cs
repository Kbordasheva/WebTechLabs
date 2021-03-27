using WebLab4.Entities;
using WebApplication_Bordasheva.Models;
using Xunit;

namespace WebApplication_Bordasheva.Tests
{
    public class ListViewModelTests
    {
        [Fact]
        public void ListViewModelCountsPages()
        {
            // Act 
            var model = ListViewModel<Phone>
                .GetModel(TestData.GetPhonesList(), 1, 3);

            // Assert 
            Assert.Equal(2, model.TotalPages);
        }
        [Theory]
        [MemberData(memberName: nameof(TestData.Params),
            MemberType = typeof(TestData))]
                public void ListViewModelSelectsCorrectQty(int page, int qty, int id)
                    {
                        // Act 
                        var model = ListViewModel<Phone>
                            .GetModel(TestData.GetPhonesList(), page, 3);

                        // Assert 
                        Assert.Equal(qty, model.Count);
                    }
        [Theory]
        [MemberData(memberName: nameof(TestData.Params),
        MemberType = typeof(TestData))]
        public void ListViewModelHasCorrectData(int page, int qty, int id)
        {
            // Act 
            var model = ListViewModel<Phone>
                .GetModel(TestData.GetPhonesList(), page, 3);

            // Assert 
            Assert.Equal(id, model[0].PhoneId);
        }
    }
}
