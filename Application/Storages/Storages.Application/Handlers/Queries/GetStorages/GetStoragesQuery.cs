using Core.Application.DTOs;
using Core.Auth.Application.Attributes;
using MediatR;
using Storages.Application.DTOs;
using Storages.Application.Handlers.Queries;

namespace Storages.Application.Handlers.Queries.GetStorages;

[RequestAuthorize]
public class GetStoragesQuery : ListStorageFilter, IBasePaginationFilter, IRequest<BaseListDto<GetStorageDto>>
{
    public int? Limit { get; init; }

    public int? Offset { get; init; }
}