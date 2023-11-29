//   \\      /\  /\\
//  o \\ \  //\\// \\
//  |  \//\//       \\
// Copyright (c) i-Wallsmedia 2023. All rights reserved.

// Licensed to the .NET Foundation under one or more agreements.
// See the LICENSE file in the project root for more information.

using System.Reflection;

namespace DotNetCore.Mps.Runtime.Configuration;

public class MpsRuntimeConfigurationOptions
{
    /// <summary>
    /// A Microservice assembly; it's used for accessing the <see cref="AssemblyProductAttribute"/>
    /// which is used as MicroserviceName for a runtime configuration.
    /// </summary>
    public Assembly MpsAssembly { get; set; }

    /// <summary>
    /// Set the valid list of configurations.
    /// If it is not set the default list will be used 
    /// </summary>
    public string[] ValidMpsConfigs { get; set; } = MpsRuntimeConfigurationProvider.ValidMpsConfigs;

    /// <summary>
    /// A prefix used as name of the section  variables.
    /// </summary>
    public string MicroserviceName { get; set; } = MpsRuntimeConfigurationProvider.DefaultMicroserviceName;

    /// <summary>
    /// A prefix used as name of the section  variables.
    /// </summary>
    public string Section { get; set; } = MpsRuntimeConfigurationProvider.DefaultSection;

    /// <summary>
    /// A prefix used as name of the section  variables.
    /// </summary>
    public string MpsEnvironmentName { get; set; } = MpsRuntimeConfigurationProvider.EnvMpsEnvironment;
}
