using Arcturus.Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Arcturus.Application.BlogPosts.Commands
{

	public class CreateBlogCommand : IRequest<int>
	{
		#region Public members
		public string Title { get; set; }
		public string Description { get; set; }
		public string Content { get; set; }
		#endregion

		#region Validator
		public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
		{
			public CreateBlogCommandValidator()
			{
				RuleFor(a => a.Title).NotNull().MaximumLength(150);
				RuleFor(a => a.Description).MaximumLength(250);
				RuleFor(a => a.Description).NotNull();
			}
		}
		#endregion



		#region Handler
		public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, int>
		{
			private readonly IMediator mediator;
			private readonly IArcturusDbContext dbContext;
			private readonly ICurrentAppUser currentUser;

			public CreateBlogCommandHandler(IMediator mediator, IArcturusDbContext dbContext, ICurrentAppUser currentUser)
			{
				this.mediator = mediator;
				this.dbContext = dbContext;
				this.currentUser = currentUser;
			}

			public async Task<int> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
			{
				var _model = new BlogPost
				{
					AuthorID = currentUser.ID,
					Title = request.Title,
					Description = request.Description,
					Content = request.Description,
					PublishedOn = DateTime.UtcNow
				};


				dbContext.BlogPosts.Add(_model);

				await dbContext.SaveChangesAsync(cancellationToken);

				return _model.ID;
			}
		}
		#endregion
	}
}
