using System;
using System.Net;
using Microsoft.AspNetCore.Routing;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class DeletedUsersResourceSampleSite : SampleSite<UserResponse>
    {
        public DeletedUsersResourceSampleSite(string resource)
            : base(resource, MatchesRequest, populateState: PopulateState)
        { }

        private static void PopulateState(State<UserResponse> state)
        {
            for (var i = 1; i <= 100; i++)
            {
                state.Items.Add(i, new UserResponse
                {
                    Id = i,
                    Name = $"name.{i}",
                    Email = $"email.{i}",
                    ExternalId = i.ToString()
                });
            }
        }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/deleted_users/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.GetById<SingleUserResponse, UserResponse>(
                            req,
                            resp,
                            routeData,
                            item => new SingleUserResponse
                            {
                                UserResponse = item
                            });
                    })
                    .MapGet("api/v2/deleted_users", (req, resp, routeData) =>
                    {
                        return RequestHelper.List<UsersListResponse, UserResponse>(
                            req,
                            resp,
                            items => new UsersListResponse
                            {
                                Users = items,
                                Count = items.Count
                            });
                    })
                    .MapDelete("api/v2/deleted_users/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.Delete<UserResponse>(
                            req,
                            resp,
                            routeData,
                            HttpStatusCode.OK);
                    });
            }
        }
    }
}
