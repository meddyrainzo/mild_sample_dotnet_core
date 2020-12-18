using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Comments;

namespace Repository.Comments
{
    public interface ICommentRepository
    {
        Task<IEnumerable<GetCommentDTO>> GetComments(int quoteId, CancellationToken token = default);
        Task<int> CreateComment(CreateCommentRequest request, CancellationToken token = default);
    }
}