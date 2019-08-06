# Epinova.IssuuMedia
Epinova's take on Issuu's media API

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Epinova.IssuuMedia&metric=alert_status)](https://sonarcloud.io/dashboard?id=Epinova.IssuuMedia)
[![Build status](https://ci.appveyor.com/api/projects/status/mcqkfnes5s9mckkp/branch/master?svg=true)](https://ci.appveyor.com/project/Epinova_AppVeyor_Team/epinova-issuumedia/branch/master)
![Tests](https://img.shields.io/appveyor/tests/Epinova_AppVeyor_Team/epinova-issuumedia.svg)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Platform](https://img.shields.io/badge/platform-.NET%20Standard%202.0-blue?style=flat&logo=.net)](https://docs.microsoft.com/en-us/dotnet/standard/net-standard)

## Usage

### Configuration

No configuration via config files are needed. All API calls are made towards Issuu's production endpoint.
Each service method in this API wrapper takes in an API key & secret, so it is the calling application that decides how to configure that.

### Add registry to IoC container

If using Structuremap:
```csharp
    container.Configure(
        x =>
        {
            x.Scan(y =>
            {
                y.TheCallingAssembly();
                y.WithDefaultConventions();
            });

            x.AddRegistry<Epinova.IssuuMedia.MediaRegistry>();
        });
```

If you cannot use the [structuremap registry](src/MediaRegistry.cs) provided with this module,
you can manually set up [MediaService](src/MediaService.cs) for [IMediaService](src/IMediaService.cs).

### Inject contract and use service

[Epinova.IssuuMedia.IMediaService](src/IMediaService.cs) describes the service.

Basically, you can fetch info for embedded documents and list all documents belonging to the specified account.

The Issuu API has several other methods not implemented/wrapped in this module yet, so this is a very simple read-only module so far.

### Prerequisites

* [EPiServer.Framework](http://www.episerver.com/web-content-management) >= v11.1 for logging purposes.
* [Automapper](https://github.com/AutoMapper/AutoMapper) >= v8.0 for mapping models.
* [StructureMap](http://structuremap.github.io/) >= v4.7 for registering service contract.

### Installing

The module is published on nuget.org.

```bat
nuget install Epinova.IssuuMedia
```

## Target framework

* .NET Standard 2.0
* Tests target .NET Core 2.1

## Authors

* **Tarjei Olsen** - *Initial work* - [apeneve](https://github.com/apeneve)

See also the list of [contributors](https://github.com/Epinova/Epinova.IssuuMedia/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

## Further reading

[Issuu API documentation](https://developer.issuu.com/)
