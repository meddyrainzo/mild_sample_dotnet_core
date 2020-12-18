using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models.Comments;

namespace Services.Comments
{
    public interface ICommentService
    {
        /// <summary>
        /// Gets comments of a quote. If the quote does not exist, throw a QuoterException
        /// </summary>
        /// <param name="quoteId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<GetCommentDTO>> GetComments(int quoteId, CancellationToken token = default);

        /// <summary>
        /// Create a comment for a quote. If the quote does not exist, throw a QuoterException
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<int> CreateComment(CreateCommentDTO dto, CancellationToken token = default);
    }
}