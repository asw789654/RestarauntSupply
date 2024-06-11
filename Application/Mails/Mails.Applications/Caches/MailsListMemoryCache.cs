using Core.Application.BaseRealizations;
using Core.Application.DTOs;

namespace Mails.Applications.Caches;

public class MailsListMemoryCache : BaseCache<BaseListDto<GetMailsDto>>;