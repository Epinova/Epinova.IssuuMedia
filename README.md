# Epinova.IssuuMedia
Epinova's take on Issuu's media API

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Epinova.IssuuMedia&metric=alert_status)](https://sonarcloud.io/dashboard?id=Epinova.IssuuMedia)
[![Build status](https://ci.appveyor.com/api/projects/status/mcqkfnes5s9mckkp/branch/master?svg=true)](https://ci.appveyor.com/project/Epinova_AppVeyor_Team/epinova-issuumedia/branch/master)
![Tests](https://img.shields.io/appveyor/tests/Epinova_AppVeyor_Team/epinova-issuumedia.svg "Tests")
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## Usage
### Add registry to Structuremap

```
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

### Inject contract and use service

Epinova.IssuuMedia.IMediaService describes the service. 