using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabEFCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace LabEFCore.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly NhatNgheShopDbContext ctx;

        public HangHoaController(NhatNgheShopDbContext db)
        {
            ctx = db;
        }

        public IActionResult Index()
        {
            var dsHangHoa = ctx.HangHoas
                .Where(hh => hh.DonGia > 10);
            return View(dsHangHoa);
        }

        public IActionResult TenHangHoa()
        {
            var dsTenHH = from hh in ctx.HangHoas
                          select hh.TenHh;
            return View(dsTenHH);
        }

        public IActionResult DsHangHoa()
        {
            var dsTenHH = ctx.HangHoas
                .Select(hh => new { hh.TenHh, hh.DonGia });
            return Json(dsTenHH);
        }

        public IActionResult HienThi()
        {
            var dsHH = ctx.HangHoas
                .Select(hh => new HangHoaView {
                    MaHh = hh.MaHh,
                    TenHh = hh.TenHh,
                    DonGia = hh.DonGia,
                    Hinh = hh.Hinh
                });
            return View(dsHH);
        }

        public IActionResult Va()
        {
            var dsHh = ctx.HangHoas
                .Where(hh => hh.TenHh.Contains("an") && hh.DonGia > 100 && hh.SoLuong < 50)
                .Select(hh => new HangHoaView
                {
                    MaHh = hh.MaHh,
                    TenHh = hh.TenHh,
                    DonGia = hh.DonGia,
                    Hinh = hh.Hinh
                });

            return View("HienThi", dsHh);
        }

        public IActionResult Hoac()
        {
            var dsHh = ctx.HangHoas
                .Where(hh => hh.TenHh.Contains("an") || hh.DonGia > 100 || hh.SoLuong < 50)
                .Select(hh => new HangHoaView
                {
                    MaHh = hh.MaHh,
                    TenHh = hh.TenHh,
                    DonGia = hh.DonGia,
                    Hinh = hh.Hinh
                });

            return View("HienThi", dsHh);
        }

        public IActionResult Dau()
        {
            var hh = ctx.HangHoas.FirstOrDefault();
            if(hh != null)
            {
                return View("HangHoaView", hh);
            }
            return NotFound();
        }

        public IActionResult Cuoi()
        {
            var hh = ctx.HangHoas.LastOrDefault();
            if (hh != null)
            {
                return View("HangHoaView", hh);
            }
            return NotFound();
        }

        public IActionResult TimTheoMa(int id)
        {
            var hh = ctx.HangHoas.SingleOrDefault(h => h.MaHh == id);
            if (hh != null)
            {
                return View("HangHoaView", hh);
            }
            return NotFound();
        }
    }
}