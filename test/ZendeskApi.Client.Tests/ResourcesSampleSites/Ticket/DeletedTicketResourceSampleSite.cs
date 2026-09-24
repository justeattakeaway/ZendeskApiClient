using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class TicketState : State<Ticket>
    {
        public readonly IDictionary<long, IList<TicketComment>> TicketComments = new Dictionary<long, IList<TicketComment>>();
    }

    internal class DeletedTicketResourceSampleSite : SampleSite<TicketState, Ticket>
    {
        public DeletedTicketResourceSampleSite(string resource)
            : base(resource, MatchesRequest, populateState: PopulateState)
        { }

        private static void PopulateState(TicketState state)
        {
            for (var i = 1; i <= 100; i++)
            {
                state.Items.Add(i, new Ticket
                {
                    Id = i,
                    Subject = $"My printer is on fire! {i}",
                    ExternalId = i.ToString(),
                    OrganisationId = i,
                    RequesterId = i,
                    AssigneeId = i,
                    CollaboratorIds = new List<long> { i }
                });
            }
        }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/deleted_tickets", (req, resp, routeData) =>
                    {
                        return RequestHelper.List<DeletedTicketsListResponse, Ticket, TicketState>(
                            req,
                            resp,
                            items => new DeletedTicketsListResponse
                            {
                                Tickets = items,
                                Count = items.Count
                            });
                    })
                    .MapPut("api/v2/deleted_tickets/{id}/restore", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req
                            .HttpContext
                            .RequestServices
                            .GetRequiredService<TicketState>();

                        if (id == int.MinValue)
                        {
                            resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                            return Task.FromResult(resp);
                        }

                        if (!state.Items.ContainsKey(id))
                        {
                            resp.StatusCode = (int) HttpStatusCode.NotFound;
                            return resp.WriteAsJson(new object());
                        }

                        state.Items.Remove(id);
                        
                        resp.StatusCode = (int)HttpStatusCode.OK;

                        return Task.CompletedTask;
                    })
                    .MapPut("api/v2/deleted_tickets/restore_many", (req, resp, routeData) =>
                    {
                        var idParameterValue = req.Query["ids"]
                            .First()
                            .ToString();

                        if (!idParameterValue.Contains(","))
                        {
                            resp.StatusCode = 500;
                            return Task.CompletedTask;
                        }

                        var theIds = idParameterValue
                            .Split(',')
                            .Select(x => long.Parse(x.Trim()))
                            .ToList();

                        var state = req.HttpContext.RequestServices.GetRequiredService<TicketState>();

                        foreach (var anId in theIds)
                        {
                            state.Items.Remove(anId);
                        }

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return Task.CompletedTask;
                    })
                    .MapDelete("api/v2/deleted_tickets/destroy_many", (req, resp, routeData) =>
                    {
                        var idParameterValue = req.Query["ids"].First().ToString();

                        if (!idParameterValue.Contains(","))
                        {
                            resp.StatusCode = 500;
                            return Task.CompletedTask;
                        }

                        var theIds = idParameterValue
                            .Split(',')
                            .Select(x => long.Parse(x.Trim()))
                            .ToList();

                        var state = req.HttpContext.RequestServices.GetRequiredService<TicketState>();

                        foreach (var anId in theIds)
                        {
                            state.Items.Remove(anId);
                        }
                        
                        var jobStatusResponse = new JobStatusResponse
                        {
                            Id = Rand.Next(999).ToString(),
                            Results = null,
                            Total = 0,
                            Message = null,
                            Progress = 0,
                            Status = "queued"
                        };

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new SingleJobStatusResponse {JobStatus = jobStatusResponse});
                    })
                    .MapDelete("api/v2/deleted_tickets/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<TicketState>();

                        if (id == int.MinValue)
                        {
                            resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                            return Task.FromResult(resp);
                        }

                        if (!state.Items.ContainsKey(id))
                        {
                            resp.StatusCode = (int) HttpStatusCode.NotFound;
                            return resp.WriteAsync("Not found");
                        }

                        state.Items.Remove(id);

                        var jobStatusResponse = new JobStatusResponse
                        {
                            Id = Rand.Next(999).ToString(),
                            Results = null,
                            Total = 0,
                            Message = null,
                            Progress = 0,
                            Status = "queued"
                        };

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new SingleJobStatusResponse {JobStatus = jobStatusResponse});
                    });
            }
        }
    }
}
