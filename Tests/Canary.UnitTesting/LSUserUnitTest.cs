using Canary.Application;
using Canary.UnitTesting.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canary.UnitTesting
{
    [TestClass]
    public class LSUserUnitTest 
    {
        [TestMethod]
        public void MyTestMethod()
        {
            using (var _services = TestServiceProvider.InSQLContext(svc => svc.AddScoped<ICurrentAppUser>(a => new TestCurrentUser(-1, "test"))))
            {
                var _dbContext = _services.GetService<ICanaryDbContext>();


                var _result = _dbContext.LSUsers.Any();

                Assert.IsTrue(_result, "No data");
            }
        }
    }
}
