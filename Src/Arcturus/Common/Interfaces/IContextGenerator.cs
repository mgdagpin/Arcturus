using System;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Arcturus.Domain.Entities;

/*
Do not modify this file! This is auto generated!
Any changes to this file will be gone when template gets generated again.
*/

namespace Arcturus.Interfaces
{
	public interface IArcturusDbContext
	{
		#region Entities
		IQueryable<Author> Authors { get; }
		IQueryable<BlogPost> BlogPosts { get; }
		IQueryable<User> Users { get; }
        #endregion
	}
}

