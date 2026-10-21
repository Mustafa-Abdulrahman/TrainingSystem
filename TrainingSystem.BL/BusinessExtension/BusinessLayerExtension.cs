using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public static class BusinessLayerExtension
{
    public static IServiceCollection AddBusinessLayerExtensionMethod (this IServiceCollection service)
    {
        service.AddScoped<ITrackManager, TrackManager>();

        return service;
    }
}
