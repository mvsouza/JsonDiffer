using System;
using JsonDiffer.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace JsonDiffer.FunctionalTest.Setup
{
    public class JsonDifferScenarioBase
    {
        private const string ApiUrlBase = "v1";

        public static IWebHostBuilder BuildWebHost() =>
            Program.CreateWebHostBuilder(null);

        public TestServer CreateServer()
        {
            return new TestServer(BuildWebHost());
        }

        public TestServer CreateServer(IWebHostBuilder buildWebHost)
        {
            return new TestServer(buildWebHost);
        }

        public static class Post
        {
            public static string Diff(string id)
            {
                return $"{ApiUrlBase}/diff/{id}";
            }

            public static string DiffLeft(string id)
            {
                return $"{Diff( id)}/left";
            }

            public static string DiffRight(string id)
            {
                return $"{Diff(id)}/right";
            }

            internal static string Diff(object id)
            {
                throw new NotImplementedException();
            }
        }
    }
}
