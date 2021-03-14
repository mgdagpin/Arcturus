using Arcturus.Domain.Entities;
using TasqR;

namespace Arcturus.Commands.BlogPostCmds
{
    public class NewBlogPostCmd : ITasq<BlogPost>
    {
        public NewBlogPostCmd(string title, string description, string content)
        {
            Title = title;
            Description = description;
            Content = content;
        }

        public string Title { get; }
        public string Description { get; }
        public string Content { get; }
    }
}
