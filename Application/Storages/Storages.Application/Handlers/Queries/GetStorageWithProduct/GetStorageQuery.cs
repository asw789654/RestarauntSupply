using Core.Auth.Application.Attributes;
using MediatR;
using Storages.Application.DTOs;

namespace Storages.Application.Handlers.Queries.GetStorageWithProduct;

[RequestAuthorize]
public class GetStorageQuery : IRequest<GetStorageDto>
{
    public int StorageId { get; init; }
}