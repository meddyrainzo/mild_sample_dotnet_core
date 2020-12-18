using Models.Bookmarks;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Bookmarks
{
    public interface IBookmarkRepository
    {
        Task<GetBookmarkDTO> GetBookmarkById(int id, CancellationToken token = default);
        Task<IEnumerable<GetBookmarkDTO>> GetBookmarks(int userid, int skip, int limit, CancellationToken token = default);
        Task<int> CreateBookmark(CreateBookmarkRequest request, CancellationToken token = default);
        Task<int> RemoveBookmark(int id, CancellationToken token = default);
    }
}