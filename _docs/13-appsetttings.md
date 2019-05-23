# Notes on appSettings.json

## Kestrel

## How Kestrel determines bindable urls

Under the `Kestrel` section, it should have a `Endpoints` sub-section.
Inside this section, each should element should have a `Url` element.
Kestrel will take this url as the url to be bound.
So in the below code, we are binding 3 urls:

1.  http://localhost:35000
2.  http://localhost:35001
3.  http://localhost:35002

The labels "LocalHost", "Http", "Https" are arbitrary.
You can label them "Cat", "Dog", "Monkey" if it makes more sense to you.

```JS
 "Kestrel": {
    "Endpoints": {
      "Localhost": {
        "Url": "http://localhost:35000"
      },
      "Http": {
        "Url": "http://localhost:35001"
      },
      "Https": {
        "Url": "http://localhost:35002"
      }
    }
  },
```

You might have notice that the production (`appsettings.json`) settings for 
Kestrel endpoints are all non-HTTPS endpoints (`http://`).
This in intentional due to setup in production server:
Cloud => HAProxy => Nginx => Kestrel

Initially in `Startup.cs`, we have a line of code that reads:

```CS
app.UseHttpsRedirection();
```

Because of this line of code, Kestrel will try to redirect requests that are non-HTTPS
to HTTPS. This will result in error, because our production setup don't handle HTTPS behind
the scenes (HAProxy). So we comment out this line and change all urls to be non-HTTPS.

ZX: Technically, since we have commented out this line, we do not need to make the urls to be 
non-HTTPS; we just have to make sure that server bind to the the non-HTTPS port.

But we intentionally do that because:

1.  We want to use the same port in production and development.
2.  To demonstrate that by defining the `Kestrel` section in `appsettings.Development.json` 
    we will override the production settings when we run the website in a local development environment.
