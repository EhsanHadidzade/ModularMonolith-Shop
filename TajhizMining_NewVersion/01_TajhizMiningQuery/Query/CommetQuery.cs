using _01_Framework.Application;
using _01_TajhizMiningQuery.Contracts.Comment;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_TajhizMiningQuery.Query
{
    public class CommetQuery : ICommentQuery
    {
        private readonly CommentContext _context;

        public CommetQuery(CommentContext context)
        {
            _context = context;
        }

        public CommentQueryModel GetChildCommentWithParentId(long id)
        {
            var comment = _context.Comments.Include(x => x.Parent).Select(x => new CommentQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Message = x.Message,
                ParentId = x.ParentId,
                parentName = x.Parent.Name,
                CreationDate = x.CretionDate.ToFarsi(),

            }).FirstOrDefault(x => x.ParentId == id);

            if (comment == null)
                throw new Exception();

            return comment;
        }
    }
}
