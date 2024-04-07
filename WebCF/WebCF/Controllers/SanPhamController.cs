using Microsoft.AspNetCore.Mvc;
//using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using WebCF.Data;
using WebCF.ViewModels;

namespace WebCF.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly WebCFContext db;
        public SanPhamController (WebCFContext context)
        {
            db = context;
        }
        public IActionResult Index( int? loai)
        {
            var sanPhams = db.SanPhams.AsQueryable();
            if (loai.HasValue)
            {
                sanPhams = sanPhams.Where(p => p.MaLoai == loai.Value);
            }
            var result = sanPhams.Select(p => new SanPhamVM
            {
                MaSP = p.MaHh,
                TenSP = p.TenHh,
                DonGia = p.DonGia ?? 0,
                Hinh = p.Hinh ?? "",
                MoTa = p.MoTa ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai
            });
            return View(result);
        }
        public IActionResult Search (string? query)
        {
            var sanPhams = db.SanPhams.AsQueryable();
            if (query != null)
            {
                sanPhams = sanPhams.Where(p => p.TenHh.Contains(query));
            }
            var result = sanPhams.Select(p => new SanPhamVM
            {
                MaSP = p.MaHh,
                TenSP = p.TenHh,
                DonGia = p.DonGia ?? 0,
                Hinh = p.Hinh ?? "",
                MoTa = p.MoTa ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai
            });
            return View(result);
        }

        public IActionResult Detail(int id )
        {
            var data = db.SanPhams
                .Include(p => p.MaLoaiNavigation)
                .SingleOrDefault(p => p.MaHh == id);
            if (data == null)
            {
                
                return NotFound();
            }
            var result = new ChiTietSPVM
            {
                MaSP = data.MaHh,
                TenSP = data.TenHh,
                DonGia = data.DonGia ?? 0,

                Hinh = data.Hinh ?? string.Empty,
                MoTa = data.MoTa ?? string.Empty,
                TenLoai = data.MaLoaiNavigation.TenLoai,

            };
             return View(result);
        }
    }
}
