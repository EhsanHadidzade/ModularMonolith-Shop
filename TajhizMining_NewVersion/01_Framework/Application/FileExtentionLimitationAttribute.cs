using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Framework.Application
{
    public class FileExtentionLimitationAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string[] _validExtentions;

        public FileExtentionLimitationAttribute(string[] validExtentions)
        {
            _validExtentions = validExtentions;
        }

        public override bool IsValid(object? value)
        {
            var file=value as IFormFile;

            if (file == null) return true;

            var fileextention = Path.GetExtension(file.FileName);
            if (_validExtentions.Contains(fileextention))
            {
                return true;
            }
            return false;
            
        }
        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val-FileExtentionLimit", ErrorMessage);
        }
    }
}
