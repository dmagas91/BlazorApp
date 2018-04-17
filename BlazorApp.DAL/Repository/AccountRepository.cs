using BlazorApp.DAL.EF;
using BlazorApp.DAL.Models;
using BlazorApp.Shared.ModelsDTO;
using BlazorApp.Utility;
using BlazorApp.Utility.Auth;
using BlazorApp.Utility.Mapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.DAL.Repository
{
    public class AccountRepository : RepositoryService<ApplicationUserDTO, User>
    {
        private readonly Expression<Func<User, bool>> _filters;
        private readonly UserManager<User> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public AccountRepository(IDbService dbService, IMapperFactory mapperFactory, UserManager<User> userManager,
            IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions, BlazorContext dbContext) 
            : base(dbService, mapperFactory, dbContext)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        public AccountRepository(IDbService dbService, IMapperFactory mapperFactory)
            : base(dbService, mapperFactory)
        {

        }

        public AccountRepository(IDbService dbService, IMapperFactory mapperFactory, Expression<Func<User, bool>> filters) 
             : base(dbService, mapperFactory)
        {
            _filters = filters;
        }

        public async Task<IdentityResult> InsertAsync(ApplicationUserDTO dtoObject)
        {
            var entity = GetEntityObject(dtoObject);
            var result = await _userManager.CreateAsync(entity, entity.Password);
            return result;
        }

        public async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify =  _userManager.FindByNameAsync(userName).Result;

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (_userManager.CheckPasswordAsync(userToVerify, password).Result)
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id.ToString()));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }

        public async Task<string> GenerateJwt(ClaimsIdentity identity, string username)
        {
            return await Tokens.GenerateJwt(identity, _jwtFactory, username, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

        public override Expression<Func<User, bool>> GetFilters()
        {
            return _filters;
        }
        public override Expression<Func<User, bool>> SearchFilters(User update)
        {
            return x => x.Id == update.Id;
        }
    }
}
