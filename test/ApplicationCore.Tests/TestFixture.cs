using System.Reflection;
using AutoMapper;
using Blog.ApplicationCore.Common.Mapping;

namespace ApplicationCore.Tests
{
    public class TestFixture
    {

        public TestFixture()
        {
            var config = new MapperConfiguration(cfg => {
                
                cfg.AddProfiles(Assembly.GetAssembly(typeof(PostMappingProfile)));
            });

            Mapper = new Mapper(config);
        }

        public IMapper Mapper { get; }
    }
}