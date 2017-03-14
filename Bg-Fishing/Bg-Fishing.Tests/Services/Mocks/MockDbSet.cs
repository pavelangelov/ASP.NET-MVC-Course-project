using System.Data.Entity;
using System.Linq;

using Moq;

namespace Bg_Fishing.Tests.Services.Mocks
{
    public class MockDbSet
    {
        public static Mock<IDbSet<T>> Mock<T>(IQueryable<T> queryable) where T : class
        {
            var mockSet = new Mock<IDbSet<T>>();

            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            return mockSet;
        }
    }
}
