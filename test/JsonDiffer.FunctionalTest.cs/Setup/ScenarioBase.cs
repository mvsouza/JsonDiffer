﻿using JsonDiffer.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace JsonDiffer.FunctionalTest.Setup
{
    public class MathSolverScenarioBase
    {
        private const string ApiUrlBase = "api/v1/Solve";

        public static IWebHostBuilder BuildWebHost() =>
            Program.CreateWebHostBuilder(null);

        public TestServer CreateServer()
        {
            return new TestServer(BuildWebHost());
        }


        public static class Post
        {
            public static string Solve()
            {
                return $"{ApiUrlBase}";
            }
        }
    }
}
