using System;
using System.IO;
using doLittle.Applications;
using doLittle.Configuration;
using doLittle.Serialization;
using doLittle.Web.Read;
using Microsoft.AspNetCore.Routing;

namespace Web
{
    public class Configurator : ICanConfigure
    {
        public void Configure(IConfigure configure)
        {
            var basePath = "./EventStore";
            var entitiesPath = Path.Combine("./", "Entities");
            var eventsPath = Path.Combine(basePath, "Events");
            var eventSequenceNumbersPath = Path.Combine(basePath, "EventSequenceNumbers");
            var eventProcessorsStatePath = Path.Combine(basePath, "EventProcessors");
            var eventSourceVersionsPath = Path.Combine(basePath, "EventSourceVersions");
            
            configure
                .Application("Basic", a => a.Structure(s => s
                        .Domain("Domain.{BoundedContext}.-{Module}.-{Feature}.^{SubFeature}*")
                        .Events("Events.{BoundedContext}.-{Module}.-{Feature}.^{SubFeature}*")
                        .Read("Read.{BoundedContext}.-{Module}.-{Feature}.^{SubFeature}*")
                        .Read("TextAnalytics.{BoundedContext}.-{Module}.-{Feature}.^{SubFeature}*")
                        .Frontend("Web.{BoundedContext}.-{Module}.-{Feature}.^{SubFeature}*")
                ))
                .Events(e =>
                {
                    e.EventStore.UsingFiles(eventsPath);
                    e.EventSequenceNumbers.UsingFiles(eventSequenceNumbersPath);
                    e.EventProcessorStates.UsingFiles(eventProcessorsStatePath);
                    e.EventSourceVersions.UsingFiles(eventSourceVersionsPath);
                })

                .Serialization
                    .UsingJson()

                .DefaultStorage
                    .UsingFiles(entitiesPath)
                    
                .Frontend
                    .Web(w =>
                    {
                        w.AsSinglePageApplication();
                        w.PathsToNamespaces.Clear();

                        var baseNamespace = "Web";

                        var @namespace = string.Format("{0}.**.", baseNamespace);

                        w.PathsToNamespaces.Add("**/", @namespace);
                        w.PathsToNamespaces.Add("/**/", @namespace);
                        w.PathsToNamespaces.Add("", baseNamespace);

                        w.NamespaceMapper.Add($"{baseNamespace}.**.", "Concepts.**.");
                        w.NamespaceMapper.Add($"{baseNamespace}.**.", "Domain.**.");
                        w.NamespaceMapper.Add($"{baseNamespace}.**.", "Read.**.");
                        w.NamespaceMapper.Add($"{baseNamespace}.**.", "Events.**.");
                        w.NamespaceMapper.Add($"{baseNamespace}.**.", $"{baseNamespace}.**.");
                    });

        }
    }
}