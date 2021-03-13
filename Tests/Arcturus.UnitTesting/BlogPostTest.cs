using Arcturus.Application;
using Arcturus.Application.BlogPosts.Commands;
using Arcturus.UnitTesting.Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Arcturus.UnitTesting
{
    [TestClass]
    public class BlogPostTest
    {
        [TestMethod]
        public async Task CreateBlogTest()
        {
            using (var _services = TestServiceProvider.InMemoryContext(svc => svc.AddScoped<ICurrentAppUser>(a => new TestCurrentUser(-1, "test"))))
            {
                var _mediator = _services.GetService<IMediator>();
                var _dbContext = _services.GetService<IArcturusDbContext>();

                var _result = await _mediator.Send(new CreateBlogCommand
                {
                    Title = "Lorem Ipsum",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor.",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
                });


                Assert.AreEqual(_result, 1);
            }
        }
    }
}
