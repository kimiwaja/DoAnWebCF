using Microsoft.AspNetCore.Mvc;
using WebCF.Data;
using WebCF.ViewModels;

namespace WebCF.ViewComponents
{

    public class MenuLoaiViewComponent : ViewComponent
    {
        private readonly WebCFContext db;
        public MenuLoaiViewComponent(WebCFContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Loais.Select(lo => new MenuLoaiVM
            {
                MaLoai = lo.MaLoai,
                TenLoai = lo.TenLoai,

            }).OrderBy(p => p.TenLoai);

            return View(data);
        }
    }
}

