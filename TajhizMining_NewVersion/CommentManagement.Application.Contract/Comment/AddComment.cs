using _01_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace CommentManagement.Application.Contract.Comment
{
    public class AddComment
    {
        public long Id { get; set; }

        [Required(ErrorMessage =ValidationMessage.IsRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Message { get; set; }

        public string Website { get; set; }
        public long OwnerRecordId { get; set; }
        public int Type { get; set; }
        public long ParentId { get; set; }
    }
}
