using Core.Application.Abstractions;
using Core.Application.DTOs;
using Mails.Application.DTOs;

namespace Mails.Application.Caches;

public interface IMailsMemoryCache : IBaseCache<GetMailDto>
{
}
