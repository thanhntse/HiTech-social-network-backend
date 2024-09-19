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
        public static ApiResponse<T> ResponseOk<T>() => ResponseNoData<T>(200, "Ok.");

        /// <summary>
        /// Convenience method to return a 201 Created response.
        /// </summary>
        /// <returns></returns>
        public static ApiResponse<T> ResponseCreated<T>() => ResponseNoData<T>(201, "Created.");

        /// <summary>
        /// Generic response for "400 Bad request" errors.
        /// </summary>
        public static ApiResponse<T> ResponseBadRequest<T>() => ResponseNoData<T>(400, "Bad request.");

        /// <summary>
        /// Generic response for "401 Unauthorized" errors.
        /// </summary>
        public static ApiResponse<T> ResponseUnauthorized<T>() => ResponseNoData<T>(401, "Authentication required.");

        /// <summary>
        /// Generic response for "403 Forbidden" errors.
        /// </summary>
        public static ApiResponse<T> ResponseForbidden<T>() => ResponseNoData<T>(403, "Access denied.");

        /// <summary>
        /// Generic response for "404 Not Found" errors.
        /// </summary>
        public static ApiResponse<T> ResponseNotFound<T>() => ResponseNoData<T>(404, "Not found.");

        /// <summary>
        /// Generic response for "409 Conflict" errors.
        /// </summary>
        public static ApiResponse<T> ResponseConflict<T>() => ResponseNoData<T>(409, "Conflict.");

        /// <summary>
        /// Generic response for "500 Internal Server Error" errors.
        /// </summary>
        public static ApiResponse<T> ResponseServerError<T>() => ResponseNoData<T>(500, "Internal Server Error.");

        /// <summary>
        /// Generic response for "501 Not Implemented" errors.
        /// </summary>
        public static ApiResponse<T> ResponseNotImplemented<T>() => ResponseNoData<T>(501, "Not implemented.");

        /// <summary>
        /// Generic response for "502 Bad Gateway" errors.
        /// </summary>
        public static ApiResponse<T> ResponseBadGateway<T>() => ResponseNoData<T>(502, "Bad Gateway.");

        /// <summary>
        /// Generic response for "503 Service Unavailable" errors.
        /// </summary>
        public static ApiResponse<T> ResponseUnavailable<T>() => ResponseNoData<T>(503, "Server is unavailable to handle the request.");

        /// <summary>
        /// Convenience method to return a 200 OK response with data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApiResponse<T> ResponseOk<T>(T? data) => data == null ? ResponseOk<T>() : new ApiResponse<T>
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
        public static ApiResponse<T> ResponseOk<T>(string message, T? data) => data == null ? ResponseOk<T>() : new ApiResponse<T>
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
        public static ApiResponse<T> ResponseNoData<T>(int statusCode) => ResponseNoData<T>(statusCode, null);

        /// <summary>
        /// Convenience method to return a response with a message and without attached data.
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApiResponse<T> ResponseNoData<T>(int statusCode, string? message)
            => String.IsNullOrWhiteSpace(message)
            ?
            new ApiResponse<T>
            {
                Status = statusCode
            }
            :
            new ApiResponse<T>
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
