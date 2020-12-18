using System.Threading;
using System.Threading.Tasks;
using Models.Likes;

namespace Repository.Likes
{
    public interface ILikeRepository
    {
        Task<int> Like(CreateLikeRequest request, CancellationToken token = default);
        Task<int> Unlike(UnlikeRequest request, CancellationToken token = default);

        Task<GetLikeDTO> GetLike(GetLikeRequest request, CancellationToken token = default);
    }
}