using System;

namespace ZendeskApi.Client.IntegrationTests.Settings
{
    public class ZendeskSettings
    {
        public string Url { get; }
        public string Username { get; }
        public string Token { get; }

        public ZendeskSettings()
        {
            Url = Environment.GetEnvironmentVariable("ZendeskApi_Credentials_Url");
            Username = Environment.GetEnvironmentVariable("ZendeskApi_Credentials_Username");
            Token = Environment.GetEnvironmentVariable("ZendeskApi_Credentials_Token");
            if( Url == null || Username == null || Token == null)
            {
                throw new Exception("One or more environment variables are missing. required: ZendeskApi_Credentials_Url, ZendeskApi_Credentials_Username, ZendeskApi_Credentials_Token");
            }
        }
    }
}
