using Core.Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Tests.Integration;

internal abstract class TestBase
{
    private IServiceScope scope;
    protected readonly HttpClient testClient;
    protected readonly AppDbContext context;

    public TestBase()
    {
        var appFactory = new TestWebFactory<Program>();
        testClient = appFactory.CreateClient();
        
        scope = appFactory.Services.CreateScope();
        context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated();
    }
    
    [OneTimeTearDown]
    public void TearDown()
    {
        scope.Dispose();
    }
}