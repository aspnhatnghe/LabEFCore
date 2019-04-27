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
                .Select(hh => new HangHoaView
                {
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
            if (hh != null)
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

        #region Sorting and Paging
        public IActionResult SapXep(string orderStr)
        {
            if (string.IsNullOrEmpty(orderStr))
            {
                orderStr = "TenHh_Asc";
            }
            var dsHangHoa = ctx.HangHoas.AsQueryable();
            switch (orderStr)
            {
                case "TenHh_Asc":
                    dsHangHoa = dsHangHoa.OrderBy(p => p.TenHh);
                    break;
                case "TenHh_Desc":
                    dsHangHoa = dsHangHoa.OrderByDescending(p => p.TenHh);
                    break;
                case "DonGia_Asc":
                    dsHangHoa = dsHangHoa.OrderBy(p => p.DonGia); break;
                case "DonGia_Desc":
                    dsHangHoa = dsHangHoa.OrderByDescending(p => p.DonGia); break;
            }
            ViewBag.OrderType = orderStr;

            return View(dsHangHoa.Select(hh => new HangHoaView
            {
                MaHh = hh.MaHh,
                TenHh = hh.TenHh,
                Hinh = hh.Hinh,
                DonGia = hh.DonGia
            }));
        }

        public IActionResult SapXepTongHop()
        {
            var dsHangHoa = ctx.HangHoas
                .OrderByDescending(p => p.DonGia)
                .ThenBy(p => p.TenHh);
            return View("SapXep", dsHangHoa.Select(hh => new HangHoaView
            {
                MaHh = hh.MaHh,
                TenHh = hh.TenHh,
                Hinh = hh.Hinh,
                DonGia = hh.DonGia
            }));
        }

        const int PAGE_SIZE = 10;
        public IActionResult PhanTrang(int? page)
        {
            if (page == null) page = 1;
            //bỏ n phần tử đầu
            int skipN = (page.Value - 1) * PAGE_SIZE;

            var dsHangHoa = ctx.HangHoas
                .OrderByDescending(p => p.TenHh)
                .Skip(skipN)
                .Take(PAGE_SIZE);
            return View("SapXep", dsHangHoa.Select(hh => new HangHoaView
            {
                MaHh = hh.MaHh,
                TenHh = hh.TenHh,
                Hinh = hh.Hinh,
                DonGia = hh.DonGia
            }));
        }
        #endregion

        #region ThongKe, GomNhom
        public IActionResult ThongKe()
        {
            var thongKe = ctx.HangHoas
                .GroupBy(p => p.Loai)
                .Select(hh => new
                {
                    hh.Key.MaLoai,
                    TenLoai = hh.Key.TenLoaiVn,
                    SoLuongMatHang = hh.Count(),
                    GiaLonNhat = hh.Max(p => p.DonGia),
                    TongSoHangHoa = hh.Sum(p => p.SoLuong)
                });

            return Json(thongKe);
        }

        public IActionResult DoanhThuTheoLoai()
        {
            var thongKe = from ctdh in ctx.ChiTietDonHangs
                          group ctdh by ctdh.HangHoa.Loai into g
                          select new
                          {
                              g.Key.MaLoai,
                              TenLoai = g.Key.TenLoaiVn,
                              DoanhThu = g.Sum(p => p.SoLuong * p.DonGia * (1 - p.GiamGia))
                          };
            return Json(thongKe);
        }

        public IActionResult DoanhThuTheoNhaCC()
        {
            var thongKe = from ctdh in ctx.ChiTietDonHangs
                          group ctdh by ctdh.HangHoa.NhaCungCap into g
                          select new
                          {
                              g.Key.MaNcc,
                              g.Key.TenNcc,
                              DoanhThu = g.Sum(p => p.SoLuong * p.DonGia * (1 - p.GiamGia))
                          };
            return Json(thongKe);
        }

        public IActionResult DoanhThuTheoThangNam(int thang, int nam)
        {
            var thongKe = from ctdh in ctx.ChiTietDonHangs
                          group ctdh by new
                          {
                              Nam = ctdh.DonHang.NgayDatHang.Year,
                              Thang = ctdh.DonHang.NgayDatHang.Month,
                          } into g
                          select new
                          {
                              ThoiGian = $"{g.Key.Thang}/{g.Key.Nam}",
                              DoanhThu = g.Sum(p => p.SoLuong * p.DonGia * (1 - p.GiamGia))
                          };


            return Json(thongKe);

        }
        #endregion
    }
}