# Zendesk Api Client
[![NuGet Version](https://img.shields.io/nuget/vpre/ZendeskApi.Client.svg?style=flat-square)](https://www.nuget.org/packages/ZendeskApi.Client)
[![NuGet Downloads](https://img.shields.io/nuget/dt/ZendeskApi.Client.svg?style=flat-square)](https://www.nuget.org/packages/ZendeskApi.Client)
[![ci](https://github.com/justeat/ZendeskApiClient/actions/workflows/ci.yml/badge.svg)](https://github.com/justeat/ZendeskApiClient/actions/workflows/ci.yml)

A .netstandard NuGet package for use with the  Zendesk v2 API.

# Breaking Changes

## 7.x.x

#### The deprecation and replacement of Status API endpoints

More detailed information on the exact changes and motivation can be found [here](https://support.zendesk.com/hc/en-us/articles/5414949730842).

For the sake of this library, it means the following methods have been removed:

- `IZendeskClient.ServiceStatus.ListComponents(...)`
- `IZendeskClient.ServiceStatus.ListSubComponents(...)`
- `IZendeskClient.ServiceStatus.GetComponentStatus(...)`
- `IZendeskClient.ServiceStatus.GetSubComponentStatus(...)`

In favour of the following:

- `IZendeskClient.ServiceStatus.ListActiveIncidents(...)`
- `IZendeskClient.ServiceStatus.GetActiveIncident(...)`
- `IZendeskClient.ServiceStatus.ListMaintenanceIncidents(...)`

## 6.x.x
#### Added support for .NET 6

ZendeskApiClient now targets both .NET Standard 2.0 and .NET 6, enabling users to leverage the latest runtime features and performance optimizations for their projects. Please note that with this update, support for .NET Core 3.1 is no longer targeted.

## 4.x.x
This will allow for cursor pagination on some endpoints. We have kept the original offset pagination available so that this release is backward compatible, however due to Zendesk adding a variation of cursor pagination to this single [endpoint](https://developer.zendesk.com/api-reference/ticketing/tickets/ticket_audits/#pagination), we have modified the original `cursorPager` class to now be `cursorPagerVariant`. If you use the TicketAudits endpoint please update your code.

Unfortunately Zendesk has not adopted cursor pagination on all endpoints. A list of compatible endpoints used in this API are as follows:

Users:
- GET /api/v2/deleted_users
- GET /api/v2/users
- GET /api/v2/users/{user_id}/identities
- GET /api/v2/user_fields
- GET /api/v2/users/{user_id}/groups

Groups:
- GET /api/v2/groups
- GET /api/v2/groups/{group_id}/users

Help Center:
- GET api/v2/help_center/articles
- GET api/v2/help_center/categories
- GET api/v2/help_center/sections

Organization:
- GET /api/v2/organization_fields
- GET /api/v2/organization_memberships
- GET /api/v2/organizations
- GET /api/v2/organizations/{organization_id}/users
- GET /api/v2/organizations/{organization_id}/tickets

Tickets:
- GET /api/v2/deleted_tickets
- GET /api/v2/tickets
- GET /api/v2/tickets/{ticketId}/comments
- GET /api/v2/ticket_fields
- GET /api/v2/ticket_audits
- GET

Satisfaction ratings:
- GET /api/v2/satisfaction_ratings

Requests
- GET /api/v2/requests
- GET /api/v2/requests/{requestId}/comments

Job Statuses
- GET /api/v2/job_statuses

[Further reading on Zendesk Pagination changes](https://support.zendesk.com/hc/en-us/articles/4402610093338-Introducing-Pagination-Changes-Zendesk-API)

## 3.x.x
This is a complete rewrite so expect breaking changes.

Some noteworthy changes include:
- `PostAsync` replaced with `CreateAsync`
- `PutAsync` replaced with `UpdateAsync`
- `Search` resource now uses `SearchAsync` instead of `Find`, and introduces a new fluent api to replace the old `ZendeskQuery<T>` class.


## Creating a client
To register this in your DI, you just need to call...
```c#
services.AddZendeskClient("https://[your_url].zendesk.com", "username", "token");
```
then you can inject in IZendeskClient anywhere you want to use it. Here you can access all the resources available.

If however you want to use the client in a DI less environment you can do this

```c#
var zendeskOptions = new ZendeskOptions
{
   EndpointUri = "https://[your_url].zendesk.com",
   Username = "username"
   Token = "token"
};

var loggerFactory = new LoggerFactory();
var zendeskOptionsWrapper = new OptionsWrapper<ZendeskOptions>(zendeskOptions);
var client = new ZendeskClient(new ZendeskApiClient(zendeskOptionsWrapper), loggerFactory.CreateLogger<ZendeskClient>());
```

## Authentication

The client supports three ways of authenticating.

### API token

```c#
services.AddZendeskClient("https://[your_url].zendesk.com", "username", "token");
```

Note that Zendesk is [retiring API tokens](https://developer.zendesk.com/documentation/api-basics/authentication/oauth-migration/),
so new integrations should prefer OAuth.

### An OAuth access token you already hold

```c#
services.AddZendeskClient("https://[your_url].zendesk.com", "oauth_token");
```

The token is used as supplied and is never renewed, so the caller is responsible for
replacing it before it expires.

### OAuth client credentials

For server to server integrations where no user is available to approve access, the client
can obtain its own access tokens from the Secret of an
[OAuth client](https://developer.zendesk.com/api-reference/ticketing/oauth/grant_type_tokens/)
using the client credentials grant.

```c#
services.AddZendeskClientWithClientCredentials(
    "https://[your_url].zendesk.com",
    "your_client_id",
    "your_client_secret");
```

Scopes can be requested where the OAuth client requires them.

```c#
services.AddZendeskClientWithClientCredentials(
    "https://[your_url].zendesk.com",
    "your_client_id",
    "your_client_secret",
    scope: "tickets:read tickets:write");
```

The Unique identifier and Secret both come from an OAuth client registered under
*Admin Center > Apps and integrations > APIs > OAuth clients*. The client must be of kind
*Confidential*, and the Secret is shown in full only once when the client is created.

Tokens are requested on first use, cached, and renewed automatically shortly before they
expire, so nothing needs to be rotated by the caller. The client credentials grant does not
issue refresh tokens; an expired token is replaced by running the grant again.

Token lifetime and the renewal margin can be configured.

```c#
services.Configure<ZendeskOptions>(options =>
{
    options.TokenExpiresInSeconds = 1800;                     // requested token lifetime
    options.TokenRenewalBuffer = TimeSpan.FromMinutes(2);     // renew this long before expiry
});
```

## Example methods
```c#
var ticket = await client.Tickets.GetAsync(1234L); // Get ticket by Id
var tickets = await client.Tickets.GetAllAsync(new [] { 1234L, 4321L }); //
var ticket = await client.Tickets.UpdateAsync(ticket);
var ticket = await client.Tickets.CreateAsync(ticket);
await client.Tickets.DeleteAsync(1234L);
```

## Searching and filtering
```c#
await client.Search.SearchAsync<User>(q =>
    q.WithFilter("email", "jazzy.b@just-eat.com")
);

await client.Search.SearchAsync<Organization>(q =>
    q.WithFilter("name", "Cupcake Cafe")
);

// All non closed tickets
await client.Search.SearchAsync<Ticket>(q =>
    q.WithFilter("status", "closed", FilterOperator.LessThan)
);
```

## Using Cursor Based Pagination
You can use the `CursorPaginatedIterator` to loop through multiple pages as shown below:
```c#

var services = new ServiceCollection();
services.AddZendeskClientWithHttpClientFactory("https://yoursubomain.zendesk.com", "your@email.com", "your_token_");
var serviceProvider = services.BuildServiceProvider();
var client = serviceProvider.GetRequiredService<IZendeskClient>();

var ticketCursorResponse = await client.Tickets.GetAllAsync(new CursorPager { Size = 5 }); // low page number to force pagination

var iteratorFactory = serviceProvider.GetRequiredService<ICursorPaginatedIteratorFactory>();
// creates the iterator with the response object of the first  request
var iterator = iteratorFactory.Create<Ticket>(ticketCursorResponse);

foreach (var ticket in iterator)
{
    Console.WriteLine("the id of this ticket is:" + ticket.Id);
} // this loop will stop at the first page

while (iterator.HasMore()) // to loop through all pages
{
    await iterator.NextPage();
    foreach (var ticket in iterator)
    {
        Console.WriteLine("the id of this ticket is:" + ticket.Id);
    }
}

// alternatively you can use .All() from the iterator
await foreach (var ticket in iterator.All())
{
    Console.WriteLine("the id of this ticket is:" + ticket.Id);
}

```

## The Zendesk API

The zendesk api documentation is available at http://developer.zendesk.com/documentation/rest_api/introduction.html
Querying and searching is limited by the searchable fields on the zendesk api

> Please note that starting from August 15, 2023, if you make offset-based pagination (OBP) requests beyond the first 100 pages (10,000 records), you will receive an error message: "400 Bad Request." To retrieve data sets larger than 10,000 records, customers must transition to cursor-based pagination (CBP). You can find more information on the transition [here](https://developer.zendesk.com/documentation/api-basics/pagination/paginating-through-lists-using-cursor-pagination/). This library designates the OBP endpoints as `Obsolete` and introduces the new CBP endpoints in version 4.x.x. Therefore, users who wish to make the transition will need to upgrade.

## Integration Tests

In order to run integration tests against your own zendesk instance use the Cake script provided by:

```powershell
.\build.ps1 --target=Run-Integration-Tests --zendeskUrl="<your zendesk url>" --zendeskUsername="<your zendesk username>" --zendeskToken="<your zendesk token>"
```

### Running integration tests on Visual Studio for Mac

In Visual Studio, open the root folder of this project and open the `test` view (has a little lightening bolt icon).
You should see a structure that looks like:
    * ZendeskApiClient
        * test
            * ZendeskApi.Client.IntegrationTests
            * ZendeskApi.Client.Tests
To run the integration tests, right click on it and select `Run Test With` ==> `Custom Parameters` - make sure you add the following variables: `ZendeskApi_Credentials_Url, ZendeskApi_Credentials_Username, ZendeskApi_Credentials_Token`
You can save this configuration and give it a name, in order to run it multiple times.

# Contributing

[CONTRIBUTING.md](./CONTRIBUTING.md).

## Versioning

We aim to follow [Semantic Versioning](https://semver.org/) guidelines within this library. When increasing the version please increment [ZendeskApi.Commons.props](https://github.com/justeat/ZendeskApiClient/blob/master/src/ZendeskApi.Build/ZendeskApi.Commons.props).
