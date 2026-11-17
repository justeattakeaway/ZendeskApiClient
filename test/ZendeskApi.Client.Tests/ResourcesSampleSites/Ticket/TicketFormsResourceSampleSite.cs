using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class TicketFormsResourceSampleSite : SampleSite<TicketForm>
    {
        public TicketFormsResourceSampleSite(string resource)
            : base(
                resource,
                MatchesRequest,
                null,
                PopulateState)
        { }

        private static void PopulateState(State<TicketForm> state)
        {
            for (var i = 1; i <= 100; i++)
            {
                state.Items.Add(i, new TicketForm
                {
                    Id = i,
                    Name = $"name.{i}"
                });
            }
        }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/ticket_forms/show_many", (req, resp, routeData) =>
                    {
                        var ids = req.Query["ids"].ToString().Split(',').Select(long.Parse);

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<TicketForm>>();

                        var obj = state.Items.Where(x => ids.Contains(x.Key)).Select(p => p.Value);

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new TicketFormsResponse { TicketForms = obj });
                    })
                    .MapGet("api/v2/ticket_forms/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.GetById<TicketFormResponse, TicketForm>(
                            req,
                            resp,
                            routeData,
                            item => new TicketFormResponse
                            {
                                TicketForm = item
                            });
                    })
                    .MapGet("api/v2/ticket_forms", (req, resp, routeData) =>
                    {
                        return RequestHelper.List<TicketFormsResponse, TicketForm>(
                            req,
                            resp,
                            items => new TicketFormsResponse
                            {
                                TicketForms = items,
                                Count = items.Count
                            });
                    })
                    .MapPost("api/v2/ticket_forms", async (req, resp, routeData) =>
                    {
                        var request = await req.ReadAsync<TicketFormCreateUpdateRequest>();
                        var membership = request.TicketForm;

                        await RequestHelper.Create(
                            req,
                            resp,
                            routeData,
                            item => item.Id,
                            membership,
                            item => new TicketFormResponse
                            {
                                TicketForm = item
                            });
                    })
                    .MapPut("api/v2/ticket_forms/{id}", async (req, resp, routeData) =>
                    {
                        var updateRequestModel = await req.ReadAsync<TicketFormCreateUpdateRequest>();
                        await RequestHelper.Update<TicketFormResponse, TicketForm>(
                            req,
                            resp,
                            routeData,
                            updateRequestModel.TicketForm,
                            item => new TicketFormResponse
                            {
                                TicketForm = item
                            });
                    })
                    .MapDelete("api/v2/ticket_forms/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.Delete<TicketForm>(
                            req,
                            resp,
                            routeData);
                    });
            }
        }
    }
}
