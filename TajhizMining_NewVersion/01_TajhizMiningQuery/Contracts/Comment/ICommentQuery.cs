using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_TajhizMiningQuery.Contracts.Comment
{
    public interface ICommentQuery
    {
        CommentQueryModel GetChildCommentWithParentId(long id);
    }
}
