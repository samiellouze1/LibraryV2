using AutoMapper;
using LIbrary.Models;
using LIbrary.ViewModels.Profile;

namespace LIbrary.Profiles
{
    public class ProfileProfile:Profile
    {
        public ProfileProfile()
        {
            CreateMap<Reader, ProfileReadVM>();
        }
    }
}
