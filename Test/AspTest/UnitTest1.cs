using AspNetCoreResult.Startup;

using System;

using Xunit;

namespace AspTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //RouteMatching.AddIgnoreRoute("/api/test/route");
            //RouteMatching.AddIgnoreRoute(" /dataApi/{everything}");
            RouteMatching.AddIgnoreRoute(@"~ \api*");
            //Assert.Equal(RouteMatching.CheckRoute("/api/test/route"), true);
            //Assert.Equal(RouteMatching.CheckRoute("/dataApi/test/get"), true);
            Assert.Equal(RouteMatching.CheckRoute("/api/data/test"), true);
        }
    }
}
