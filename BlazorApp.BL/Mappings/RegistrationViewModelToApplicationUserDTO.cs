using AutoMapper;
using BlazorApp.Shared.ModelsDTO;
using BlazorApp.Shared.ViewModels;

namespace BlazorApp.BL.Mappings
{
    public class RegistrationViewModelToApplicationUserDTO : Profile
    {
        public RegistrationViewModelToApplicationUserDTO()
        {
            /*
             *  Instead of implementing and using certain members you could simply use
                Mapper.CreateMap<destinationModel, sourceModel>(MemberList.Source);  
             *
             */
            CreateMap<RegistrationViewModel, ApplicationUserDTO>(MemberList.None);            
        }
    }
}
