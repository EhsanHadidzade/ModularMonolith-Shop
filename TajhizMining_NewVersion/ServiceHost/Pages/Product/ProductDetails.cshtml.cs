using _01_TajhizMiningQuery.Contracts.Product;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages.Product
{
    public class ProductDetailsModel : PageModel
    {
        [BindProperty]
        public AddComment command { get; set; }

        public ProductQueryModel Product;
        public List<ProductQueryModel> RelatedProducts;

        private readonly IProductQuery _productQuery;
        private readonly ICommentApplication _commentApplication;
        public ProductDetailsModel(IProductQuery productQuery, ICommentApplication commentApplication)
        {
            _productQuery = productQuery;
            _commentApplication = commentApplication;
        }

        //id is The ProductSlug To use 
        public void OnGet(string id)
        {
            Product=_productQuery.GetProductDetailsByProductSlug(id);
            RelatedProducts=_productQuery.GetRelatedProductsByCategorySlug(Product.CategorySlug).Where(x=>x.Id!=Product.Id).ToList();
        }
        public IActionResult OnPost(string ProductSlug)
        {
           
            command.Type = CommentType.Product;
            var result=_commentApplication.Add(command);

            if (!result.IsSuccedded)
                throw new Exception();


            return RedirectToPage("./productdetails", new { id = ProductSlug });
        }
    }
}
