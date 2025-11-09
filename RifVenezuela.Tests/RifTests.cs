using Shouldly;

namespace RifVenezuela.Tests;

public class RifTests
{
    [Fact]
    public void Constructor_WithoutArguments_ShouldNotThrowException()
    {
        var action = () => new Rif();

        action.ShouldNotThrow();
    }
}