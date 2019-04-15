# Epinova.IssuuMedia
Epinova's take on Issuu's media API

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