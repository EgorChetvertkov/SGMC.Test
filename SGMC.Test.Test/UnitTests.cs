using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Moq;

using SGMC.Test.Application.Nomenclatures.Create;
using SGMC.Test.DB;
using SGMC.Test.DB.Entities;

namespace SGMC.Test.Test;

public class UnitTests
{
    //NOTE : Время подходит к концу, сделал пару тестов
    [Fact]
    public async Task Handle_ValidRequest()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var dbContext = new ApplicationDBContext(options);

        var logger = new Mock<ILogger<CreateNomenclatureRequestHandler>>();
        var handler = new CreateNomenclatureRequestHandler(logger.Object, dbContext);
        var request = new CreateNomenclatureRequest("TestNomenclature", 100, []);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsError);
    }

    [Fact]
    public async Task Handle_InvalidRequest()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var dbContext = new ApplicationDBContext(options);

        using (var context = new ApplicationDBContext(options))
        {
            context.Nomenclatures.Add(new Nomenclature { Name = "TestNomenclature", Price = 100 });
            context.SaveChanges();
        }

        var logger = new Mock<ILogger<CreateNomenclatureRequestHandler>>();
        var handler = new CreateNomenclatureRequestHandler(logger.Object, dbContext);
        var request = new CreateNomenclatureRequest("TestNomenclature", 100, []);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsError);
    }
}