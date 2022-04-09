using _01_Framework.Application;
using _01_Framework.Infrastructure;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Domain.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
    {
        private readonly CommentContext _commentcontext;

        public CommentRepository(CommentContext commentcontext):base(commentcontext)
        {
            _commentcontext = commentcontext;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _commentcontext.Comments.Select(c => new CommentViewModel
            {
                Id = c.Id,
                CommentDate = c.CretionDate.ToFarsi(),
                Email = c.Email,
                IsConfirmed = c.IsConfirmed,
                Message = c.Message,
                Name = c.Name,
                OwnerName = "Owner name here",
                OwnerRecordId = c.OwenerRecordId,
                Type = c.Type,
                Website = c.Website,
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));


            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(x => x.Email == searchModel.Email);

            return query.OrderByDescending(x=>x.Id).ToList();
        }   
    }
}
