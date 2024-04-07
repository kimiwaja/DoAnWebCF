using System;
using System.Collections.Generic;

namespace WebCF.Data;

public partial class SanPham
{
    public int MaHh { get; set; }

    public string TenHh { get; set; } = null!;

    public string? TenAlias { get; set; }

    public int MaLoai { get; set; }

    public double? DonGia { get; set; }

    public string? Hinh { get; set; }

    public DateTime NgaySx { get; set; }

    public double GiamGia { get; set; }

    public string? MoTa { get; set; }


    public virtual ICollection<ChiTietHd> ChiTietHds { get; set; } = new List<ChiTietHd>();

    public virtual Loai MaLoaiNavigation { get; set; } = null!;



    public virtual ICollection<YeuThich> YeuThiches { get; set; } = new List<YeuThich>();
}
