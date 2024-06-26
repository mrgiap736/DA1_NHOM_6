﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Data.Entities
{
    public class ChiTietHoaDon
    {
        [Key]
        public Guid MaChiTietHoaDon { get; set; }

        public int SoLuong { get; set; }

        public int DonGia { get; set; }

        public virtual HoaDon? HoaDon { get; set; }

        public virtual SanPham? SanPham { get; set; }
    }
}


