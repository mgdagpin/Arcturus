using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Arcturus.Application.Common.Extensions
{
    public static class IQueryableExtensions
    {
        public static DbSet<T> AsDbSet<T>(this IQueryable<T> queryable) where T : class
        {
            return (DbSet<T>)queryable;
        }
    }
}
