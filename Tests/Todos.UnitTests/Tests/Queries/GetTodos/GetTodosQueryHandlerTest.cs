using System.Linq.Expressions;
using AutoFixture;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.DTOs;
using Core.Auth.Application.Abstractions.Service;
using Core.Tests;
using Core.Tests.Fixtures;
using Core.Users.Domain.Enums;
using MediatR;
using Moq;
using Products.Applications.Caches;
using Products.Applications.DTOs;
using Products.Applications.Handlers.Queries.GetProducts;
using Products.Domain;
using Xunit.Abstractions;

namespace Todos.UnitTests.Tests.Queries.GetTodos;

public class GetProductsQueryHandlerTest : RequestHandlerTestBase<GetProductsQuery, BaseListDto<GetProductDto>>
{
    private readonly Mock<IBaseReadRepository<Product>> _productsMok = new();

    private readonly Mock<ICurrentUserService> _currentServiceMok = new();

    private readonly Mock<ProductsListMemoryCache> _mockProductsMemoryCache = new();

    private readonly IMapper _mapper;

    public GetProductsQueryHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(GetProductsQuery).Assembly).Mapper;
    }

    protected override IRequestHandler<GetProductsQuery, BaseListDto<GetProductDto>> CommandHandler =>
        new GetProductsQueryHandler(_productsMok.Object, _currentServiceMok.Object, _mapper, _mockProductsMemoryCache.Object);


    [Fact]
    public async Task Should_BeValid_When_GetProductsByAdmin()
    {
        // arrange
        var userId = Guid.NewGuid();
        _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);

        var query = new GetProductsQuery();

        var products = TestFixture.Build<Product>().CreateMany(10).ToArray();
        var count = products.Length;

        _currentServiceMok.Setup(
                p => p.UserInRole(ApplicationUserRolesEnum.Admin))
            .Returns(true);

        _productsMok.Setup(
            p => p.AsAsyncRead().ToArrayAsync(It.IsAny<Expression<Func<Product, bool>>>(), default)
        ).ReturnsAsync(products);

        _productsMok.Setup(
            p => p.AsAsyncRead().CountAsync(It.IsAny<Expression<Func<Product, bool>>>(), default)
        ).ReturnsAsync(count);

        // act and assert
        await AssertNotThrow(query);
    }

    [Fact]
    public async Task Should_BeValid_When_GetProductsByClient()
    {
        // arrange
        var userId = Guid.NewGuid();
        _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);

        var query = new GetProductsQuery();

        var products = TestFixture.Build<Product>().CreateMany(10).ToArray();
        var count = products.Length;

        _currentServiceMok.Setup(
                p => p.UserInRole(ApplicationUserRolesEnum.Admin))
            .Returns(false);

        _productsMok.Setup(
            p => p.AsAsyncRead().ToArrayAsync(It.IsAny<Expression<Func<Product, bool>>>(), default)
        ).ReturnsAsync(products);

        _productsMok.Setup(
            p => p.AsAsyncRead().CountAsync(It.IsAny<Expression<Func<Product, bool>>>(), default)
        ).ReturnsAsync(count);

        // act and assert
        await AssertNotThrow(query);
    }
}