using _01_Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentManagement.Domain.Comment
{
    public class Comment:EntityBase
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string? Website { get; private set; }
        public string Message { get; private set; }
        public bool IsConfirmed { get; private set; }

        //To Get The Record id Of Owner that is e product or article
        public long OwenerRecordId { get; private set; }

        //To Find What is The owner . product or article
        public int Type { get; private set; }

        //Relation
        public long ParentId { get; private set; }
        public Comment Parent { get; private set; }

        public Comment(string name, string email, string website, string message, long owenerRecordId, int type, long parentId)
        {
            Name = name;
            Email = email;
            Website = website;
            Message = message;
            OwenerRecordId = owenerRecordId;
            Type = type;
            ParentId = parentId;
            IsConfirmed = false;
        }
        public void Confirm()
        {
            IsConfirmed = true;
        }
        public void Cancel()
        {
            IsConfirmed= false;
        }
    }
}
