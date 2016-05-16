using AutoMapper;
using SiteZeras.Tests.Objects;

namespace SiteZeras.Tests.Data.Mapping
{
    public class TestObjectMapper
    {
        public static void MapObjects()
        {
            Mapper.CreateMap<TestModel, TestView>();
            Mapper.CreateMap<TestView, TestModel>();
        }
    }
}
