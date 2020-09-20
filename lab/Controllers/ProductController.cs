using lab.DAL.Data;
using lab.DAL.Entities;
using lab.Extensions;
using lab.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace lab.Controllers
{
    public class ProductController : Controller
    {
        ApplicationDbContext _context;

        int _pageSize;

        public ProductController(ApplicationDbContext context)
        {
            _pageSize = 3;
            _context = context;
        }

        [Route("Product")]
        [Route("Product/Page_{page}")]
        public IActionResult Index(int? group, int page = 1)
        {
            //Поместить список групп во ViewData 
            ViewData["Groups"] = _context.MusInstrumentGroups;
            //Получить id текущей группы и поместить в TempData
            ViewData["CurrentGroup"] = group ?? 0;

            var filteredItems = _context.MusInstruments.Where(item => !group.HasValue || item.GroupId == group.Value);

            var model = ListViewModel<MusInstrument>.GetModel(filteredItems, page, _pageSize);

            if (Request.IsAjaxRequest())
                return PartialView("_listpartial", model);
            else
                return View(model);
        }
    }
}
