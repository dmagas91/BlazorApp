using AutoMapper;
using BlazorApp.DAL.Models;
using BlazorApp.Shared.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.DAL.Mappings
{
    public class ApplicationUserDTOToUser : Profile
    {
        public ApplicationUserDTOToUser()
        {
            /*
             *  Instead of implementing and using certain members you could simply use
                Mapper.CreateMap<destinationModel, sourceModel>(MemberList.Source);  
             *
             */
            CreateMap<ApplicationUserDTO, User>(MemberList.None);
        }
    }
}
