using DotNetCore.Mps.Runtime.Configuration;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System;
using System.Runtime.InteropServices;
using Xunit;

namespace Mps.Runtime.Tests;

public class RuntimeConfigurationProviderUnitTests
{
    ConfigurationBuilder _sut;
    MpsRuntimeConfigurationSource _src;
    MpsRuntimeConfigurationOptions _opt;
    public RuntimeConfigurationProviderUnitTests()
    {
        _sut = new ConfigurationBuilder();
        _opt = new MpsRuntimeConfigurationOptions();
        _src = new MpsRuntimeConfigurationSource(_opt);
        _sut.Add(_src);

    }

    [Fact]
    public void TestCommonHostRuntime()
    {
        // arrange 
        var configuration = _sut.Build();

        // act
        var mpsRuntime = configuration.GetSection(nameof(MpsRuntime)).Get<MpsRuntime>();

        // assert
        mpsRuntime.MachineName.Should().Be(Environment.MachineName);

        mpsRuntime.IsLinux.Should().Be(RuntimeInformation.IsOSPlatform(OSPlatform.Linux));

        bool.TryParse(Environment.GetEnvironmentVariable(MpsRuntimeConfigurationProvider.EnvDotnetRunningInContainer), out bool IsDocker);
        mpsRuntime.IsDocker.Should().Be(IsDocker);

        configuration[$"{nameof(mpsRuntime)}:{nameof(mpsRuntime.IsDocker)}"].Should().Be(mpsRuntime.IsDocker.ToString().ToLower());
        configuration[$"{nameof(mpsRuntime)}:{nameof(mpsRuntime.MachineName)}"].Should().Be(mpsRuntime.MachineName);
        configuration[$"{nameof(mpsRuntime)}:{nameof(mpsRuntime.IsLinux)}"].Should().Be(mpsRuntime.IsLinux.ToString().ToLower());

        configuration[$"{_opt.Section}:docker"].Should().Be(mpsRuntime.IsDocker.ToString().ToLower());
        configuration[$"{_opt.Section}:host"].Should().Be(mpsRuntime.MachineName);
        configuration[$"{_opt.Section}:linux"].Should().Be(mpsRuntime.IsLinux.ToString().ToLower());
    }

    [Theory]
    [InlineData("WRONG")]
    [InlineData(null)]
    public void TestMpsRuntimeDefault(string env)
    {
        // arrange 
        var expected = MpsRuntimeConfigurationProvider.DefaultMpsEnvironment;
        Environment.SetEnvironmentVariable(MpsRuntimeConfigurationProvider.EnvMpsEnvironment, env);

        var configuration = _sut.Build();

        // act
        var mpsRuntime = configuration.GetSection(nameof(MpsRuntime)).Get<MpsRuntime>();

        // assert
        mpsRuntime.MpsEnvironment.Should().Be(expected);
        mpsRuntime.IsMpsConfigurationValid.Should().BeFalse();
        mpsRuntime.ServiceBusName.Should().Be(Environment.MachineName);

        configuration[$"{nameof(mpsRuntime)}:{nameof(mpsRuntime.MpsEnvironment)}"].Should().Be(expected);
        configuration[$"{nameof(mpsRuntime)}:{nameof(mpsRuntime.IsMpsConfigurationValid)}"].Should().Be("false");
        configuration[$"{nameof(mpsRuntime)}:{nameof(mpsRuntime.ServiceBusName)}"].Should().Be(Environment.MachineName);

        configuration[$"{_opt.Section}:env"].Should().Be(expected);
        configuration[$"{_opt.Section}:environment"].Should().Be(expected);
    }

    [Theory]
    [InlineData("sbx")]
    [InlineData("qat")]
    [InlineData("dev")]
    [InlineData("uat")]
    [InlineData("pdn")]
    [InlineData("DEv")]
    [InlineData("uAT")]
    [InlineData("pDn")]
    public void TestMpsRuntimeGoodEnv(string env)
    {
        // arrange 
        Environment.SetEnvironmentVariable(MpsRuntimeConfigurationProvider.EnvMpsEnvironment, env);

        var expected = env.ToLower();

        var configuration = _sut.Build();

        // act
        MpsRuntime mpsRuntime = configuration.GetSection(nameof(mpsRuntime)).Get<MpsRuntime>();

        // assert
        mpsRuntime.MpsEnvironment.Should().Be(expected);
        mpsRuntime.IsMpsConfigurationValid.Should().BeTrue();
        mpsRuntime.ServiceBusName.Should().Be(expected);

        configuration[$"{nameof(mpsRuntime)}:{nameof(mpsRuntime.MpsEnvironment)}"].Should().Be(expected);
        configuration[$"{nameof(mpsRuntime)}:{nameof(mpsRuntime.IsMpsConfigurationValid)}"].Should().Be("true");
        configuration[$"{nameof(mpsRuntime)}:{nameof(mpsRuntime.ServiceBusName)}"].Should().Be(expected);

        configuration[$"{_opt.Section}:env"].Should().Be(expected);
        configuration[$"{_opt.Section}:environment"].Should().Be(expected);
    }
}