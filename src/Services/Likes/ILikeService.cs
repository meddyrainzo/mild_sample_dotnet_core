using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Models.Likes;

namespace Services.Likes
{
    public interface ILikeService
    {
        /// <summary>
        /// Like a quote. 
        /// If the user has already liked the quote, an unlike is triggered instead
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<int> LikeOrUnlike(CreateLikeDTO dto, CancellationToken token = default);
    }
}