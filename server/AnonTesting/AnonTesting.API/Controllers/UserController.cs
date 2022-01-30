﻿using AnonTesting.BLL.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace AnonTesting.API.Controllers
{
    [ApiController]
    [Route("api/user/")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            try
            {
                var token = await _mediator.Send(command);

                return Ok(_tokenHandler.WriteToken(token));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Register([FromBody] LoginUserCommand command)
        {
            try
            {
                var token = await _mediator.Send(command);

                return Ok(_tokenHandler.WriteToken(token));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
