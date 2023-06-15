using Microsoft.AspNetCore.Mvc;

namespace Project_NGO.CustomStatusCode
{
    public class CustomHandleResultJson
    {
        public static ActionResult GetActionJson<T>(IEnumerable<T> resources)
        {
            if (resources != null && resources.Any())
            {
                var response = new CustomStatusResult<IEnumerable<T>>
                    (StatusCodes.Status200OK, "Get list success", resources, null);
                return new OkObjectResult(response);
            }
            else
            {
                var response = new CustomStatusResult<IEnumerable<T>>
                    (StatusCodes.Status404NotFound, "Not found result or data", null, null);
                return new NotFoundObjectResult(response);
            }
        }

        public static ActionResult GetActionJsonById<T>(T resource, int id)
        {
            if (resource != null)
            {
                var response = new CustomStatusResult<T>
                    (StatusCodes.Status201Created, "Find id success", resource, null);
                return new OkObjectResult(resource);
            }
            else
            {
                var response = new CustomStatusResult<Object>
                    (StatusCodes.Status400BadRequest, "Find id Fail", null, null);
                return new BadRequestObjectResult(response);
            }
        }

        public static ActionResult PostActionJson<T>(T resource)
        {
            if (resource != null)
            {
                var response = new CustomStatusResult<T>
                    (StatusCodes.Status201Created, "Created success", resource, null);
                return new OkObjectResult(resource);
            }
            else
            {
                var response = new CustomStatusResult<Object>
                    (StatusCodes.Status400BadRequest, "Create fail", null, null);
                return new BadRequestObjectResult(response);
            }
        }

        public static ActionResult PutActionJson<T>(T result)
        {
            if (result != null)
            {
                var response = new CustomStatusResult<T>
                    (StatusCodes.Status200OK, "Updated success", result, null);
                return new OkObjectResult(response);
            }
            else
            {
                var response = new CustomStatusResult<Object>
                    (StatusCodes.Status400BadRequest, "Update fail", null, null);
                return new BadRequestObjectResult(response);
            }
        }

        public static ActionResult DeleteActionJson<T>(bool result)
        {
            if (result)
            {
                var response = new CustomStatusResult<Object>
                    (StatusCodes.Status200OK, "Deleted success", null, null);
                return new OkObjectResult(result);
            }
            else
            {
                var response = new CustomStatusResult<Object>
                    (StatusCodes.Status400BadRequest, "Delete fail", null, null);
                return new BadRequestObjectResult(response);
            }
        }

        public static ActionResult ErrorActionResult<T>(Exception ex)
        {
            var response = new CustomStatusResult<Object>
                (StatusCodes.Status500InternalServerError, "An error occured while retrived model", null, ex.Message);
            return new ObjectResult(response);
        }
    }
}
