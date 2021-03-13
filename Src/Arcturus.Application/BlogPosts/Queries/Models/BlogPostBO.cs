using AutoMapper;
using Arcturus.Domain.Entities;
using System;

namespace Arcturus.Application.BlogPosts.Queries.Models
{
    public class BlogPostBO : IMapFrom<BlogPost>
    {
        public int AuthorID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime PublishedOn { get; set; }



        //public void Mapping(Profile profile)
        //{
        //    profile.CreateMap<BlogPost, BlogPostBO>()
        //        .ForMember(a => a.Author, a => a.MapFrom(b => b.Author.User));
        //}
    }
}
