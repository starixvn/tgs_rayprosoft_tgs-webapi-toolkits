using Microsoft.Extensions.Logging;

namespace Tgs.WebApi.Toolkits.UnitTests;

public class LogTests
{
    [Test]
    public async Task HttpContent_Log測試()
    {
        var sut = new FakeLogger();

        var content = new StringContent("test");

        sut.LogInformation("{content}", await content.ReadAsStringAsync());

        Assert.AreEqual("FormattedLogValues", sut.StateType);
        Assert.AreEqual("test", sut.StateText);
    }

    [Test]
    public void HttpRequestMessage_Log測試()
    {
        var sut = new FakeLogger();

        var request = new HttpRequestMessage();

        sut.LogInformation("{request}", request);

        Assert.AreEqual("FormattedLogValues", sut.StateType);
        Assert.IsNotEmpty(sut.StateText);
    }
}
