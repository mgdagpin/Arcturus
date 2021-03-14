using Arcturus.Domain.Entities;
using Arcturus.Interfaces;
using Arcturus.Queries.BlogPostQrs;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TasqR;

namespace Arcturus.Application.Handlers.Queries.BlogPostQrs
{
    public class FindBlogQrHandler : TasqHandlerAsync<FindBlogQr, BlogPost>
    {
        private readonly IArcturusDbContext dbContext;

        public FindBlogQrHandler(IArcturusDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public override Task<BlogPost> RunAsync(FindBlogQr request, CancellationToken cancellationToken = default)
        {
            return dbContext.BlogPosts
                .SingleOrDefaultAsync(a => a.ID == request.Id)
                .ContinueWith(r =>
                {
                    return r.Result;
                });
        }
    }
}
