using Core.Auth.Application.Attributes;
using MediatR;

namespace Storages.Application.Handlers.Commands.DeleteStorage;

[RequestAuthorize]
public class DeleteStorageCommand : IRequest<Unit>
{
    public int StorageId { get; init; }
}