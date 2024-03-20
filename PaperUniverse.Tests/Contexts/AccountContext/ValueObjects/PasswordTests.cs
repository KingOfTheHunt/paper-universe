using PaperUniverse.Core.Contexts.AccountContext.ValueObjects;

namespace PaperUniverse.Tests.Contexts.AccountContext.ValueObjects;

[TestClass]
public class PasswordTests
{
    [TestMethod]
    public void ShouldReturnFalseWhenPasswordIsValid()
    {
        var password = new Password("localhost");
        var result = password.Challenge("locahost");

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void ShouldReturnTrueWhenPasswordIsValid()
    {
        var password = new Password("123456");
        var result = password.Challenge("123456");

        Assert.IsTrue(result);
    }
}