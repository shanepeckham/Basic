using doLittle.Configuration;
using doLittle.Execution;
using doLittle.StructureMap;
using Microsoft.AspNetCore.Http;
using StructureMap;
using IContainer = doLittle.Execution.IContainer;
using Container = doLittle.StructureMap.Container;

namespace Web
{
    public class ContainerCreator : ICanCreateContainer
    {
        public IContainer CreateContainer()
        {
            var structureMap = new StructureMap.Container(_ => 
            {
                _.For<IHttpContextAccessor>().Use<HttpContextAccessor>();
            });
            var container = new Container(structureMap);
            return container;
        }
    }
}