//   \\      /\  /\\
//  o \\ \  //\\// \\
//  |  \//\//       \\
// Copyright (c) i-Wallsmedia 2024. All rights reserved.

// Licensed to the .NET Foundation under one or more agreements.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCore.Mps.Runtime.Configuration;

public class MpsLifetimeEventsHostedService : IHostedService
#if NET8_0_OR_GREATER
    , IHostedLifecycleService
#endif 
{
    private readonly MpsRuntime _mpsRuntime;
    private readonly ILogger _logger;
    private readonly IHostApplicationLifetime _appLifetime;

    public MpsLifetimeEventsHostedService(
        ILogger<MpsLifetimeEventsHostedService> logger,
        MpsRuntime mpsRuntime,
        IHostApplicationLifetime appLifetime)
    {
        _mpsRuntime = mpsRuntime;
        _logger = logger;
        _appLifetime = appLifetime;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _appLifetime.ApplicationStarted.Register(OnStarted);
        _appLifetime.ApplicationStopping.Register(OnStopping);
        _appLifetime.ApplicationStopped.Register(OnStopped);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private void OnStarted()
    {
        _logger.LogInformation($"ApplicationStarted - ServiceName: '{_mpsRuntime.MicroserviceName}'");
        _logger.LogInformation("LogInformation " +
                              $"Starting ServiceName: '{_mpsRuntime.MicroserviceName}'', WebHostEnvironment : '{_mpsRuntime.MpsEnvironment}',"
                              + $" IsEnvDefined : {_mpsRuntime.IsMpsConfigurationValid}, Docker: '{_mpsRuntime.IsDocker}', Linux: '{_mpsRuntime.IsLinux}',"
                              + $" Bus : '{_mpsRuntime.ServiceBusName}' , MachineName : '{_mpsRuntime.MachineName}'");
    }

    private void OnStopping()
    {
        _logger.LogInformation($"ApplicationStopping - ServiceName: '{_mpsRuntime.MicroserviceName}'");
    }

    private void OnStopped()
    {
        _logger.LogInformation($"ApplicationStopped - ServiceName: '{_mpsRuntime.MicroserviceName}'");
    }

    public Task StartedAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"LifetimeEventsHostedService - StartedAsync: '{_mpsRuntime.MicroserviceName}'");
        return Task.CompletedTask;
    }

    public Task StartingAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"LifetimeEventsHostedService - StartingAsync: '{_mpsRuntime.MicroserviceName}'");
        return Task.CompletedTask;
    }

    public Task StoppedAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"LifetimeEventsHostedService - StoppedAsync: '{_mpsRuntime.MicroserviceName}'");
        return Task.CompletedTask;
    }

    public Task StoppingAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"LifetimeEventsHostedService - StoppingAsync: '{_mpsRuntime.MicroserviceName}'");
        return Task.CompletedTask;
    }
}
