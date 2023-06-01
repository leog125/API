namespace TestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Token_Created()
        {
            API.Business.Security security = new();
            var result = security.CreateToken(new Entities.User { Name="test", Password="test"});
            Assert.AreNotEqual(result, string.Empty);
        }

        [Test]
        public void Invalid_Credentials()
        {
            API.Business.Security security = new();
            var result = security.CreateToken(new Entities.User { Name = "NE", Password = "NE" });
            Assert.AreEqual(result, string.Empty);
        }

    }
}