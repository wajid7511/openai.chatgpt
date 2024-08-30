using OpenAI.ChatGpt.Options;

namespace OpenAI.ChatGpt.Tests;

[TestClass]
public class ChatGptOptionsTests
{
    [TestInitialize]
    public void Initialize()
    {
        //
    }
    [TestCleanup]
    public void Cleanup()
    {
        //
    }
    [TestMethod]
    public void ChatGptOptionsTest()
    {
        //Arrange 
        var chatGptOptions = new ChatGptOptions();

        //Act

        //Act
        Assert.AreEqual("OpenAI:ChatGPT", ChatGptOptions.CONFIG_PATH);
        Assert.AreEqual("", chatGptOptions.CompletionsUrl);
        Assert.AreEqual("", chatGptOptions.ApiKey);
        Assert.AreEqual("", chatGptOptions.Model);
        Assert.AreEqual("", chatGptOptions.Role);
    }

}
