using AutoMapper;
using CollaboratorRegisterApi.Models.Entities;
using CollaboratorRegisterApi.Models.Requests;
using CollaboratorRegisterApi.Models.Responses;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CollaboratorRegisterApi.Profiles
{
    public class CollaboratorProfile : Profile
    {
        public CollaboratorProfile()
        {
            CreateMap<CollaboratorAddRequest, Collaborator>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.MapFrom(s => Criptografy(s.Password)));

            CreateMap<CollaboratorUpdateRequest, Collaborator>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(s => Criptografy(s.Password)));

            CreateMap<CollaboratorPhoneAddRequest, CollaboratorPhone>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CollaboratorId, opt => opt.Ignore());

            CreateMap<IEnumerable<Collaborator>, CollaboratorGetResponse>()
                 .ForPath(dest => dest.Collaborators, opt => opt.MapFrom(src => src.Select(c => new CollaboratorResponse
                 {
                     Id = c.Id,
                     Name = c.Name,
                     Mail = c.Mail,
                     PlateNumber = c.PlateNumber,
                     LeaderId = c.LeaderId,
                     Phones = c.Phones == null || !c.Phones.Any() ? null : c.Phones.Select(p => new CollaboratorPhoneResponse
                     {
                         Description = p.Description,
                         Number = p.Number
                     })
                 })))
                .ForMember(dest => dest.StatusCode, opt => opt.MapFrom(s => StatusCodes.Status200OK))
                .ForMember(dest => dest.ErrorMessage, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedId, opt => opt.Ignore());

            CreateMap<CollaboratorPhone, CollaboratorPhoneResponse>();
        }

        //ToDo: esse metodo faria sentido jogar em um base project pra gerar um package nuget
        public string Criptografy(string password)
        {
            var md5Hash = MD5.Create();
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));

            return sBuilder.ToString();
        }
    }
}
