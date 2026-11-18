using System;
using Microsoft.AspNetCore.Routing;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class TicketFieldsResourceSampleSite : SampleSite<TicketField>
    {
        public TicketFieldsResourceSampleSite(string resource)
            : base(
                resource,
                MatchesRequest,
                null,
                PopulateState)
        { }

        private static void PopulateState(State<TicketField> state)
        {
            for (var i = 1; i <= 100; i++)
            {
                state.Items.Add(i, new TicketField
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
                    .MapGet("api/v2/ticket_fields/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.GetById<TicketFieldResponse, TicketField>(
                            req,
                            resp,
                            routeData,
                            item => new TicketFieldResponse
                            {
                                TicketField = item
                            });
                    })
                    .MapGet("api/v2/ticket_fields", (req, resp, routeData) =>
                    {
                        return RequestHelper.List<TicketFieldsResponse, TicketField>(
                            req,
                            resp,
                            items => new TicketFieldsResponse
                            {
                                TicketFields = items,
                                Count = items.Count
                            });
                    })
                    .MapPost("api/v2/ticket_fields", async (req, resp, routeData) =>
                    {
                        var request = await req.ReadAsync<TicketFieldCreateUpdateRequest>();
                        var membership = request.TicketField;

                        await RequestHelper.Create(
                            req,
                            resp,
                            routeData,
                            item => item.Id,
                            membership,
                            item => new TicketFieldResponse
                            {
                                TicketField = item
                            });
                    })
                    .MapPut("api/v2/ticket_fields/{id}", async (req, resp, routeData) =>
                    {
                        var updateRequestModel = await req.ReadAsync<TicketFieldCreateUpdateRequest>();
                        await RequestHelper.Update(
                            req,
                            resp,
                            routeData,
                            updateRequestModel.TicketField,
                            item => new TicketFieldResponse
                            {
                                TicketField = item
                            });
                    })
                    .MapDelete("api/v2/ticket_fields/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.Delete<TicketField>(
                            req,
                            resp,
                            routeData);
                    });
            }
        }
    }
}
