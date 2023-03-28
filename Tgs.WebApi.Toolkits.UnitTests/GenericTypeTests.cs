using Tgs.WebApi.Toolkits.ViewModels;

namespace Tgs.WebApi.Toolkits.UnitTests;

public class GenericTypeTests
{
    [Test]
    public void Check_GenericType()
    {
        var expected = typeof(SuccessViewModel<>);

        var actual = typeof(SuccessViewModel<int>);

        Assert.AreEqual(expected, actual.GetGenericTypeDefinition());
    }
}
