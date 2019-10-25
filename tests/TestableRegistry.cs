using EPiServer.Logging;
using Moq;
using StructureMap;

namespace Epinova.IssuuMediaTests
{
    internal class TestableRegistry : Registry
    {
        public TestableRegistry()
        {
            For<ILogger>().Use(new Mock<ILogger>().Object);
        }
    }
}
