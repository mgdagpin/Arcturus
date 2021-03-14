using Arcturus.Domain.Entities;
using TasqR;

namespace Arcturus.Queries.BlogPostQrs
{
    public class FindBlogQr : ITasq<BlogPost>
    {
        public FindBlogQr(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
