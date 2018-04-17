using AutoMapper;
using BlazorApp.Shared.ModelsDTO;
using BlazorApp.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.BL.Mappings
{
    public class LoginViewModelToApplicationUserDTO : Profile
    {
        public LoginViewModelToApplicationUserDTO()
        {
            /*
             *  Instead of implementing and using certain members you could simply use
                Mapper.CreateMap<destinationModel, sourceModel>(MemberList.Source);  
             *
             */
            CreateMap<LoginViewModel, ApplicationUserDTO>(MemberList.None);
        }
    }

}
