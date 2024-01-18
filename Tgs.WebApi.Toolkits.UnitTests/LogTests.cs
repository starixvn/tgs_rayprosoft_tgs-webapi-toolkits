using Microsoft.Extensions.Logging;
using NUnit.Framework.Legacy;

namespace Tgs.WebApi.Toolkits.UnitTests;

public class LogTests
{
    [Test]
    public async Task HttpContent_Log測試()
    {
        var sut = new FakeLogger();

        var content = new StringContent("test");

        sut.LogInformation("{content}", await content.ReadAsStringAsync());

        ClassicAssert.AreEqual("FormattedLogValues", sut.StateType);
        ClassicAssert.AreEqual("test", sut.StateText);
    }

    [Test]
    public void HttpRequestMessage_Log測試()
    {
        var sut = new FakeLogger();

        var request = new HttpRequestMessage();

        sut.LogInformation("{request}", request);

        ClassicAssert.AreEqual("FormattedLogValues", sut.StateType);
        ClassicAssert.IsNotEmpty(sut.StateText);
    }
}
