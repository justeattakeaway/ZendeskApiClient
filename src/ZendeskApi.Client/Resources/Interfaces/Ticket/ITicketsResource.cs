using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{   
    /// <summary>
    /// <see cref="https://developer.zendesk.com/rest_api/docs/core/tickets"/>
    /// </summary>
    public interface ITicketsResource
    {
        #region GetAll Tickets
        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        Task<IPagination<Ticket>> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<ICursorPagination<Ticket>> GetAllAsync(
            CursorPager pager,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllByOrganizationIdAsync` with CursorPager parameter instead.")]
        Task<IPagination<Ticket>> GetAllByOrganizationIdAsync(
            long organizationId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<ICursorPagination<Ticket>> GetAllByOrganizationIdAsync(
            long organizationId,
            CursorPager pager,
            CancellationToken cancellationToken = default);

        Task<IPagination<Ticket>> GetAllByRequestedByIdAsync(
            long userId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<IPagination<Ticket>> GetAllByCcdIdAsync(
            long userId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<IPagination<Ticket>> GetAllByAssignedToIdAsync(
            long userId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<IPagination<Ticket>> GetAllByExternalIdAsync(
            string externalId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        #endregion

        #region List Tickets
        [Obsolete("Use `GetAllAsync` instead.")]
        Task<IPagination<Ticket>> ListAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllByOrganizationIdAsync` instead.")]
        Task<IPagination<Ticket>> ListForOrganizationAsync(
            long organizationId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllByRequestedByIdAsync` instead.")]
        Task<IPagination<Ticket>> ListRequestedByAsync(
            long userId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllByCcdIdAsync` instead.")]
        Task<IPagination<Ticket>> ListCcdAsync(
            long userId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllByAssignedToIdAsync` instead.")]
        Task<IPagination<Ticket>> ListAssignedToAsync(
            long userId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);
        #endregion

        #region Show Tickets
        Task<TicketResponse> GetAsync(
            long ticketId,
            CancellationToken cancellationToken = default);

        Task<IPagination<Ticket>> GetAllAsync(
            long[] ticketIds, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);
        #endregion

        #region Create Tickets
        Task<TicketResponse> CreateAsync(
            TicketCreateRequest ticket,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a ticket, sending <paramref name="idempotencyKey"/> as the Zendesk Idempotency-Key header
        /// so that repeating the same request does not create another ticket.
        /// </summary>
        Task<TicketResponse> CreateAsync(
            TicketCreateRequest ticket,
            string idempotencyKey,
            CancellationToken cancellationToken = default);

        Task<JobStatusResponse> CreateAsync(
            IEnumerable<TicketCreateRequest> tickets,
            CancellationToken cancellationToken = default);
        #endregion

        #region Update Tickets
        Task<TicketResponse> UpdateAsync(
            TicketUpdateRequest ticket,
            CancellationToken cancellationToken = default);

        Task<JobStatusResponse> UpdateAsync(
            IEnumerable<TicketUpdateRequest> tickets,
            CancellationToken cancellationToken = default);

        Task<JobStatusResponse> TagListsUpdateAsync(
            long[] ticketIds,
            TicketTagListsUpdateRequest tickets,
            CancellationToken cancellationToken = default);
        #endregion

        #region Mark Ticket as Spam and Suspend Requester
        Task<bool> MarkTicketAsSpamAndSuspendRequester(
            long ticketId,
            CancellationToken cancellationToken = default);

        Task<JobStatusResponse> MarkTicketAsSpamAndSuspendRequester(
            long[] ticketIds,
            CancellationToken cancellationToken = default);
        #endregion

        #region Delete Tickets
        Task DeleteAsync(
            long ticketId,
            CancellationToken cancellationToken = default);

        Task<JobStatusResponse> DeleteAsync(
            IEnumerable<long> ticketIds,
            CancellationToken cancellationToken = default);
        #endregion
    }
}
