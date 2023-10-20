using Microsoft.AspNetCore.Mvc;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class StatusController : Controller
    {
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> ConsultaValores()
        {
            try
            {
                return "pong";
            }
            catch (Exception ex)
            {
               
                throw;
            }
        }
    }
}
