using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Interfaces.Services.User;

namespace Api.Application.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok (await  _service.GetAll());
            }
            catch(ArgumentException e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}