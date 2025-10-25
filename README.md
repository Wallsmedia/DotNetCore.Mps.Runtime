## DotNetCore MicroService Platform Runtime Configuration

**DotNetCore.Mps.Runtime** - configuration provider that injects into IConfiguration collected runtime information.


### Nuget.org

- Nuget package [DotNetCore.Mps.Runtime](https://www.nuget.org/packages/DotNetCore.Mps.Runtime/)

### Version: 10.0.1xx
- supports **netstandard2.0/2.1** ; **net10.0**


### Setup in the project

``` csharp

      // Adds Runtime Configuration
       builder.Configuration.AddMpsRuntimeWithMicroServiceName(typeof(Program).Assembly);
```
or with exact microservice name.
``` csharp

      // Adds Runtime Configuration
       builder.Configuration.AddMpsRuntimeWithMicroServiceName(microserviceName: "MyMicroserviceName");
```


### Get MpsRuntime class object

``` csharp
     MpsRuntime mpsRuntime = builder.Configuration.GetSection(nameof(MpsRuntime)).Get<MpsRuntime>();
     builder.Services.AddSingleton(mpsRuntime);
```


### IConfiguration Values:

|  Configuration path/key    | Value             |
| -------------------------  | ----------------  |
mps:env | Value defined by Environment **microservice_environment**, default value is **dev** |
mps:msname    | Microservice name. Value defined by Assembly "*AssemblyProductAttribute*" attribute or via "MpsRuntimeConfigurationOptions"  |
mps:docker    | "true" is runs in docker container |
mps:host      | Value of environment "MachineName"  |
mps:bus       | Service bus name either the mps:env value or mps:host  |
mps:linux     | "true" when run on linux |


### Runtime Customization 

Use configuration  
``` csharp
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
```

### Runtime information logging

``` csharp
     MpsRuntime mpsRuntime = builder.Configuration.GetSection(nameof(MpsRuntime)).Get<MpsRuntime>();
     builder.Services.AddSingleton(mpsRuntime);
     builder.Services.AddHostedService<MpsLifetimeEventsHostedService>();
```

### Enterprise Configuration Integrations  

**DotNetCore.Mps.Runtime** - can be used with *DotNetCore Generic Configuration*.

**DotNetCore Generic Configuration** creates a new configuration values by substituting IConfiguration Keys with Values from other IConfiguration Keys.

References:
- Nuget package: [DotNetCore.Configuration.Formatter](https://www.nuget.org/packages/DotNetCore.Configuration.Formatter/)
- GitHub project and examples: [DotNetCore Generic Configuration ](https://github.com/Wallsmedia/DotNetCore.Configuration.Formatter)
 
