using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using Arcturus.Interfaces;
using Arcturus.Domain.Entities;

/*
Do not modify this file! This is auto generated!
Any changes to this file will be gone when template gets generated again.
*/

namespace Arcturus.Infrastructure.Persistence
{
	public partial class ArcturusDbContext : DbContext, IArcturusDbContext
	{
		#region Entities
		private DbSet<Author> db_Authors { get; set; }
		public IQueryable<Author> Authors 
		{ 
			get { return db_Authors; }
			private set { db_Authors = (DbSet<Author>)value; }
		}
		private DbSet<BlogPost> db_BlogPosts { get; set; }
		public IQueryable<BlogPost> BlogPosts 
		{ 
			get { return db_BlogPosts; }
			private set { db_BlogPosts = (DbSet<BlogPost>)value; }
		}
		private DbSet<User> db_Users { get; set; }
		public IQueryable<User> Users 
		{ 
			get { return db_Users; }
			private set { db_Users = (DbSet<User>)value; }
		}
        #endregion

		public ArcturusDbContext(DbContextOptions<ArcturusDbContext> dbContextOpt) : base(dbContextOpt)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
	}
}

namespace Arcturus.Infrastructure.Persistence.Configurations
{
	public partial class Author_Configuration : BaseConfiguration<Author> { }
	public partial class BlogPost_Configuration : BaseConfiguration<BlogPost> { }
	public partial class User_Configuration : BaseConfiguration<User> { }
}
