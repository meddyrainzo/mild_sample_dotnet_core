using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models.Bookmarks;

namespace Services.Bookmarks
{
    public interface IBookmarkService
    {
        /// <summary>
        /// Gets a paged list of bookmarks for the user 
        /// It defaults to 10 bookmarks returned
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="skip"></param>
        /// <param name="limit"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<GetBookmarkDTO>> GetBookmarks(int userid, int skip = 0, int limit = 10, CancellationToken token = default);

        /// <summary>
        /// Create a bookmark.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<int> CreateBookmark(CreateBookmarkDTO dto, CancellationToken token = default);

        /// <summary>
        /// Remove the bookmark
        /// NOTE: Any user can remove any bookmark. There is no validation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<int> RemoveBookmark(int id, CancellationToken token = default);
    }
}