using Microsoft.Extensions.DependencyInjection;
using Services.Bookmarks;
using Services.Comments;
using Services.Likes;
using Services.Quotes;
using Services.Users;

namespace Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IQuoteService, QuoteService>();
            services.AddTransient<ILikeService, LikeService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IBookmarkService, BookmarkService>();

            return services;
        }
    }
}