# Epinova.IssuuMedia
Epinova's take on Issuu's media API

## Add registry to Structuremap

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