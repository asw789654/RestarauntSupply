using AutoMapper;
using Core.Application.Abstractions.Mappings;
using Core.Users.Domain;

namespace Users.Application.Dtos;

public class GetUserMailDto : IMapFrom<ApplicationUser>
{
    public Guid ApplicationUserId { get; set; }
    
    public string Login { get; set; } = default!;

    public string? MailAddress { get; set; } = default!;

    public void CreateMap(Profile profile)
    {
        profile.CreateMap<ApplicationUser, GetUserMailDto>();
    }
}