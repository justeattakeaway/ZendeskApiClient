using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ZendeskApi.Client.Json;
using Xunit;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Tests.Converters
{
    public class CustomFieldConverterTests
    {
        [Fact]
        public void CustomField_Deserializes_MultiValues_Correctly()
        {
            //Arrange
            var ticketJson = File.ReadAllText(AppContext.BaseDirectory + "/Converters/testTicket.json");

            //Act
            var ticket = JsonSerializer.Deserialize<Ticket>(ticketJson, ZendeskJson.Options);

            //Assert
            AssertDeserializationWasSuccessful(ticket);
        }

        [Fact]
        public void CustomField_Deserializes_NullValues_Correctly()
        {
            //Arrange
            var ticketJson = File.ReadAllText(AppContext.BaseDirectory + "/Converters/testTicketNull.json");

            //Act
            var ticket = JsonSerializer.Deserialize<Ticket>(ticketJson, ZendeskJson.Options);

            //Assert
            AssertDeserializationWasSuccessful(ticket, true);
        }

        [Fact]
        public void CustomField_Serializes_MultiValues_Correctly()
        {
            //Arrange
            var ticket = new Ticket()
            {
                CustomFields = new CustomFields()
                {
                    new CustomField() {Id = 27612842, Value = "festive_delivery_request"},
                    new CustomField() {Id = 360000027769, Values = new List<string>() { "fd_1st_january", "fd_2nd_january"}}
                }
            };

            //Act
            var ticketJson = JsonSerializer.Serialize(ticket, ZendeskJson.Options);
            var deserializedTicket = JsonSerializer.Deserialize<Ticket>(ticketJson, ZendeskJson.Options);

            //Assert
            AssertDeserializationWasSuccessful(deserializedTicket);
        }

        [Fact]
        public void CustomField_Serializes_Null_Correctly()
        {
            //Arrange
            var ticket = new Ticket()
            {
                CustomFields = new CustomFields()
                {
                    new CustomField() {Id = 27612842, Value = null},
                    new CustomField() {Id = 360000027769, Values = new List<string>() { "fd_1st_january", "fd_2nd_january"}}
                }
            };

            //Act
            var ticketJson = JsonSerializer.Serialize(ticket, ZendeskJson.Options);
            var deserializedTicket = JsonSerializer.Deserialize<Ticket>(ticketJson, ZendeskJson.Options);

            //Assert
            AssertDeserializationWasSuccessful(deserializedTicket, true);
        }

        private void AssertDeserializationWasSuccessful(Ticket ticket, bool expectNullValue = false)
        {
            var multiValueCustomField = ticket.CustomFields.Where(x => x.Values != null && x.Value == null);
            var singleValueFields = ticket.CustomFields.Where(x => x.Value != null && x.Values == null);
            var nullValueFields = ticket.CustomFields.Where(x => x.Value == null && x.Values == null);

            Assert.Equal(2, ticket.CustomFields.Count);

            Assert.Single(multiValueCustomField);

            Assert.Equal(2, multiValueCustomField.First().Values.Count);
            Assert.Contains(multiValueCustomField.First().Values, x => x == "fd_1st_january");
            Assert.Contains(multiValueCustomField.First().Values, x => x == "fd_2nd_january");

            Assert.Equal(expectNullValue ? 0 : 1, singleValueFields.Count());
            Assert.Equal(expectNullValue ? 1 : 0, nullValueFields.Count());
        }
    }
}
