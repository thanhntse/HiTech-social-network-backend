using System.Text.Json.Serialization;

namespace HiTech.Shared.Controllers;

/// <summary>
/// Response to an API call with no data.
/// </summary>
public class ApiResponse
{
    /// <summary>
    /// Status of the API call, following HTTP status codes convention.
    /// </summary>
    [JsonPropertyName("status")]
    [JsonPropertyOrder(1)]
    public int Status { get; set; }

    /// <summary>
    /// Extra information if any (e.g. the detailed error message).
    /// </summary>
    [JsonPropertyName("message")]
    [JsonPropertyOrder(2)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; set; }

    /// <summary>
    /// Extra data if any.
    /// </summary>
    [JsonPropertyName("extras")]
    [JsonPropertyOrder(4)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Extras { get; set; }

    /// <summary>
    /// Debug information if any.
    /// </summary>
    [JsonPropertyName("debug_info")]
    [JsonPropertyOrder(5)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? DebugInfo { get; set; }
}

/// <summary>
/// Response to an API call with data type T.
/// </summary>
public class ApiResponse<T> : ApiResponse
{
    /// <summary>
    /// The data returned by the API call (specific to individual API).
    /// </summary>
    [JsonPropertyName("data")]
    [JsonPropertyOrder(3)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public T? Data { get; set; }
}
