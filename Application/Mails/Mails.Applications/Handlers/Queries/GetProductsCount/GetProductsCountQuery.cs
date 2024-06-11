using Core.Auth.Application.Attributes;
using MediatR;

namespace Mails.Applications.Handlers.Queries.GetMailsCount;

[RequestAuthorize]
public class GetMailsCountQuery : ListProductFilter, IRequest<int>
{

}