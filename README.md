## DotNetCore MicroService Platform Runtime Configuration

**DotNetCore.Mps.Runtime** - configuration provider that injects into IConfiguration collected runtime information.


### Nuget.org

- Nuget package [DotNetCore.Mps.Runtime](https://www.nuget.org/packages/DotNetCore.Mps.Runtime/)

### Version: 8.0.0
- supports **netstandard2.0/2.1** ; **net8.0**


### Setup in the project

``` csharp

      // Adds Runtime Configuration
       builder.Configuration.AddMpsRuntimeWithMicroServiceName(typeof(Program).Assembly);
```
### Get MpsRuntime class object

``` csharp
     MpsRuntime mpsRuntime = builder.Configuration.GetSection(nameof(MpsRuntime)).Get<MpsRuntime>();
```


### IConfiguration Values:

|  Configuration path/key    | Value             |
| -------------------------  | ----------------  |
mps:env | Value defined by Environment "**microservice_environment**", default value is **dev** |
mps:msname    | Micorservice name. Value defined by Assembly "AssemblyProductAttribute" attribute or via "MpsRuntimeConfigurationOptions"  |
mps:docker    | "true" is runs in doecker container |
mps:host      | Value of environment "MachineName"  |
mps:bus       | Service busname the mps:env value or mps:host  |
mps:linux     | "true" when run on kinux |


### Project Customization 

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
```

**the BEST - Or Create own vision of the configuration provider locally**.


### Enterprice Configuration Integrations 

**DotNetCore.Mps.Runtime** - expected to use with 
DotNetCore Generic Configuration ie.e **DotNetCore.Configuration.Formatter** creates a new configuration values by substituting IConfiguration Keys with Values from other IConfiguration Keys.
- Nuget package: [DotNetCore.Configuration.Formatter](https://www.nuget.org/packages/DotNetCore.Configuration.Formatter/)
- GitHub project and examples: [DotNetCore Generic Configuration ](https://github.com/Wallsmedia/DotNetCore.Configuration.Formatter)
 
