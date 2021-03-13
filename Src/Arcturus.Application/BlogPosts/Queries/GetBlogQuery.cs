using AutoMapper;
using AutoMapper.QueryableExtensions;
using Arcturus.Application.BlogPosts.Queries.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Arcturus.Application.BlogPosts.Queries
{


	public class GetBlogQueryQuery : IRequest<IQueryable<BlogPostBO>>
	{
		#region Public members

		#endregion

		#region Handler			
		public class GetBlogQueryQueryHandler : IRequestHandler<GetBlogQueryQuery, IQueryable<BlogPostBO>>
		{
			private readonly IMediator mediator;
			private readonly IArcturusDbContext dbContext;
			private readonly IMapper mapper;

			public GetBlogQueryQueryHandler(IMediator mediator, IArcturusDbContext dbContext, IMapper mapper)
			{
				this.mediator = mediator;
				this.dbContext = dbContext;
				this.mapper = mapper;
			}

			public async Task<IQueryable<BlogPostBO>> Handle(GetBlogQueryQuery request, CancellationToken cancellationToken)
			{
				var _result = dbContext.BlogPosts
					.ProjectTo<BlogPostBO>(mapper.ConfigurationProvider);

				return _result;
			}
		}
		#endregion
	}
}
