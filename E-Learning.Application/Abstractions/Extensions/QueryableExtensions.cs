using E_Learning.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Application.Abstractions.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<GetAllDataResponse<TResponse>> ToPagedResponseAsync<TEntity, TResponse>(
          this IQueryable<TEntity> query,
          int pageNumber,
          int pageSize,
          Func<TEntity, TResponse> mapper)
        {
            var totalCount = await query.CountAsync();

            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new GetAllDataResponse<TResponse>
            {
                PageNumber = pageNumber,
                TotalDataCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data = data.Select(mapper).ToList()
            };
        }
    }
}
