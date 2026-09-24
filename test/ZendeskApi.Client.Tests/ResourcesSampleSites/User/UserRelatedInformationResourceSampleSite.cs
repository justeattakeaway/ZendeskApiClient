using System;
using Microsoft.AspNetCore.Routing;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class UserRelatedInformationResourceSampleSite : SampleSite<UserRelatedInformationResponse>
    {
        public UserRelatedInformationResourceSampleSite(string resource)
            : base(resource, MatchesRequest, populateState: PopulateState)
        { }

        private static void PopulateState(State<UserRelatedInformationResponse> state)
        {
            for (var i = 1; i <= 100; i++)
            {
                state.Items.Add(i, new UserRelatedInformationResponse
                {
                    AssignedTickets = i,
                    RequestedTickets = i + 1,
                    CcdTickets = i + 2,
                    OrganizationSubscriptions = i + 3,
                    Topics = i + 4,
                    TopicComments = i + 5,
                    Votes = i + 6,
                    Subscriptions = i + 7,
                    EntrySubscriptions = i + 8,
                    ForumSubscriptions = i + 9
                });
            }
        }

        private static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/users/{id}/related", (req, resp, routeData) =>
                    {
                        return RequestHelper.GetById<SingleUserRelatedInformationResponse, UserRelatedInformationResponse>(
                            req,
                            resp,
                            routeData,
                            item => new SingleUserRelatedInformationResponse
                            {
                                UserRelatedInformationResponse = item
                            });
                    });
            }
        }
    }
}