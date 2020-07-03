using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Core;

namespace OnlineShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Route("Admin/Comments/List/{productId}")]
        public IActionResult List(int productId)
        {
            var comments = _unitOfWork.CommentRepository.Find(x => x.ProductId == productId, new[] {"Member"}).ToList();
            return PartialView("_CommentsDetails",comments);
        }
        [Route("Admin/Comments/Delete/{commentId}")]
        public IActionResult Delete(int commentId)
        {
            var url = HttpContext.Request.Path;
            var comment = _unitOfWork.CommentRepository.Get(commentId, new string[0]);
            _unitOfWork.CommentRepository.Delete(comment);
            _unitOfWork.Complete();
            return Redirect(url.ToString());
        }

    }
}