//   \\      /\  /\\
//  o \\ \  //\\// \\
//  |  \//\//       \\
// Copyright (c) i-Wallsmedia 2023. All rights reserved.

// Licensed to the .NET Foundation under one or more agreements.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace DotNetCore.Mps.Runtime.Configuration;

/// <summary>
/// An environment runtime variable based <see cref="ConfigurationProvider"/>.
/// </summary>
public class MpsRuntimeConfigurationProvider : ConfigurationProvider
{
    public static readonly string EnvDotnetRunningInContainer = "DOTNET_RUNNING_IN_CONTAINER";
    public static readonly string EnvMpsEnvironment = "microservice_environment";
    public static readonly string[] ValidMpsConfigs = new string[] { "sbx", "dev", "uat", "qat", "stg", "pdn", "prd" };
    public static readonly string DefaultMpsEnvironment = "dev";
    public static readonly string DefaultSection = "mps";
    public static readonly string DefaultMicroserviceName = "mps-microservice-api";
    private readonly MpsRuntimeConfigurationOptions _mpsRuntimeConfigurationOptions;

    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    public MpsRuntimeConfigurationProvider()
    {
        _mpsRuntimeConfigurationOptions = new MpsRuntimeConfigurationOptions();
    }

    /// <summary>
    /// Initializes a new instance with the specified section.
    /// </summary>
    /// <param name="mpsRuntimeConfigurationOptions">Mps runtime configuration options.</param>
    public MpsRuntimeConfigurationProvider(MpsRuntimeConfigurationOptions mpsRuntimeConfigurationOptions)
    {
        _mpsRuntimeConfigurationOptions = mpsRuntimeConfigurationOptions ?? throw new ArgumentNullException(nameof(mpsRuntimeConfigurationOptions));
    }

    /// <summary>
    /// Loads the runtime variables.
    /// </summary>
    public override void Load()
    {
        var data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var section = string.IsNullOrEmpty(_mpsRuntimeConfigurationOptions.Section) ? string.Empty : _mpsRuntimeConfigurationOptions.Section + ":";

        var mpsEnv = Environment.GetEnvironmentVariable(_mpsRuntimeConfigurationOptions.MpsEnvironmentName)?.ToLower();
        var IsMpsConfigurationValid = _mpsRuntimeConfigurationOptions.ValidMpsConfigs.Contains(mpsEnv);
        string MpsEnvironment = IsMpsConfigurationValid ? mpsEnv : DefaultMpsEnvironment;
        var MachineName = Environment.MachineName;
        string ServiceBusName = IsMpsConfigurationValid ? MpsEnvironment : MachineName;
        bool IsLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        bool.TryParse(Environment.GetEnvironmentVariable(EnvDotnetRunningInContainer), out bool IsDocker);

        data[$"{nameof(MpsRuntime)}:{nameof(_mpsRuntimeConfigurationOptions.MicroserviceName)}"] = _mpsRuntimeConfigurationOptions.MicroserviceName;
        data[$"{nameof(MpsRuntime)}:{nameof(MpsEnvironment)}"] = MpsEnvironment;
        data[$"{nameof(MpsRuntime)}:{nameof(IsDocker)}"] = IsDocker.ToString().ToLower();
        data[$"{nameof(MpsRuntime)}:{nameof(IsMpsConfigurationValid)}"] = IsMpsConfigurationValid.ToString().ToLower();
        data[$"{nameof(MpsRuntime)}:{nameof(MachineName)}"] = MachineName;
        data[$"{nameof(MpsRuntime)}:{nameof(ServiceBusName)}"] = ServiceBusName;
        data[$"{nameof(MpsRuntime)}:{nameof(IsLinux)}"] = IsLinux.ToString().ToLower();

        data[$"{section}msname"] = _mpsRuntimeConfigurationOptions.MicroserviceName;
        data[$"{section}env"] = MpsEnvironment;
        data[$"{section}environment"] = MpsEnvironment;
        data[$"{section}docker"] = IsDocker.ToString().ToLower();
        data[$"{section}host"] = MachineName;
        data[$"{section}bus"] = ServiceBusName;
        data[$"{section}linux"] = IsLinux.ToString().ToLower(); ;

        Data = data;
    }

}
