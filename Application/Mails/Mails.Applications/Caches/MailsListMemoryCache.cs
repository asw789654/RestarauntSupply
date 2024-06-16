using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Mails.Applications.DTOs;

namespace Mails.Applications.Caches;

public class MailsListMemoryCache : BaseCache<BaseListDto<GetMailDto>>;