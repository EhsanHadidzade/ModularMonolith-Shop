using _01_Framework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentManagement.Application.Contract.Comment
{
    public interface ICommentApplication
    {
        OperationResult Add(AddComment command);
        OperationResult Confirm(long commentId);
        OperationResult Cancel(long commentId);
        List<CommentViewModel> Search(CommentSearchModel searchModel);

    }
}
