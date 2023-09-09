using AutoMapper;
using JituComments.Models;
using JituComments.Models.Dtos;

namespace JituProduct.Profiles
{
    public class ProductProfile:Profile
    {

        public ProductProfile()
        {
            CreateMap<CommentRequestDto,Comments>().ReverseMap();
        }
    }
}