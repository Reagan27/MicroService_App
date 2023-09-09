using AutoMapper;
using JituPost.Models;
using JituPost.Models.Dtos;

namespace JituPost.Profiles
{
    public class ProductProfile:Profile
    {

        public ProductProfile()
        {
            CreateMap<PostRequestDto,ThePost>().ReverseMap();
        }
    }
}