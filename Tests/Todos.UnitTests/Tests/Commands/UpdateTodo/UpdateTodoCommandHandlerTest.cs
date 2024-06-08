using System.Linq.Expressions;
using AutoFixture;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Auth.Application.Abstractions.Service;
using Core.Tests;
using Core.Tests.Attributes;
using Core.Tests.Fixtures;
using Core.Tests.Helpers;
using Core.Users.Domain.Enums;
using MediatR;
using Moq;
using Products.Applications.Caches;
using Products.Applications.DTOs;
using Products.Applications.Handlers.Commands.UpdateProduct;
using Products.Applications.Handlers.Queries.GetProducts;
using Products.Domain;
using Xunit.Abstractions;

namespace Todos.UnitTests.Tests.Commands.UpdateTodo;

public class UpdateProductCommandHandlerTest : RequestHandlerTestBase<UpdateProductCommand, GetProductDto>
{
    private readonly Mock<IBaseWriteRepository<Product>> _productsMok = new();

    private readonly Mock<ICurrentUserService> _currentServiceMok = new();

    private readonly ICleanProductsCacheService _cleanProductsCacheService;

    private readonly IMapper _mapper;

    public UpdateProductCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(GetProductsQuery).Assembly).Mapper;
        _cleanProductsCacheService = new CleanProductsCacheService(new Mock<ProductsMemoryCache>().Object, new Mock<ProductsListMemoryCache>().Object, new Mock<ProductsCountMemoryCache>().Object);
    }

    protected override IRequestHandler<UpdateProductCommand, GetProductDto> CommandHandler =>
        new UpdateProductCommandHandler(_productsMok.Object, _mapper, _currentServiceMok.Object, _cleanProductsCacheService);

    [Theory, FixtureInlineAutoData]
    public async Task Should_BeValid_When_GetProductsByAdmin(UpdateProductCommand command, Guid userId)
    {
        // arrange
        _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);
        _currentServiceMok.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(true);

        var product = TestFixture.Build<Product>().Create();
        product.OwnerId = GuidHelper.GetNotEqualGiud(userId);
        _productsMok.Setup(
            p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Product, bool>>>(), default)
        ).ReturnsAsync(product);

        // act and assert
        await AssertNotThrow(command);
    }

    [Theory, FixtureInlineAutoData]
    public async Task Should_BeValid_When_GetProductsByClient(UpdateProductCommand command, Guid userId)
    {
        // arrange
        _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);
        _currentServiceMok.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Client)).Returns(true);

        var product = TestFixture.Build<Product>().Create();
        product.OwnerId = userId;
        _productsMok.Setup(
            p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Product, bool>>>(), default)
        ).ReturnsAsync(product);

        // act and assert
        await AssertNotThrow(command);
    }

    [Theory, FixtureInlineAutoData]
    public async Task Should_ThrowForbidden_When_GetProductsWithOtherOwnerByClient(UpdateProductCommand command, Guid userId)
    {
        // arrange
        _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);
        _currentServiceMok.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Client)).Returns(true);

        var product = TestFixture.Build<Product>().Create();
        product.OwnerId = GuidHelper.GetNotEqualGiud(userId);

        _productsMok.Setup(
            p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Product, bool>>>(), default)
        ).ReturnsAsync(product);

        // act and assert
        await AssertThrowForbiddenFound(command);
    }

    [Theory, FixtureInlineAutoData]
    public async Task Should_ThrowNotFound_When_ProductNotFound(UpdateProductCommand command)
    {
        // arrange

        _productsMok.Setup(
            p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Product, bool>>>(), default)
        ).ReturnsAsync(null as Product);

        // act and assert
        await AssertThrowNotFound(command);
    }
}