using BookWebApi.ReturnModels;
using Microsoft.AspNetCore.Mvc;

namespace BookWebApi.Controller
{
    public class BaseController : ControllerBase
    {

        public IActionResult ResponseForStatusCode<T>(ReturnModel<T> response)
        {
            //1. Yöntem

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(response);
                    break;

                case System.Net.HttpStatusCode.Created:
                    return Created("/", response);
                    break;

                case System.Net.HttpStatusCode.BadRequest:
                    return BadRequest(response);
                    break;

                case System.Net.HttpStatusCode.NotFound:
                    return NotFound(response);
                    break;

                default:
                    return NoContent();
            }


        }

    }
}
