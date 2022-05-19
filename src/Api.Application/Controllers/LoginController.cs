using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Entites;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginService _service;
        public LoginController(ILoginService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<object> Login ([FromBody] UserEntity userEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(userEntity==null)
            {
                return BadRequest();
            } 

            try
            {
                var result = await _service.FindByLogin(userEntity);
                if (result==null)
                    return NotFound();
                return Ok(result);                                
            }
            catch(ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message );
            }
        }
    }
}