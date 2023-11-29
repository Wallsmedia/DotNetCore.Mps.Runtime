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
/// Represents environment variables as an <see cref="IConfigurationSource"/>.
/// </summary>
public class MpsRuntimeConfigurationSource : IConfigurationSource
{
    private readonly MpsRuntimeConfigurationOptions _mpsRuntimeConfigurationOptions;

    /// <summary>
    /// Initialize Configuration Source.
    /// </summary>
    public MpsRuntimeConfigurationSource()
    {
        _mpsRuntimeConfigurationOptions = new MpsRuntimeConfigurationOptions();
    }

    /// <summary>
    /// Initialize Configuration Source.   
    /// </summary>
    /// <param name="mpsRuntimeConfigurationOptions"> Configuration options</param>
    /// <exception cref="ArgumentNullException"></exception>
    public MpsRuntimeConfigurationSource(MpsRuntimeConfigurationOptions mpsRuntimeConfigurationOptions)
    {
        _mpsRuntimeConfigurationOptions = mpsRuntimeConfigurationOptions ?? throw new ArgumentNullException(nameof(mpsRuntimeConfigurationOptions));
    }

    /// <summary>
    /// Builds the <see cref="MpsRuntimeConfigurationProvider"/> for this source.
    /// </summary>
    /// <param name="builder">The <see cref="IConfigurationBuilder"/>.</param>
    /// <returns>A <see cref="MpsRuntimeConfigurationProvider"/></returns>
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {

        if (_mpsRuntimeConfigurationOptions.MpsAssembly != null)
        {
            var assemblyProductAttribute = _mpsRuntimeConfigurationOptions.MpsAssembly.GetCustomAttribute(typeof(AssemblyProductAttribute)) as AssemblyProductAttribute;
            if (assemblyProductAttribute != null)
            {
                _mpsRuntimeConfigurationOptions.MicroserviceName = assemblyProductAttribute.Product;
            }
        }
        return new MpsRuntimeConfigurationProvider(_mpsRuntimeConfigurationOptions);
    }
}
