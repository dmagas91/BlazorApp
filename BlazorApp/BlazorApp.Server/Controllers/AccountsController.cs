using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.DAL.EF;
using BlazorApp.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BlazorApp.Shared.ModelsDTO;
using AutoMapper;
using BlazorApp.Utility;
using BlazorApp.Shared.ViewModels;
using BlazorApp.BL;
using BlazorApp.Utility.Utils.Exceptions;
using BlazorApp.BL.Interfaces;

namespace BlazorApp.Server.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly AccountsBL _accountsBL;

        public AccountsController(IAccountsBL accountsBL)
        {
            _accountsBL = accountsBL as AccountsBL;
        }

        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
        {
            AResponse response = null;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            response = await _accountsBL.Register(model);

            if(response.ResponseStatus != Enums.ActionResponse.Success)
            {
                return new BadRequestObjectResult(Errors.AddErrorsToModelState((IdentityResult)response.Data, ModelState));
            }

            return new OkObjectResult("Account created");
        }

        // POST api/accounts/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel credentials)
        {
            AResponse response = null;
            string jwt = String.Empty;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            response = await _accountsBL.Login(credentials);

            if (response.ResponseStatus != Enums.ActionResponse.Success)
            {
                if (response.Exception.GetType() == typeof(InvalidCredentialsException))
                {
                    return BadRequest(Errors.AddErrorToModelState("login_failure", response.Exception.Message, ModelState));
                }
                else if (response.Exception.GetType() == typeof(JwtGenerateException))
                {
                    return BadRequest(Errors.AddErrorToModelState("jwt_exception", response.Exception.Message, ModelState));
                }
                else
                {
                    return BadRequest(Errors.AddErrorToModelState("errors", response.Exception.Message, ModelState));
                }
            }

            jwt = (string)response.Data;

            return new OkObjectResult(jwt);
        }
    }
}