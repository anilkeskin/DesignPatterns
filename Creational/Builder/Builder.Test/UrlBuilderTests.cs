namespace Creational.Builder.UrlBuilder.Tests;

public class UrlBuilderTests
{
    // Test 1: Construct URL with no path or query parameters
    [Fact]
    public void Build_ShouldReturnBaseUrl_WhenNoPathAndQueryParamsAreAppended()
    {
        // Arrange
        var builder = new UrlBuilder("https://localhost");

        // Act
        var result = builder.Build();

        // Assert
        Assert.Equal("https://localhost", result);
    }

    // Test 2: Append a path to the URL
    [Fact]
    public void AppendPath_ShouldAddPathSegmentToBaseUrl()
    {
        // Arrange
        var builder = new UrlBuilder("https://localhost");

        // Act
        builder.AppendPath("users");
        var result = builder.Build();

        // Assert
        Assert.Equal("https://localhost/users", result);
    }

    // Test 3: Append multiple path segments
    [Fact]
    public void AppendPath_ShouldAppendMultiplePathSegments()
    {
        // Arrange
        var builder = new UrlBuilder("https://localhost");

        // Act
        builder.AppendPath("users").AppendPath("123").AppendPath("profile");
        var result = builder.Build();

        // Assert
        Assert.Equal("https://localhost/users/123/profile", result);
    }

    // Test 4: Append query parameters to the URL
    [Fact]
    public void AppendQueryParam_ShouldAddQueryParamToUrl()
    {
        // Arrange
        var builder = new UrlBuilder("https://localhost");

        // Act
        builder.AppendPath("users").AppendQueryParam("id", "123");
        var result = builder.Build();

        // Assert
        Assert.Equal("https://localhost/users?id=123", result);
    }

    // Test 5: Append multiple query parameters
    [Fact]
    public void AppendQueryParam_ShouldAddMultipleQueryParams()
    {
        // Arrange
        var builder = new UrlBuilder("https://localhost");

        // Act
        builder.AppendPath("search")
               .AppendQueryParam("query", "unit test")
               .AppendQueryParam("page", "1");
        var result = builder.Build();

        // Assert
        Assert.Equal("https://localhost/search?query=unit%20test&page=1", result);
    }

    // Test 6: Query parameters with null values
    [Fact]
    public void AppendQueryParam_ShouldEncodeNullValues()
    {
        // Arrange
        var builder = new UrlBuilder("https://localhost");

        // Act
        builder.AppendQueryParam("id", null);
        var result = builder.Build();

        // Assert
        Assert.Equal("https://localhost?id=null", result);
    }

    // Test 7: Ensure proper handling of multiple trailing slashes in the base URL
    [Fact]
    public void Constructor_ShouldTrimTrailingSlashesFromBaseUrl()
    {
        // Arrange
        var builder = new UrlBuilder("https://localhost/");

        // Act
        var result = builder.Build();

        // Assert
        Assert.Equal("https://localhost", result);
    }

    // Test 8: Ensure path is correctly formatted without leading slashes
    [Fact]
    public void AppendPath_ShouldTrimLeadingSlashes()
    {
        // Arrange
        var builder = new UrlBuilder("https://localhost");

        // Act
        builder.AppendPath("/users");
        var result = builder.Build();

        // Assert
        Assert.Equal("https://localhost/users", result);
    }

    // Test 9: Empty base URL throws exception
    [Fact]
    public void Constructor_ShouldThrowException_WhenBaseUrlIsEmpty()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => new UrlBuilder(""));
    }

    // Test 10: Null base URL throws exception
    [Fact]
    public void Constructor_ShouldThrowException_WhenBaseUrlIsNull()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => new UrlBuilder(null));
    }

    // Test 11: Special characters in path and query parameters
    [Fact]
    public void AppendPathAndQueryParam_ShouldEncodeSpecialCharacters()
    {
        // Arrange
        var builder = new UrlBuilder("https://localhost");

        // Act
        builder.AppendPath("search")
               .AppendQueryParam("query", "hello world!&special");
        var result = builder.Build();

        // Assert
        Assert.Equal("https://localhost/search?query=hello%20world%21%26special", result);
    }

    // Test 12: Build URL without query parameters
    [Fact]
    public void Build_ShouldReturnUrlWithoutQueryParams_WhenNoParamsAreAppended()
    {
        // Arrange
        var builder = new UrlBuilder("https://localhost");
        builder.AppendPath("users");

        // Act
        var result = builder.Build();

        // Assert
        Assert.Equal("https://localhost/users", result);
    }
}
