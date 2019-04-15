using Epinova.IssuuMedia;
using StructureMap;
using Xunit;
using Xunit.Abstractions;

namespace Epinova.IssuuMediaTests
{
    public class MediaRegistryTests
    {
        private readonly Container _container;
        private readonly ITestOutputHelper _output;

        public MediaRegistryTests(ITestOutputHelper output)
        {
            _output = output;
            _container = new Container(new TestableRegistry());
            _container.Configure(x => { x.AddRegistry(new MediaRegistry()); });
        }


        [Fact]
        public void AssertConfigurationIsValid()
        {
            _output.WriteLine(_container.WhatDoIHave());
            _container.AssertConfigurationIsValid();
        }


        [Fact]
        public void Getting_IMediaService_ReturnsMediaService()
        {
            var instance = _container.GetInstance<IMediaService>();

            Assert.IsType<MediaService>(instance);
        }
    }
}