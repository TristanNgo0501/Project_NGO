using Microsoft.AspNetCore.Mvc;
using Project_NGO.Utils;

namespace Project_NGO.Models.CustomResponse
{
    public static class CustomMethodResponse
    {
        public static ActionResult GetListResponse200Ok<T>(IEnumerable<T> result, string message)
        {
            var response = new CustomStatusResult<IEnumerable<T>>
                (StatusCodes.Status200OK, message, result, null);
            return new OkObjectResult(response);
        }

        public static ActionResult GetResponse200Ok<T>(T result, string message)
        {
            var response = new CustomStatusResult<T>
                    (StatusCodes.Status200OK, message, result, null);
            return new OkObjectResult(response);
        }

        public static ActionResult GetResponse201Created<T>(T result, string message)
        {
            var response = new CustomStatusResult<T>
                    (StatusCodes.Status201Created, message, result, null);
            return new OkObjectResult(response);
        }

        public static ActionResult GetResponse400BadResquest(string message)
        {
            var response = new CustomStatusResult<Object>
                     (StatusCodes.Status400BadRequest, message, null, null);
            return new BadRequestObjectResult(response);
        }

        public static ActionResult GetResponse404NotFound(string message)
        {
            var response = new CustomStatusResult<Object>
                     (StatusCodes.Status404NotFound, message, null, null);
            return new NotFoundObjectResult(response);
        }

        public static ActionResult Response500Error(Exception ex)
        {
            var response = new CustomStatusResult<Object>
                (StatusCodes.Status500InternalServerError, "An error occured while retrived model", null, new List<string> { ex.Message });
            return new ObjectResult(response);
        }
    }
}
