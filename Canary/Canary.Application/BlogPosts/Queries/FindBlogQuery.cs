using AutoMapper;
using Canary.Application.BlogPosts.Queries.Models;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Canary.Application.BlogPosts.Queries
{


	public class FindBlogQuery : IRequest<BlogPostBO>
	{
		#region Public members
		public int BlogPostID { get; set; }
		#endregion

		#region Validator
		public class FindBlogQueryValidator : AbstractValidator<FindBlogQuery>
		{
			public FindBlogQueryValidator()
			{
				RuleFor(a => a.BlogPostID).NotEmpty();
			}
		}
		#endregion


		#region Handler			
		public class FindBlogQueryHandler : IRequestHandler<FindBlogQuery, BlogPostBO>
		{
			private readonly IMediator mediator;
			private readonly ICanaryDbContext dbContext;
			private readonly IMapper mapper;

			public FindBlogQueryHandler(IMediator mediator, ICanaryDbContext dbContext, IMapper mapper)
			{
				this.mediator = mediator;
				this.dbContext = dbContext;
				this.mapper = mapper;
			}

			public async Task<BlogPostBO> Handle(FindBlogQuery request, CancellationToken cancellationToken)
			{
				var _result = await dbContext.BlogPosts
						.FindAsync(request.BlogPostID);

				return mapper.Map<BlogPostBO>(_result);
			}
		}
		#endregion
	}
}
