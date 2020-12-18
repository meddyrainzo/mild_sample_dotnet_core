using Microsoft.Extensions.DependencyInjection;
using Repository.Bookmarks;
using Repository.Comments;
using Repository.Likes;
using Repository.Quotes;
using Repository.Users;

namespace Repository
{
    public static class DepenendencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IQuoteRepository, QuoteRepository>();
            services.AddTransient<ILikeRepository, LikeRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IBookmarkRepository, BookmarkRepository>();
            return services;
        }
    }
}