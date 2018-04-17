using AutoMapper;
using BlazorApp.BL.Base;
using BlazorApp.BL.Interfaces;
using BlazorApp.DAL.Models;
using BlazorApp.DAL.Repository;
using BlazorApp.Shared.ModelsDTO;
using BlazorApp.Shared.ViewModels;
using BlazorApp.Utility;
using BlazorApp.Utility.Auth;
using BlazorApp.Utility.Mapper;
using BlazorApp.Utility.Utils.Exceptions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static BlazorApp.Utility.Enums;

namespace BlazorApp.BL
{
    public class AccountsBL : BaseBL<ApplicationUserViewModel, ApplicationUserDTO, User>, IAccountsBL
    {

        public AccountsBL(IRepositoryService<ApplicationUserDTO, User> accountRepository, IMapperFactory mapper, IBaseBL logger) 
            : base(accountRepository, mapper, logger)
        {           
        }


        public async Task<AResponse> Register(RegistrationViewModel viewModel)
        {
            AResponse response = null;
            ApplicationUserDTO dtoRegistration = null;

            try
            {
                response = new AResponse();

                dtoRegistration = GetDtoObject(viewModel);        
                var result = await (Repository as AccountRepository).InsertAsync(dtoRegistration);

                if (!result.Succeeded)
                {                    
                    response.Data = result;
                    throw new Exception();
                }

                response.StatusMessage = "Record inserted successfully";
                response.ResponseStatus = ActionResponse.Success;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = ActionResponse.DBInsertFailed;
                response.Exception = ex;
                response.StatusMessage = "Error occurred";
            }

            return response;
        }

        public async Task<AResponse> Login(LoginViewModel viewModel)
        {
            AResponse response = null;
            string jwt = String.Empty;
            ApplicationUserDTO dtoLogin;

            try
            {
                response = new AResponse();
                dtoLogin = GetDtoObject(viewModel);

                var identity = await (Repository as AccountRepository).GetClaimsIdentity(dtoLogin.Username, dtoLogin.Password);

                if (identity == null)
                {
                    throw new InvalidCredentialsException();
                }

                try
                {
                    jwt = await (Repository as AccountRepository).GenerateJwt(identity, dtoLogin.Username);
                }
                catch (Exception ex)
                {
                    throw new JwtGenerateException(dtoLogin.Username);
                }
                

                response.Data = jwt;
                response.ResponseStatus = Enums.ActionResponse.Success;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = ActionResponse.InvalidValues;
                response.Exception = ex;
                response.StatusMessage = "Error occurred";
            }

            return response;
        }        
    }
}
