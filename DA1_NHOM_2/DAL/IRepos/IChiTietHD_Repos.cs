﻿using App.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.IRepos
{
	public interface IChiTietHD_Repos
	{
		public List<ChiTietHoaDon> GetAll(Guid mahd);

		public void Create(ChiTietHoaDon cthd);

		public void Update(ChiTietHoaDon cthd);

		public void Delete(ChiTietHoaDon cthd);	

    }
}
