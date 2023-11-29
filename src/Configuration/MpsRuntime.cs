//   \\      /\  /\\
//  o \\ \  //\\// \\
//  |  \//\//       \\
// Copyright (c) i-Wallsmedia 2023. All rights reserved.

// Licensed to the .NET Foundation under one or more agreements.
// See the LICENSE file in the project root for more information.

namespace DotNetCore.Mps.Runtime.Configuration;

/// <summary>
/// A configuration class the used to extract into it the values provided 
/// <see cref="MpsRuntimeConfigurationProvider"/> via predefined section "MpsRuntime"
/// <example>
/// 
/// var runtime = configuration.Get<MpsRuntime>(); 
/// 
/// </example>
/// </summary>
public class MpsRuntime
{
    /// <summary>
    /// Gets the value of Mps MicroService name.
    /// The of the Microservice can be provided via the <see cref="AssemblyProductAttribute"/>
    /// or directly with <see cref="MpsRuntimeExtensions"/> 
    /// </summary>
    public string MicroserviceName { get; set; }

    /// <summary>
    /// Gets the value of Mps environment runtime.
    /// Default environment name is "microservice_environment".
    /// The valid configurations and environment name can be provided with <see cref="MpsRuntimeExtensions"/>.
    /// Default value is "dev".
    /// </summary>
    public string MpsEnvironment { get; set; }

    /// <summary>
    /// Gets the flag true if MpsEnvironment it's not valid runtime environment.
    /// The valid configurations can be provided with <see cref="MpsRuntimeExtensions"/> 
    /// The default set  is  "sbx", "dev", "uat", "qat", "stg", "pdn" 
    /// </summary>
    public bool IsMpsConfigurationValid { get; set; }

    /// <summary>
    /// Gets the host machine name.
    /// </summary>
    public string MachineName { get; set; }

    /// <summary>
    /// Gets the specifics Service Bus name.
    /// </summary>
    public string ServiceBusName { get; set; }


    /// <summary>
    /// Gets the flag true if it's running  in Linux OS
    /// </summary>
    public bool IsLinux { get; set; }

    /// <summary>
    /// Gets the flags true if it's running inside of DotNet container.
    /// </summary>
    public bool IsDocker { get; set; }
}
