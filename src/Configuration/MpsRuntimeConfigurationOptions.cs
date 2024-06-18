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
    /// The default list is  "sbx", "dev", "uat", "qat", "stg", "pdn", "prd" 
    /// </summary>
    public string[] ValidMpsConfigs { get; set; } = MpsRuntimeConfigurationProvider.ValidMpsConfigs;

    /// <summary>
    /// A microservice name. The default name is 'mps-microservice-api'.
    /// </summary>
    public string MicroserviceName { get; set; } = MpsRuntimeConfigurationProvider.DefaultMicroserviceName;

    /// <summary>
    /// A prefix used as name of the section  variables. The default value is 'mps'.
    /// </summary>
    public string Section { get; set; } = MpsRuntimeConfigurationProvider.DefaultSection;

    /// <summary>
    /// An environment name. The default name is 'microservice_environment'.
    /// </summary>
    public string MpsEnvironmentName { get; set; } = MpsRuntimeConfigurationProvider.EnvMpsEnvironment;
}
