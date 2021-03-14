using Arcturus.Application.Common.Extensions;
using Arcturus.Commands.BlogPostCmds;
using Arcturus.Common.Exceptions;
using Arcturus.Domain.Entities;
using Arcturus.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TasqR;

namespace Arcturus.Application.Handlers.Commands.BlogPostCmds
{
    public class NewBlogPostCmdHandler : TasqHandlerAsync<NewBlogPostCmd, BlogPost>
    {
        private readonly ICurrentAppUser currentAppUser;
        private readonly IArcturusDbContext dbContext;
        private readonly DbContext dbContextWriter;

        public NewBlogPostCmdHandler(ICurrentAppUser currentAppUser, IArcturusDbContext dbContext)
        {
            this.currentAppUser = currentAppUser;
            this.dbContext = dbContext;
            dbContextWriter = dbContext as DbContext;
        }

        public override Task<BlogPost> RunAsync(NewBlogPostCmd request, CancellationToken cancellationToken = default)
        {
            BlogPost post = new BlogPost
            {
                AuthorID = currentAppUser.ID,
                Title = request.Title,
                Description = request.Description,
                Content = request.Description,
                PublishedOn = DateTime.UtcNow
            };

            dbContext.BlogPosts
                .AsDbSet()
                .Add(post);

            return dbContextWriter.SaveChangesAsync().ContinueWith(a =>
            {
                if (a.Result == 0) throw new ArcturusCommandErrorException();

                return post;
            });
        }

        public override Task BeforeRunAsync(NewBlogPostCmd tasq, CancellationToken cancellationToken = default)
        {
            var validator = new NewBlogPostCmdValidator();

            return validator.ValidateAsync(tasq).ContinueWith(a =>
            {
                var failures = a.Result.Errors;

                if (failures.Any())
                {
                    throw new ValidationException(failures);
                }
            });
        }
    }

    #region Validator
    public class NewBlogPostCmdValidator : AbstractValidator<NewBlogPostCmd>
    {
        public NewBlogPostCmdValidator()
        {
            RuleFor(a => a.Title).NotNull().MaximumLength(150);
            RuleFor(a => a.Description).MaximumLength(250);
            RuleFor(a => a.Description).NotNull();
        }
    } 
    #endregion
}
