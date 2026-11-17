using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Formatters;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    /// <summary>
    /// <see cref="https://developer.zendesk.com/rest_api/docs/core/tickets"/>
    /// </summary>
    public class TicketsResource : AbstractBaseResource<TicketsResource>,
        ITicketsResource
    {
        private const string ResourceUri = "api/v2/tickets";
        private const string OrganizationResourceUriFormat = "api/v2/organizations/{0}/tickets";
        private const string UserResourceUriFormat = "api/v2/users/{0}/tickets";

        public TicketsResource(
            IZendeskApiClient apiClient, 
            ILogger logger)
            : base(apiClient, logger, "tickets")
        { }

        #region GetAll Tickets
        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        public async Task<IPagination<Ticket>> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<TicketsListResponse>(
                ResourceUri,
                "list-tickets",
                "ListAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<ICursorPagination<Ticket>> GetAllAsync(
            CursorPager pager,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<TicketsListCursorResponse>(
                ResourceUri,
                "list-tickets",
                "ListAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        [Obsolete("Use `GetAllByOrganizationIdAsync` with CursorPager parameter instead.")]
        public async Task<IPagination<Ticket>> GetAllByOrganizationIdAsync(
            long organizationId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetWithNotFoundCheckAsync<TicketsListResponse>(
                string.Format(OrganizationResourceUriFormat, organizationId),
                "list-tickets",
                $"GetAllByOrganizationIdAsync({organizationId})",
                $"Tickets in organization {organizationId} not found",
                pager,
                cancellationToken);
        }

        public async Task<ICursorPagination<Ticket>> GetAllByOrganizationIdAsync(
            long organizationId,
            CursorPager pager,
            CancellationToken cancellationToken = default)
        {
            return await GetWithNotFoundCheckAsync<TicketsListCursorResponse>(
                string.Format(OrganizationResourceUriFormat, organizationId),
                "list-tickets",
                $"GetAllByOrganizationIdAsync({organizationId})",
                $"Tickets in organization {organizationId} not found",
                pager,
                cancellationToken);
        }

        public async Task<IPagination<Ticket>> GetAllByRequestedByIdAsync(
            long userId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetWithNotFoundCheckAsync<TicketsListResponse>(
                $"{string.Format(UserResourceUriFormat, userId)}/requested",
                "list-tickets",
                $"GetAllByRequestedByIdAsync({userId})",
                $"Requested ticketsResponse for user {userId} not found",
                pager,
                cancellationToken);
        }

        public async Task<IPagination<Ticket>> GetAllByCcdIdAsync(
            long userId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetWithNotFoundCheckAsync<TicketsListResponse>(
                $"{string.Format(UserResourceUriFormat, userId)}/ccd",
                "list-tickets",
                $"GetAllByCcdIdAsync({userId})",
                $"CCD ticketsResponse for user {userId} not found",
                pager,
                cancellationToken);
        }

        public async Task<IPagination<Ticket>> GetAllByAssignedToIdAsync(
            long userId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetWithNotFoundCheckAsync<TicketsListResponse>(
                $"{string.Format(UserResourceUriFormat, userId)}/assigned",
                "list-tickets",
                $"ListAssignedToAsync({userId})",
                $"Assigned ticketsResponse for user {userId} not found",
                pager,
                cancellationToken);
        }

        public async Task<IPagination<Ticket>> GetAllByExternalIdAsync(
            string externalId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<TicketsListResponse>(
                $"{ResourceUri}?external_id={externalId}",
                "list-tickets-by-external-id",
                $"GetAllByExternalIdAsync({externalId})",
                pager,
                cancellationToken: cancellationToken);
        }
        #endregion

        #region List Tickets
        [Obsolete("Use `GetAllAsync` instead.")]
        public async Task<IPagination<Ticket>> ListAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAllAsync(
                pager,
                cancellationToken);
        }

        [Obsolete("Use `GetAllByOrganizationIdAsync` instead.")]
        public async Task<IPagination<Ticket>> ListForOrganizationAsync(
            long organizationId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAllByOrganizationIdAsync(
                organizationId,
                pager,
                cancellationToken);
        }

        [Obsolete("Use `GetAllByRequestedByIdAsync` instead.")]
        public async Task<IPagination<Ticket>> ListRequestedByAsync(
            long userId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAllByRequestedByIdAsync(
                userId,
                pager,
                cancellationToken);
        }

        [Obsolete("Use `GetAllByCcdIdAsync` instead.")]
        public async Task<IPagination<Ticket>> ListCcdAsync(
            long userId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAllByCcdIdAsync(
                userId,
                pager,
                cancellationToken);
        }

        [Obsolete("Use `GetAllByAssignedToIdAsync` instead.")]
        public async Task<IPagination<Ticket>> ListAssignedToAsync(
            long userId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAllByAssignedToIdAsync(
                userId,
                pager,
                cancellationToken);
        }
        #endregion

        #region Show Tickets
        public async Task<TicketResponse> GetAsync(
            long ticketId,
            CancellationToken cancellationToken = default)
        {
            return await GetWithNotFoundCheckAsync<TicketResponse>(
                $"{ResourceUri}/{ticketId}",
                "show-ticket",
                $"GetAsync({ticketId})",
                $"TicketResponse {ticketId} not found",
                cancellationToken: cancellationToken);
        }

        [Obsolete("Use `GetAllAsync` instead.")]
        public async Task<IPagination<Ticket>> GetAsync(
            long[] ticketIds, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAllAsync(
                ticketIds,
                pager,
                cancellationToken);
        }

        public async Task<IPagination<Ticket>> GetAllAsync(
            long[] ticketIds,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<TicketsListResponse>(
                $"{ResourceUri}/show_many?ids={ZendeskFormatter.ToCsv(ticketIds)}",
                "show-multiple-tickets",
                $"GetAllAsync({ZendeskFormatter.ToCsv(ticketIds)})",
                pager,
                cancellationToken: cancellationToken);
        }
        #endregion

        #region Create Tickets
        public async Task<TicketResponse> CreateAsync(
            TicketCreateRequest ticket,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await CreateAsync(ticket, null, cancellationToken);
        }

        public async Task<TicketResponse> CreateAsync(
            TicketCreateRequest ticket,
            string idempotencyKey,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await CreateAsync<TicketResponse, TicketRequest<TicketCreateRequest>>(
                ResourceUri,
                new TicketRequest<TicketCreateRequest>(ticket),
                "create-ticket",
                cancellationToken: cancellationToken,
                idempotencyKey: idempotencyKey);
        }

        public async Task<JobStatusResponse> CreateAsync(
            IEnumerable<TicketCreateRequest> tickets,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await CreateAsync<SingleJobStatusResponse, TicketListRequest<TicketCreateRequest>>(
                $"{ResourceUri}/create_many",
                new TicketListRequest<TicketCreateRequest>(tickets),
                "create-many-tickets",
                HttpStatusCode.OK,
                cancellationToken: cancellationToken);

            return response?
                .JobStatus;
        }
        #endregion

        #region Update Tickets
        public async Task<TicketResponse> UpdateAsync(
            TicketUpdateRequest ticket,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await UpdateWithNotFoundCheckAsync<TicketResponse, TicketRequest<TicketUpdateRequest>>(
                $"{ResourceUri}/{ticket.Id}",
                new TicketRequest<TicketUpdateRequest>(ticket),
                "update-ticket",
                $"Cannot update ticketResponse as ticketResponse {ticket.Id} cannot be found",
                cancellationToken: cancellationToken);
        }

        public async Task<JobStatusResponse> UpdateAsync(
            IEnumerable<TicketUpdateRequest> tickets,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await UpdateAsync<JobStatusResponse, TicketListRequest<TicketUpdateRequest>>(
                $"{ResourceUri}/update_many.json",
                new TicketListRequest<TicketUpdateRequest>(tickets),
                "update-many-tickets",
                cancellationToken: cancellationToken);
        }

        public async Task<JobStatusResponse> TagListsUpdateAsync(
            long[] ticketIds,
            TicketTagListsUpdateRequest tickets,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await UpdateAsync<JobStatusResponse, TicketRequest<TicketTagListsUpdateRequest>>(
                $"{ResourceUri}/update_many.json?ids={ZendeskFormatter.ToCsv(ticketIds)}",
                new TicketRequest<TicketTagListsUpdateRequest>(tickets),
                "updating-tag-lists",
                cancellationToken: cancellationToken);
        }
        #endregion

        #region Mark Ticket as Spam and Suspend Requester
        public async Task<bool> MarkTicketAsSpamAndSuspendRequester(
            long ticketId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await UpdateWithNotFoundCheckAsync<HttpResponseMessage, object>(
                $"{ResourceUri}/{ticketId}/mark_as_spam",
                new { },
                "mark-ticket-as-spam-and-suspend-requester",
                $"Cannot mark ticketResponse {ticketId} as spam as the ticketResponse is not found",
                "MarkTicketAsSpamAndSuspendRequester",
                cancellationToken);

            return response != null;
        }

        public async Task<JobStatusResponse> MarkTicketAsSpamAndSuspendRequester(
            long[] ticketIds,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await UpdateAsync<JobStatusResponse, object>(
                $"{ResourceUri}/mark_many_as_spam.json?ids={ZendeskFormatter.ToCsv(ticketIds)}",
                new {},
                "bulk-mark-tickets-as-spam",
                $"MarkTicketAsSpamAndSuspendRequester({ZendeskFormatter.ToCsv(ticketIds)})",
                cancellationToken);
        }
        #endregion

        #region Delete Tickets
        public async Task DeleteAsync(
            long ticketId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await DeleteAsync(
                ResourceUri,
                ticketId,
                "delete-ticket",
                cancellationToken: cancellationToken);
        }

        public async Task<JobStatusResponse> DeleteAsync(
            IEnumerable<long> ticketIds,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var ids = ticketIds
                .ToList();

            var jobStatusResponse = await DeleteAsync<SingleJobStatusResponse>(
                $"{ResourceUri}/destroy_many.json",
                ids,
                "bulk-delete-tickets",
                cancellationToken: cancellationToken);
            
            return jobStatusResponse?
                .JobStatus;
        }
        #endregion
    }
}
