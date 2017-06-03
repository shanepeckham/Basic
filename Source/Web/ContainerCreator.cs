using doLittle.Configuration;
using doLittle.Execution;
using doLittle.StructureMap;

namespace Web
{
    public class ContainerCreator : ICanCreateContainer
    {
        public IContainer CreateContainer()
        {
            var structureMap = new StructureMap.Container();
            var container = new Container(structureMap);
            return container;
        }
    }
}