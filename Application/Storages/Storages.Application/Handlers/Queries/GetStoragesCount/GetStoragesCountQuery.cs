using Core.Auth.Application.Attributes;
using MediatR;
using Storages.Application.Handlers.Queries;

namespace Storages.Application.Handlers.Queries.GetStoragesCount;

[RequestAuthorize]
public class GetStoragesCountQuery : ListStorageFilter, IRequest<int>
{

}