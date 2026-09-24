using PrestaKit.Entities;
using PrestaKit.Querying;
using Shouldly;

namespace PrestaKit.Tests.Querying
{
    public class DummyClass
    {
        public string? Name { get; set; }
    }

    [Trait("Category", "Unit")]
    public class QueryTests
    {
        [Fact]
        public void WhereEquals_ShouldAddEscapedFilterParamToQuery()
        {
            // Arrange
            var query = new Query<Product>();
            query.WhereEquals(x => x.Name, "Chair Mark&Brown Small/Medium/Large");

            // Act
            var queryString = query.ToQueryString(DisplayMode.IdsOnly);

            // Assert
            queryString.ShouldBe("filter[name]=Chair%20Mark%26Brown%20Small%2FMedium%2FLarge");
        }

        [Fact]
        public void WhereBeginsWith_ShouldAddEscapedWildcardFilterParamToQuery()
        {
            // Arrange
            var query = new Query<Product>();
            query.WhereBeginsWith(x => x.Name, "Chair");

            // Act
            var queryString = query.ToQueryString(DisplayMode.IdsOnly);

            // Assert
            queryString.ShouldBe("filter[name]=[Chair]%");
        }

        [Fact]
        public void WhereBetween_ShouldAddEscapedRangeFilterParamToQuery()
        {
            // Arrange
            var query = new Query<Product>();
            query.WhereBetween(x => x.Id, "1", "10");

            // Act
            var queryString = query.ToQueryString(DisplayMode.IdsOnly);

            // Assert
            queryString.ShouldBe("filter[id]=[1,10]");
        }

        [Fact]
        public void OrderBy_WhenAscending_ShouldAddAscendingSortParamToQuery()
        {
            // Arrange
            var query = new Query<Product>();
            query.OrderBy(x => x.Id, false);

            // Act
            var queryString = query.ToQueryString(DisplayMode.IdsOnly);

            // Assert
            queryString.ShouldBe("sort=[id_ASC]");
        }

        [Fact]
        public void OrderBy_WhenDescending_ShouldAddDescendingSortParamToQuery()
        {
            // Arrange
            var query = new Query<Product>();
            query.OrderBy(x => x.Id, true);

            // Act
            var queryString = query.ToQueryString(DisplayMode.IdsOnly);

            // Assert
            queryString.ShouldBe("sort=[id_DESC]");
        }

        [Fact]
        public void Page_ShouldAddLimitParamToQuery()
        {
            // Arrange
            var query = new Query<Product>();
            query.Page(1, 10);

            // Act
            var queryString = query.ToQueryString(DisplayMode.IdsOnly);

            // Assert
            queryString.ShouldBe("limit=1,10");
        }

        [Fact]
        public void OrderBy_WhenExpressionBodyInvalid_ShouldThrowNotSupportedException()
        {
            // Arrange
            var query = new Query<Product>();


            // Act && Assert
            Should.Throw(() => query.OrderBy(x => x.Name == x.Description), typeof(NotSupportedException));
        }

        [Fact]
        public void OrderBy_WhenWithoutXmlElementAttribute_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var query = new Query<DummyClass>();


            // Act && Assert
            Should.Throw(() => query.OrderBy(x => x.Name), typeof(InvalidOperationException));
        }

        [Fact]
        public void OrderBy_ShouldResolveName_WhenExpressionWrappedInConvert()
        {
            // Arrange
            var query = new Query<Product>().OrderBy(p => (object)p.Id!);

            // Act
            var result = query.ToQueryString(DisplayMode.IdsOnly);

            // Assert
            result.ShouldContain("sort=[id_ASC]");
        }
    }
}
