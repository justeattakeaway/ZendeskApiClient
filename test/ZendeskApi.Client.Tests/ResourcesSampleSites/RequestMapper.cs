using System.Linq;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal static class RequestMapper
    {
        internal static UserResponse MapToUserResponse(UserCreateRequest request)
        {
            return new UserResponse
            {
                Name = request.Name,
                Email = request.Email,
                Alias = request.Alias,
                ExternalId = request.ExternalId,
                OrganizationId = request.OrganizationId,
                DefaultGroupId = request.DefaultGroupId,
                Phone = request.Phone,
                Tags = request.Tags?.ToList()
            };
        }

        internal static void ApplyTo(UserUpdateRequest source, UserResponse destination)
        {
            if (source.Name != null) destination.Name = source.Name;
            if (source.Email != null) destination.Email = source.Email;
            if (source.Alias != null) destination.Alias = source.Alias;
            if (source.ExternalId != null) destination.ExternalId = source.ExternalId;
            if (source.OrganizationId.HasValue) destination.OrganizationId = source.OrganizationId;
            if (source.DefaultGroupId.HasValue) destination.DefaultGroupId = source.DefaultGroupId;
            if (source.Phone != null) destination.Phone = source.Phone;
            if (source.Tags != null) destination.Tags = source.Tags.ToList();
        }

        internal static TicketResponse MapToTicketResponse(TicketCreateRequest request)
        {
            return new TicketResponse
            {
                Ticket = MapToTicket(request)
            };
        }

        private static Ticket MapToTicket(TicketCreateRequest request)
        {
            return new Ticket
            {
                Subject = request.Subject,
                ExternalId = request.ExternalId,
                RequesterId = request.RequesterId,
                AssigneeId = request.AssigneeId,
                OrganisationId = request.OrganisationId,
                GroupId = request.GroupId,
                CollaboratorIds = request.CollaboratorIds?.ToList(),
                Type = request.Type,
                Priority = request.Priority,
                Status = request.Status.GetValueOrDefault(),
                Tags = request.Tags?.ToList(),
                ForumTopicId = request.ForumTopicId,
                ProblemId = request.ProblemId,
                Due = request.Due,
                FormId = request.FormId,
                BrandId = request.BrandId
            };
        }

        internal static void ApplyTo(TicketUpdateRequest source, TicketResponse destination)
        {
            var ticket = destination.Ticket;
            if (source.Subject != null) ticket.Subject = source.Subject;
            if (source.RequesterId.HasValue) ticket.RequesterId = source.RequesterId;
            if (source.AssigneeId.HasValue) ticket.AssigneeId = source.AssigneeId;
            if (source.GroupId.HasValue) ticket.GroupId = source.GroupId;
            if (source.OrganisationId.HasValue) ticket.OrganisationId = source.OrganisationId;
            if (source.CollaboratorIds != null) ticket.CollaboratorIds = source.CollaboratorIds.ToList();
            if (source.Type.HasValue) ticket.Type = source.Type;
            if (source.Priority.HasValue) ticket.Priority = source.Priority;
            if (source.Status.HasValue) ticket.Status = source.Status.Value;
            if (source.Tags != null) ticket.Tags = source.Tags.ToList();
            if (source.ExternalId != null) ticket.ExternalId = source.ExternalId;
            if (source.ProblemId.HasValue) ticket.ProblemId = source.ProblemId;
            if (source.Due.HasValue) ticket.Due = source.Due;
            if (source.FormId.HasValue) ticket.FormId = source.FormId;
            if (source.BrandId.HasValue) ticket.BrandId = source.BrandId;
        }
    }
}
