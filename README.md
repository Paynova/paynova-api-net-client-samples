# Paynova .NET API Client Samples #
This repo is where we collect samples that makes use of [the Paynova .NET API Client](https://github.com/Paynova/paynova-api-net-client).

## MvcSample ##
Contains some documentation and lets you perform checkouts, finalizations, refunds etc. You can also use it to inspect postbacks for: success, cancel & pending; as well as inspect Event hook notification callbacks, made from Paynova.

### State ###
By default it runs in memory using the Event hook notification callbacks from Paynova to keep track of payments and store this in-memory.

### Configuration ###
Credentials against Paynova need to be provided in the `Web.Config`.

You also need to provide a `callbacks_server_endpoint` which is where Paynova will do POST-redirects for: success, cancel & pending; as well as for Event hook notifications. It's used to accept the following POSTs:

- **POST:** `callbacks_server_endpoint + /postbacks/success`
- **POST:** `callbacks_server_endpoint + /postbacks/cancel`
- **POST:** `callbacks_server_endpoint + /postbacks/pending`
- **POST:** `callbacks_server_endpoint + /callbacks`

So if you e.g. host this in IIS-Express, you need to configure the site in IIS-Express to also listen on e.g. your IP and then ensure your firewall accepts incoming traffic. This is configured in: `C:\...\your_user\Documents\IISExpress\config\applicationhost.config`, where you e.g. can add:

```
<binding
    protocol="http"
    bindingInformation="*:63567:192.168.201.132" />
```

## Points of interest ##
The `SamplesController` consumes the `SamplePaynovaService` which encapsulates all interaction with Paynova, using the `PaynovaClient` distributed via the NuGet: [Paynova.Api.Client](http://nuget.org/packages/paynova.api.client)