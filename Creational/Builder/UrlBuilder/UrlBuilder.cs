using System.Text;

namespace Creational.Builder.UrlBuilder;

public class UrlBuilder
{
    private readonly StringBuilder _pathBuilder = new();
    private readonly Dictionary<string, string> _queryParams = new();
    private const char DefaultDelimiter = '/';
    private const char QueryParamDelimiter = '&';
    private const char QueryStringStart = '?';

    public string BaseUrl { get; set; }

    // Constructor that initializes the base URL and trims trailing slashes
    public UrlBuilder(string baseUrl)
    {
        if (string.IsNullOrWhiteSpace(baseUrl))
            throw new ArgumentException("Base URL cannot be null or empty", nameof(baseUrl));

        BaseUrl = baseUrl.TrimEnd('/');
    }

    // Appends a path segment to the URL
    public UrlBuilder AppendPath(string segment)
    {
        if (string.IsNullOrWhiteSpace(segment))
            throw new ArgumentException("Path segment cannot be null or empty.", nameof(segment));

        // Ensure the path is correctly formatted by trimming any leading slashes
        _pathBuilder.Append(DefaultDelimiter);
        _pathBuilder.Append(segment.Trim('/'));

        return this;
    }

    // Appends a query parameter to the URL
    public UrlBuilder AppendQueryParam(string key, string value)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Query parameter key cannot be null or empty.", nameof(key));

        // URL encoding is applied only to query parameters.
        if (value == null)
        {
            _queryParams[Uri.EscapeDataString(key)] = null;
        }
        else
        {
            // We encode the query parameters using Uri.EscapeDataString.
            _queryParams[Uri.EscapeDataString(key)] = Uri.EscapeDataString(value);
        }

        return this;
    }

    // Builds and returns the final URL
    public string Build()
    {
        var url = BuildUrl();
        var queryParams = BuildQueryString();

        // If there are query parameters, append them to the URL
        if (!string.IsNullOrWhiteSpace(queryParams))
        {
            url += QueryStringStart + queryParams;
        }

        return url;
    }

    // Builds the base URL with path segments appended
    private string BuildUrl()
    {
        var url = new StringBuilder(BaseUrl);

        // Append the path if there are any segments
        if (_pathBuilder.Length > 0)
        {
            url.Append(DefaultDelimiter).Append(_pathBuilder.ToString().TrimStart(DefaultDelimiter));
        }

        return url.ToString();
    }

    // Constructs the query string from the stored parameters
    private string BuildQueryString()
    {
        var queryParams = new StringBuilder();

        // Iterate through each query parameter and append it to the query string
        foreach (var param in _queryParams)
        {
            if (queryParams.Length > 0)
                queryParams.Append(QueryParamDelimiter);

            // If the parameter value is null, include "null" in the query string
            if (param.Value == null)
            {
                queryParams.AppendFormat("{0}=null", param.Key);
            }
            else
            {
                queryParams.AppendFormat("{0}={1}", param.Key, param.Value);
            }
        }

        return queryParams.ToString();
    }
}