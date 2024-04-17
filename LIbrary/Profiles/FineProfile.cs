using AutoMapper;
using LIbrary.Models;
using LIbrary.ViewModels.Fine;

namespace LIbrary.Profiles
{
    public class FineProfile:Profile
    {
        public FineProfile()
        {
            CreateMap<Fine, FineReadVM>();
        }
    }
}
