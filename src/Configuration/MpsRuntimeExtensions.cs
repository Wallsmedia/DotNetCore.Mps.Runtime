//   \\      /\  /\\
//  o \\ \  //\\// \\
//  |  \//\//       \\
// Copyright (c) i-Wallsmedia 2023. All rights reserved.

// Licensed to the .NET Foundation under one or more agreements.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;

namespace DotNetCore.Mps.Runtime.Configuration;

/// <summary>
/// Extension methods for registering <see cref="MpsRuntimeConfigurationProvider"/> with <see cref="IConfigurationBuilder"/>.
/// </summary>
public static class MpsRuntimeExtensions
{
    /// <summary>
    /// Adds an <see cref="IConfigurationProvider"/> that reads configuration values values from environment variables.
    /// </summary>
    /// <param name="configurationBuilder">The <see cref="IConfigurationBuilder"/> to add to.</param>
    /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
    public static IConfigurationBuilder AddMpsRuntime(this IConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Add(new MpsRuntimeConfigurationSource());
        return configurationBuilder;
    }

    /// <summary>
    /// Adds an <see cref="IConfigurationProvider"/> that configuration runtime values values from environment variables
    /// into a specified section/prefix.
    /// </summary>
    /// <param name="configurationBuilder">The <see cref="IConfigurationBuilder"/> to add to.</param>
    /// <param name="configiratioSection">The configuration section.</param>
    /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
    public static IConfigurationBuilder AddMpsRuntime(
        this IConfigurationBuilder configurationBuilder,
        string configiratioSection)
    {
        configiratioSection = configiratioSection ?? throw new ArgumentNullException(nameof(configiratioSection));
        var options = new MpsRuntimeConfigurationOptions { Section = configiratioSection };
        configurationBuilder.Add(new MpsRuntimeConfigurationSource(options));
        return configurationBuilder;
    }

    /// <summary>
    /// Adds an <see cref="IConfigurationProvider"/> that configuration runtime values values from environment variables
    /// into a specified section/prefix.
    /// </summary>
    /// <param name="configurationBuilder">The <see cref="IConfigurationBuilder"/> to add to.</param>
    /// <param name="productAssembly">The <see cref="AssemblyProductAttribute"/> will be used as Microservice name.</param>
    /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
    public static IConfigurationBuilder AddMpsRuntimeWithMicroServiceName(
        this IConfigurationBuilder configurationBuilder,
        Assembly productAssembly)
    {
        productAssembly = productAssembly ?? throw new ArgumentNullException(nameof(productAssembly));
        var options = new MpsRuntimeConfigurationOptions { MpsAssembly = productAssembly };
        configurationBuilder.Add(new MpsRuntimeConfigurationSource(options));
        return configurationBuilder;
    }

    /// <summary>
    /// Adds an <see cref="IConfigurationProvider"/> that configuration runtime values values from environment variables
    /// into a specified section/prefix.
    /// </summary>
    /// <param name="configurationBuilder">The <see cref="IConfigurationBuilder"/> to add to.</param>
    /// <param name="microserviceName">The microservice name.</param>
    /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
    public static IConfigurationBuilder AddMpsRuntimeWithMicroServiceName(
        this IConfigurationBuilder configurationBuilder,
        string microserviceName)
    {
        microserviceName = microserviceName ?? throw new ArgumentNullException(nameof(microserviceName));
        var options = new MpsRuntimeConfigurationOptions { MicroserviceName = microserviceName };
        configurationBuilder.Add(new MpsRuntimeConfigurationSource(options));
        return configurationBuilder;
    }

    /// <summary>
    /// Adds an <see cref="IConfigurationProvider"/> that reads configuration runtime values from environment variables.
    /// </summary>
    /// <param name="builder">The <see cref="IConfigurationBuilder"/> to add to.</param>
    /// <param name="configureSource">Configures the source.</param>
    /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
    public static IConfigurationBuilder AddMpsRuntime(this IConfigurationBuilder configurationBuilder, Action<MpsRuntimeConfigurationOptions> configureSource)
    {
        var options = new MpsRuntimeConfigurationOptions();
        configureSource(options);
        configurationBuilder.Add(new MpsRuntimeConfigurationSource(options));
        return configurationBuilder;
    }
}