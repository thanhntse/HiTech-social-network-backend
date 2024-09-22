using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiTech.Shared.Controllers
{
    /// <summary>
    /// API Response Tool
    /// </summary>
    /// <remarks>
    ///		- Uses application/json as the default content type.<br/>
    ///		- Provides utility methods to conveniently return common responses.
    /// </remarks>
    public static class HiTechApi
    {
        /// <summary>
        /// Convenience method to return a 200 OK response.
        /// </summary>
        /// <returns></returns>
        public static ApiResponse ResponseOk() => ResponseNoData(200, "Ok.");

        /// <summary>
        /// Convenience method to return a 201 Created response.
        /// </summary>
        /// <returns></returns>
        public static ApiResponse ResponseCreated() => ResponseNoData(201, "Created.");

        /// <summary>
        /// Generic response for "400 Bad request" errors.
        /// </summary>
        public static ApiResponse ResponseBadRequest() => ResponseNoData(400, "Bad request.");

        /// <summary>
        /// Generic response for "401 Unauthorized" errors.
        /// </summary>
        public static ApiResponse ResponseUnauthorized() => ResponseNoData(401, "Authentication required.");

        /// <summary>
        /// Generic response for "403 Forbidden" errors.
        /// </summary>
        public static ApiResponse ResponseForbidden() => ResponseNoData(403, "Access denied.");

        /// <summary>
        /// Generic response for "404 Not Found" errors.
        /// </summary>
        public static ApiResponse ResponseNotFound() => ResponseNoData(404, "Not found.");

        /// <summary>
        /// Generic response for "409 Conflict" errors.
        /// </summary>
        public static ApiResponse ResponseConflict() => ResponseNoData(409, "Conflict.");

        /// <summary>
        /// Generic response for "500 Internal Server Error" errors.
        /// </summary>
        public static ApiResponse ResponseServerError() => ResponseNoData(500, "Internal Server Error.");

        /// <summary>
        /// Generic response for "501 Not Implemented" errors.
        /// </summary>
        public static ApiResponse ResponseNotImplemented() => ResponseNoData(501, "Not implemented.");

        /// <summary>
        /// Generic response for "502 Bad Gateway" errors.
        /// </summary>
        public static ApiResponse ResponseBadGateway() => ResponseNoData(502, "Bad Gateway.");

        /// <summary>
        /// Generic response for "503 Service Unavailable" errors.
        /// </summary>
        public static ApiResponse ResponseUnavailable() => ResponseNoData   (503, "Server is unavailable to handle the request.");

        /// <summary>
        /// Convenience method to return a 200 OK response with data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApiResponse<T> ResponseOk<T>(T data) => new ApiResponse<T>
        {
            Status = 200,
            Message = "Ok.",
            Data = data
        };

        /// <summary>
        /// Convenience method to return a 200 OK response with data and message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApiResponse<T> ResponseOk<T>(string message, T data) => new ApiResponse<T>
        {
            Status = 200,
            Message = message,
            Data = data
        };

        /// <summary>
        /// Convenience method to return a response without attached data.
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public static ApiResponse ResponseNoData(int statusCode) => ResponseNoData(statusCode, null);

        /// <summary>
        /// Convenience method to return a response with a message and without attached data.
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApiResponse ResponseNoData(int statusCode, string? message)
            => String.IsNullOrWhiteSpace(message)
            ?
            new ApiResponse
            {
                Status = statusCode
            }
            :
            new ApiResponse
            {
                Status = statusCode,
                Message = message
            };

        /// <summary>
        /// Convenience method to return a custom response.
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApiResponse<T> Response<T>(int statusCode, string? message, T? data)
            => new ApiResponse<T>
            {
                Status = statusCode,
                Message = message,
                Data = data
            };
    }
}
