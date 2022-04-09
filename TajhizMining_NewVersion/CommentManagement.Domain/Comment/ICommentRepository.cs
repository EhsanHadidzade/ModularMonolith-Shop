using _01_Framework.Domain;
using CommentManagement.Application.Contract.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentManagement.Domain.Comment
{
    public interface ICommentRepository:IRepository<long,Comment>
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
