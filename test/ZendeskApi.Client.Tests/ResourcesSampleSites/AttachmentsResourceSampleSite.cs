using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class AttachmentsResourceSampleSite : SampleSite<State<Attachment>, Attachment>
    {
        public AttachmentsResourceSampleSite(string resource)
        : base(
            resource, 
            MatchesRequest,
            null,
            PopulateState)
        { }

        private static void PopulateState(State<Attachment> state)
        {
            for (var i = 1; i <= 100; i++)
            {
                state.Items.Add(i, new Attachment
                {
                    Id = i,
                    FileName = $"filename.{i}"
                });
            }
        }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/attachments/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.GetById<Attachment, Attachment>(
                            req,
                            resp,
                            routeData,
                            item => item);
                    })
                    .MapPost("api/v2/uploads", async (req, resp, routeData) =>
                    {
                        var filename = req.Query["filename"];

                        if (string.IsNullOrWhiteSpace(filename))
                        {
                            resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                            return;
                        }

                        resp.StatusCode = (int)HttpStatusCode.Created;

                        resp.Headers.Add("Location", "https://localhost/api/v2/attachments/498483");

                        var state = req
                            .HttpContext
                            .RequestServices
                            .GetRequiredService<State<Attachment>>();

                        var attachment = new Attachment
                        {
                            ContentType = "text/plain",
                            ContentUrl = "https://company.zendesk.com/attachments/crash.log",
                            Size = await GetLengthAsync(req.Body),
                            FileName = req.Query["filename"],
                            Id = long.Parse(Rand.Next().ToString())
                        };

                        state.Items.Add(attachment.Id.Value, attachment);

                        await resp.WriteAsJson(new UploadResponse{
                            Upload = new Upload {
                                Attachment = attachment,
                                Token = "6bk3gql82em5nmf"
                            }
                        });
                    })
                    .MapDelete("api/v2/uploads/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.Delete<Attachment>(
                            req,
                            resp,
                            routeData);
                    })
                    .MapDelete("api/v2/attachments/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.Delete<Attachment>(
                            req,
                            resp,
                            routeData);
                    });
            }
        }

        /// <remarks>
        /// Cannot simply use <see cref="Stream.Length"/> because some <see cref="Stream"/>s are not seekable.
        /// Here we use a wrapper stream which is seekable to determine length.
        /// </remarks>
        private static async Task<long> GetLengthAsync(Stream stream)
        {
            var seekableStream = new MemoryStream();
            await stream.CopyToAsync(seekableStream);
            return seekableStream.Length;
        }
    }
}
