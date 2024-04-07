using AutoMapper;
using WebCF.Data;
using WebCF.Helpers;
using WebCF.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace WebCF.Controllers
{
	public class KhachHangController : Controller
	{
		private readonly WebCFContext db;
		private readonly IMapper _mapper;

		public KhachHangController(WebCFContext context, IMapper mapper)
		{
			db = context;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult DangKy()
		{
			return View();
		}
		
		[HttpPost]
		public IActionResult DangKy(RegisterVM model, IFormFile Hinh)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var khachHang = _mapper.Map<KhachHang>(model);
					khachHang.RandomKey = MyUtil.GenerateRamdomKey();
					khachHang.MatKhau = model.MatKhau.ToMd5Hash(khachHang.RandomKey);
					khachHang.HieuLuc = true;//sẽ xử lý khi dùng Mail để active
					khachHang.VaiTro = 0;

					if (Hinh != null)
					{
						khachHang.Hinh = MyUtil.UploadHinh(Hinh, "KhachHang");
					}

					db.Add(khachHang);
					db.SaveChanges();
					return RedirectToAction("Index", "Home");
				}
				catch (Exception ex)
				{
					var mess = $"{ex.Message} shh";
				}
			}
			return View();
		}
		

		[HttpGet]
		public IActionResult DangNhap(string? ReturnUrl)
		{
			ViewBag.ReturnUrl = ReturnUrl;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> DangNhap(LoginVM model, string? ReturnUrl)
		{
			ViewBag.ReturnUrl = ReturnUrl;
			if (ModelState.IsValid)
			{
				var KhacHang = db.KhachHangs.SingleOrDefault(kh => kh.MaKh == model.LoginName);
				if (KhacHang == null)
				{
					ModelState.AddModelError("Lỗi", "Sai thông tin đăng nhập");
				}
				else
				{
					if(KhacHang.MatKhau != model.Password.ToMd5Hash(KhacHang.RandomKey))
					{
						ModelState.AddModelError("Lỗi", "Sai thông tin đăng nhập");
					}
					else
					{
						var claims = new List<Claim> { new Claim(ClaimTypes.Email, KhacHang.Email),
						               new Claim(ClaimTypes.Name, KhacHang.HoTen)
						};
					
					     var claimsIdentity = new ClaimsIdentity(claims,"login");
						var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

						await HttpContext.SignInAsync(claimsPrincipal);
						if (Url.IsLocalUrl(ReturnUrl))
						{
							return Redirect(ReturnUrl);
						}
						else
						{
							return Redirect("/");
						}
					} 					   
				}
			}
			return View();
		}
		[Authorize]
		public IActionResult Profile()
		{
			return View();
		}
		[Authorize]
		public async Task<IActionResult> DangXuat()
		{
			await HttpContext.SignOutAsync();
			return Redirect("/");
		}
	}

}
