using System;
using Microsoft.AspNetCore.Routing;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests.User;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class UserFieldsResourceSampleSite : SampleSite<UserField>
    {
        public UserFieldsResourceSampleSite(string resource)
            : base(
                resource,
                MatchesRequest,
                null,
                PopulateState)
        { }

        private static void PopulateState(State<UserField> state)
        {
            for (var i = 1; i <= 100; i++)
            {
                state.Items.Add(i, new UserField
                {
                    Id = i,
                    RawTitle = $"raw.title.{i}"
                });
            }
        }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/user_fields/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.GetById<UserFieldResponse, UserField>(
                            req,
                            resp,
                            routeData,
                            item => new UserFieldResponse
                            {
                                UserField = item
                            });
                    })
                    .MapGet("api/v2/user_fields", (req, resp, routeData) =>
                    {
                        return RequestHelper.List<UserFieldsResponse, UserField>(
                            req,
                            resp,
                            items => new UserFieldsResponse
                            {
                                UserFields = items,
                                Count = items.Count
                            });
                    })
                    .MapPost("api/v2/user_fields", async (req, resp, routeData) =>
                    {
                        var request = await req.ReadAsync<UserFieldCreateUpdateRequest>();
                        var userField = request.UserField;

                        await RequestHelper.Create(
                            req,
                            resp,
                            routeData,
                            item => item.Id,
                            userField,
                            item => new UserFieldResponse
                            {
                                UserField = item
                            });
                    })
                    .MapPut("api/v2/user_fields/{id}", async (req, resp, routeData) =>
                    {
                        var request = await req.ReadAsync<UserFieldCreateUpdateRequest>();
                        await RequestHelper.Update(
                            req,
                            resp,
                            routeData,
                            request.UserField,
                            item => new UserFieldResponse
                            {
                                UserField = item
                            });
                    })
                    .MapDelete("api/v2/user_fields/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.Delete<UserField>(
                            req,
                            resp,
                            routeData);
                    })
                    ;
            }
        }
    }
}
