using _01_Framework.Application;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Domain.Comment;

namespace CommentManagement.Application
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public OperationResult Add(AddComment command)
        {
            var operation =new OperationResult();
            var comment=new Comment(command.Name,command.Email,command.Website,command.Message,command.OwnerRecordId,command.Type,command.ParentId);

            _commentRepository.Create(comment);
            _commentRepository.Save();

            return operation.Succedded();
        }

        public OperationResult Confirm(long commentId)
        {
            var operation = new OperationResult();

            var comment=_commentRepository.Get(commentId);
            if(comment==null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            comment.Confirm();
            _commentRepository.Save();
            return operation.Succedded();
        }

        public OperationResult Cancel(long commentId)
        {
            var operation = new OperationResult();

            var comment = _commentRepository.Get(commentId);
            if (comment == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            comment.Cancel();
            _commentRepository.Save();
            return operation.Succedded();
        }


        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            return _commentRepository.Search(searchModel);
        }
    }
}
