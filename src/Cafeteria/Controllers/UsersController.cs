﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Сafeteria.DataModels.Entities;
using Сafeteria.Infrastructure.Abstraction;
using Сafeteria.Infrastructure.ModelsDTO;

namespace Сafeteria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> Get([FromRoute] int userId)
        {
            var result = await _userService.GetById(userId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAll();
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("{userId}")]
        public async Task<IActionResult> Delete([FromRoute] int userId)
        {
            var userResult = await _userService.DeleteById(userId);

            if (userResult != true)
            {
                return NotFound();
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.Authenticate(model.Email, model.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] UserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isRegistered = await _userService.Register(model);

            if (isRegistered == false)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(true);
        }
    }
}
