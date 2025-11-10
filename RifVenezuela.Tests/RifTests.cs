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

    [Fact]
    public void Constructor_WithInvalidKind_ShouldThrowArgumentOutOfRangeException()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => new Rif('X', 12345678));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(100_000_000)]
    public void Constructor_WithInvalidIdentifier_ShouldThrowArgumentOutOfRangeException(int identifier)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => new Rif('J', identifier));
    }

    [Theory]
    [InlineData('G', 20000004, "G-20000004-0")]
    [InlineData('G', 20000003, "G-20000003-1")]
    [InlineData('G', 20000008, "G-20000008-2")]
    [InlineData('G', 20000002, "G-20000002-3")]
    [InlineData('G', 20000007, "G-20000007-4")]
    [InlineData('G', 20000001, "G-20000001-5")]
    [InlineData('G', 20000006, "G-20000006-6")]
    [InlineData('G', 20000014, "G-20000014-7")]
    [InlineData('G', 20000005, "G-20000005-8")]
    [InlineData('G', 20000013, "G-20000013-9")]
    [InlineData('J', 00343994, "J-00343994-0")]
    [InlineData('J', 30399491, "J-30399491-1")]
    [InlineData('J', 30438135, "J-30438135-2")]
    [InlineData('J', 30468971, "J-30468971-3")]
    [InlineData('J', 00002950, "J-00002950-4")]
    [InlineData('J', 07013380, "J-07013380-5")]
    [InlineData('J', 09512460, "J-09512460-6")]
    [InlineData('J', 00008933, "J-00008933-7")]
    [InlineData('J', 30142060, "J-30142060-8")]
    [InlineData('J', 00006372, "J-00006372-9")]
    public void Constructor_WithValidTypeAndNumber_CreatesRif(char type, int identifier, string expectedRif) 
    {
        var rif = new Rif(type, identifier);
        rif.ToString().ShouldBe(expectedRif);
    }
}