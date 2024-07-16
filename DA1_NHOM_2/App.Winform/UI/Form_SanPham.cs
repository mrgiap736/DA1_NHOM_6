﻿using App.Data.Data;
using App.Data.Entities;
using App.Services.Services;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace App.Winform.UI
{
    public partial class Form_SanPham : Form
    {
        SanPham_Services _service;
        List<ChiTietSanPham> _listSP = new();
        Guid _idwhenclick;
        public Form_SanPham(NhanVien nv)
        {
            InitializeComponent();
            _service = new SanPham_Services();
            LoadGird(ApplyFilters());
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ///123///////2113232
            LoadComboBox();

            cbx_FillHangSX.SelectedItem = "Tất cả";
            cbx_FillMauSac.SelectedItem = "Tất cả";
            cbx_FillChatLieu.SelectedItem = "Tất cả";
            cbx_FillLoaiRen.SelectedItem = "Tất cả";
            cbx_TrangThai.SelectedItem = "Còn hàng";

        }

        public List<Control> GetCtrl()
        {
            List<Control> ctrls = new List<Control>();

            foreach (Control ctrl in pn_Form_SanPham.Controls)
            {
                ctrls.Add(ctrl);
            }
            return ctrls;
        }
        public void LoadGird(dynamic data)
        {
            dtgView.Rows.Clear();
            dtgView.ColumnCount = 13;
            dtgView.Columns[0].Name = "STT";
            dtgView.Columns[1].Name = "Tên Sản Phẩm";
            dtgView.Columns[2].Name = "Số lượng";
            dtgView.Columns[3].Name = "Loại sản phẩm";
            dtgView.Columns[4].Name = "Giá bán";
            dtgView.Columns[5].Name = "Chiều dài";
            dtgView.Columns[6].Name = "Cân nặng";
            dtgView.Columns[7].Name = "Chất liệu";
            dtgView.Columns[8].Name = "Màu sắc";
            dtgView.Columns[9].Name = "Loại ren";
            dtgView.Columns[10].Name = "Hãng sản xuất";
            dtgView.Columns[11].Name = "Trạng thái";
            dtgView.Columns[12].Name = "Mã sản phẩm ct";
            dtgView.Columns[12].Visible = false;

            _listSP = _service.GetAll();
            foreach (var sp in data)
            {
                int stt = _listSP.IndexOf(sp) + 1;
                string giaBanFormatted = sp.GiaBan.ToString("#,##0");
                dtgView.Rows.Add(stt, sp.SanPham.TenSanPham,
                    sp.SoLuong,
                    sp.SanPham.LoaiSanPham,
                    giaBanFormatted,
                    sp.ChieuDai,
                    sp.CanNang,
                    sp.ChatLieu.Name,
                    sp.MauSac.Name,
                    sp.LoaiRen.Name,
                    sp.HangSanXuat.Name,
                    sp.TrangThai,
                    sp.Id,
                    sp.HinhAnh
                    );
            }
            dtgView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        private void pn_LamMoi_Click(object sender, EventArgs e)
        {
            txt_SoLuong.Text = "";
            txt_TenSanPham.Text = "";
            txt_LoaiSp.Text = "";
            txt_ChieuDai.Text = "";
            txt_CanNang.Text = "";
            txt_GiaBan.Text = "";

            cbx_HangSX.Text = "";
            cbx_LoaiRen.Text = "";
            cbx_ChatLieu.Text = "";
            cbx_MauSac.Text = "";
            cbx_TrangThai.Text = "Còn hàng";

            txt_Search.Text = "";
            cbx_FillChatLieu.Text = "Tất cả";
            cbx_FillMauSac.Text = "Tất cả";
            cbx_FillLoaiRen.Text = "Tất cả";
            cbx_FillHangSX.Text = "Tất cả";

            ptb_Anh.Image = null;
        }

        private void LoadComboBox()
        {
            cbx_HangSX.Items.Clear();
            cbx_MauSac.Items.Clear();
            cbx_ChatLieu.Items.Clear();
            cbx_LoaiRen.Items.Clear();

            cbx_FillHangSX.Items.Clear();
            cbx_FillMauSac.Items.Clear();
            cbx_FillChatLieu.Items.Clear();
            cbx_FillLoaiRen.Items.Clear();

            cbx_FillHangSX.Items.Add("Tất cả");
            cbx_FillMauSac.Items.Add("Tất cả");
            cbx_FillChatLieu.Items.Add("Tất cả");
            cbx_FillLoaiRen.Items.Add("Tất cả");

            foreach (var item in _service.GetAllHangSanXuat())
            {
                cbx_HangSX.Items.Add(item.Name);
                cbx_FillHangSX.Items.Add(item.Name);
            }

            foreach (var item in _service.GetAllMauSac())
            {
                cbx_MauSac.Items.Add(item.Name);
                cbx_FillMauSac.Items.Add(item.Name);
            }

            foreach (var item in _service.GetAllLoaiRen())
            {
                cbx_LoaiRen.Items.Add(item.Name);
                cbx_FillLoaiRen.Items.Add(item.Name);
            }

            foreach (var item in _service.GetAllChatLieu())
            {
                cbx_ChatLieu.Items.Add(item.Name);
                cbx_FillChatLieu.Items.Add(item.Name);
            }
        }

        private void pn_ThemSP_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu nhập vào
            if (string.IsNullOrWhiteSpace(txt_SoLuong.Text) || string.IsNullOrWhiteSpace(txt_TenSanPham.Text) ||
                string.IsNullOrWhiteSpace(txt_LoaiSp.Text) || string.IsNullOrWhiteSpace(txt_ChieuDai.Text) ||
                string.IsNullOrWhiteSpace(txt_CanNang.Text) || string.IsNullOrWhiteSpace(txt_GiaBan.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin sản phẩm.", "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ptb_Anh.Image == null)
            {
                MessageBox.Show("Vui lòng tải ảnh sản phẩm.", "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra độ dài tên sản phẩm
            if (txt_TenSanPham.Text.Length > 50)
            {
                MessageBox.Show("Tên sản phẩm chỉ được phép nhập tối đa 50 ký tự.",
                                "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra giá nhập và giá bán có phải số không
            if (!int.TryParse(txt_GiaBan.Text.Replace(",", ""), out int giaBan))
            {
                MessageBox.Show("Giá nhập và giá bán phải là số.", "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // Kiểm tra giá nhập và giá bán có lớn hơn hoặc bằng 1,000 không
            if (giaBan < 1000)
            {
                MessageBox.Show(
                    "Giá nhập và giá bán phải lớn hơn hoặc bằng 1,000.", "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Chuẩn bị dữ liệu hình ảnh
            byte[] imageData = null;
            if (ptb_Anh.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    try
                    {
                        ptb_Anh.Image.Save(ms, ptb_Anh.Image.RawFormat);
                        ptb_Anh.BackgroundImage = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi lưu ảnh ");
                    }
                    imageData = ms.ToArray();
                }
            }

            //Check xem Sản phẩm đã tồn tại hay chưa nếu đã tồn tại thì 
            List<SanPham> lstSP = _service.GetAllSP();
            bool checkExist = true;


            foreach (var item in lstSP)
            {
                if (txt_TenSanPham.Text == item.TenSanPham)
                {
                    checkExist = false;
                }
            }

            if (checkExist)
            {

                var sp = new SanPham();
                sp.MaSanPham = Guid.NewGuid();
                sp.TenSanPham = txt_TenSanPham.Text;
                sp.LoaiSanPham = txt_LoaiSp.Text;

                //
                var ctsp = new ChiTietSanPham();

                ctsp.Id = Guid.NewGuid();
                ctsp.MaSanPham = sp.MaSanPham;
                ctsp.MaHangSanXuat = _service.GetIdHangSX(cbx_HangSX.Text);
                ctsp.MaMauSac = _service.GetIdMauSac(cbx_MauSac.Text);
                ctsp.MaChatLieu = _service.GetIdChatLieu(cbx_ChatLieu.Text);
                ctsp.MaLoaiRen = _service.GetIdLoaiRen(cbx_LoaiRen.Text);
                ctsp.SoLuong = Convert.ToInt32(txt_SoLuong.Text);
                ctsp.ChieuDai = Convert.ToDouble(txt_ChieuDai.Text);
                ctsp.CanNang = Convert.ToDouble(txt_CanNang.Text);
                ctsp.GiaBan = giaBan;
                ctsp.TrangThai = cbx_TrangThai.Text;
                ctsp.HinhAnh = imageData;

                // Hiển thị hộp thoại xác nhận
                var option = MessageBox.Show("Xác nhận muốn thêm sản phẩm?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (option == DialogResult.Yes)
                {
                    if (_service.Add(sp))
                    {
                        // Thêm sản phẩm vào cơ sở dữ liệu và xử lý kết quả
                        MessageBox.Show(_service.AddCTSP(ctsp));
                    }
                    else
                    {
                        MessageBox.Show("Thêm sản phẩm thất bại");
                    }

                    // Tải lại danh sách sản phẩm sau khi thêm thành công
                    LoadGird(ApplyFilters());
                }
                else
                {
                    return;
                }
            }
            else
            {
                SanPham sp = lstSP.FirstOrDefault(x => x.TenSanPham == txt_TenSanPham.Text);

                var ctsp = new ChiTietSanPham();

                ctsp.Id = Guid.NewGuid();
                ctsp.MaSanPham = sp.MaSanPham;
                ctsp.MaHangSanXuat = _service.GetIdHangSX(cbx_HangSX.Text);
                ctsp.MaMauSac = _service.GetIdMauSac(cbx_MauSac.Text);
                ctsp.MaChatLieu = _service.GetIdChatLieu(cbx_ChatLieu.Text);
                ctsp.MaLoaiRen = _service.GetIdLoaiRen(cbx_LoaiRen.Text);
                ctsp.SoLuong = Convert.ToInt32(txt_SoLuong.Text);
                ctsp.ChieuDai = Convert.ToDouble(txt_ChieuDai.Text);
                ctsp.CanNang = Convert.ToDouble(txt_CanNang.Text);
                ctsp.GiaBan = giaBan;
                ctsp.TrangThai = cbx_TrangThai.Text;
                ctsp.HinhAnh = imageData;

                // Hiển thị hộp thoại xác nhận
                var option = MessageBox.Show("Xác nhận muốn thêm sản phẩm?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (option == DialogResult.Yes)
                {
                    // Thêm sản phẩm vào cơ sở dữ liệu và xử lý kết quả
                    MessageBox.Show(_service.AddCTSP(ctsp));

                    // Tải lại danh sách sản phẩm sau khi thêm thành công
                    LoadGird(ApplyFilters());
                }
                else
                {
                    return;
                }
            }


        }



        private void pn_UpdateSP_Click(object sender, EventArgs e)
        {
            //// Kiểm tra đã chọn sản phẩm để cập nhật chưa  //cần sửa
            //if (_idwhenclick == null)
            //{
            //    MessageBox.Show("Vui lòng chọn sản phẩm muốn cập nhật.", "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //// Kiểm tra dữ liệu nhập vào và các điều kiện khác
            //if (string.IsNullOrWhiteSpace(txt_MaSanPham.Text) ||
            //    string.IsNullOrWhiteSpace(txt_TenSanPham.Text) ||
            //    string.IsNullOrWhiteSpace(txt_HangSanPham.Text) ||
            //    string.IsNullOrWhiteSpace(txt_ThongSoKyThuat.Text))
            //{
            //    MessageBox.Show("Vui lòng nhập đầy đủ thông tin sản phẩm.", "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //if (txt_MaSanPham.Text.Length < 3 || txt_MaSanPham.Text.Length > 10)
            //{
            //    MessageBox.Show("Mã sản phẩm phải có ít nhất 3 và tối đa 10 ký tự.",
            //                    "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //// Kiểm tra độ dài tên sản phẩm
            //if (txt_TenSanPham.Text.Length > 50)
            //{
            //    MessageBox.Show("Tên sản phẩm chỉ được phép nhập tối đa 50 ký tự.",
            //                    "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //// Kiểm tra độ dài hãng sản xuất
            //if (txt_HangSanPham.Text.Length > 20)
            //{
            //    MessageBox.Show("Hãng sản xuất chỉ được phép nhập tối đa 20 ký tự.",
            //                    "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //// Kiểm tra độ dài thông số kỹ thuật
            //if (txt_ThongSoKyThuat.Text.Length > 200)
            //{
            //    MessageBox.Show("Thông số kỹ thuật chỉ được phép nhập tối đa 200 ký tự.",
            //                    "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //// Kiểm tra giá nhập và giá bán có phải số không
            //if (!int.TryParse(txt_GiaNhap.Text.Replace(",", ""), out int giaNhap) ||
            //    !int.TryParse(txt_GiaBan.Text.Replace(",", ""), out int giaBan))
            //{
            //    MessageBox.Show("Giá nhập và giá bán phải là số.", "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //// Kiểm tra giá nhập và giá bán có lớn hơn hoặc bằng 1,000 không
            //if (giaNhap < 1000 || giaBan < 1000)
            //{
            //    MessageBox.Show("Giá nhập và giá bán phải lớn hơn hoặc bằng 1,000.", "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //// Kiểm tra xem mã sản phẩm có chứa ký tự đặc biệt hay không
            //string maSanPham = txt_MaSanPham.Text;
            //if (!Regex.IsMatch(maSanPham, @"^[a-zA-Z0-9]+$"))
            //{
            //    MessageBox.Show("Mã sản phẩm không được chứa ký tự đặc biệt.", "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //byte[] imageData = null;
            //// Kiểm tra xem người dùng đã chọn ảnh mới hay chưa
            //if (ptb_Anh.Image != null && !IsDefaultImage(ptb_Anh.Image))
            //{
            //    using (MemoryStream ms = new MemoryStream())
            //    {


            //        try
            //        {
            //            ptb_Anh.Image.Save(ms, ptb_Anh.Image.RawFormat);
            //            ptb_Anh.BackgroundImage = null;
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("Lỗi khi lưu ảnh ");
            //        }

            //        imageData = ms.ToArray();


            //    }
            //}
            //else
            //{
            //    // Nếu không chọn ảnh mới, giữ nguyên ảnh cũ từ dữ liệu sản phẩm
            //    // Thay vì gán giá trị null cho imageData, hãy lấy dữ liệu ảnh từ cơ sở dữ liệu hoặc dịch vụ của bạn
            //    // Ví dụ: imageData = _service.GetImageData(_idwhenclick);
            //    // hoặc imageData = GetImageDataFromDatabase(_idwhenclick);
            //}

            //var sp = new SanPham();
            //sp.MaSanPham = _idwhenclick;
            //sp.TenSanPham = txt_TenSanPham.Text;
            //sp.HangSanXuat = txt_HangSanPham.Text;
            //sp.ThongSoKyThuat = txt_ThongSoKyThuat.Text;
            //sp.GiaNhap = giaNhap;
            //sp.GiaBan = giaBan;

            //// Chỉ gán dữ liệu hình ảnh nếu có dữ liệu hình ảnh mới
            //if (imageData != null)
            //{
            //    sp.HinhAnh = imageData;
            //}

            //// Kiểm tra trạng thái sản phẩm
            //if (rd_ConHang.Checked)
            //{
            //    sp.TrangThai = 1;
            //}
            //else if (rd_HetHang.Checked)
            //{
            //    sp.TrangThai = 0;
            //}
            //else
            //{
            //    MessageBox.Show("Vui lòng chọn trạng thái sản phẩm.", "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //// Hiển thị hộp thoại xác nhận
            //var option = MessageBox.Show("Xác nhận muốn cập nhật sản phẩm?", "Xác nhận",
            //    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (option == DialogResult.Yes)
            //{
            //    // Thêm sản phẩm vào cơ sở dữ liệu và xử lý kết quả
            //    MessageBox.Show(_service.Update(sp));
            //    // Tải lại danh sách sản phẩm sau khi thêm thành công
            //    LoadGird(null);
            //}
            //else
            //{
            //    return;
            //}
        }


        // Phương thức kiểm tra xem ảnh có phải là ảnh mặc định hay không
        private bool IsDefaultImage(Image image)
        {
            // Đặc tả cách kiểm tra ảnh mặc định tại đây
            // Ví dụ:
            // return (image == defaultImage);
            return false; // Cập nhật logic kiểm tra ảnh mặc định ở đây
        }



        private void pn_XoaSP_Click(object sender, EventArgs e)
        {
            //// Kiểm tra xem có sản phẩm được chọn hay không
            //if (string.IsNullOrEmpty(_idwhenclick))
            //{
            //    MessageBox.Show("Vui lòng chọn sản phẩm muốn xoá.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //var sp = new SanPham();
            //sp.MaSanPham = _idwhenclick;
            //var option = MessageBox.Show("Xác nhận muốn xoá sản phẩm?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (option == DialogResult.Yes)
            //{
            //    MessageBox.Show(_service.Remove(sp));
            //    LoadGird(null);
            //}
            //else
            //{
            //    return;
            //}
        }



        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            LoadGird(null);
        }

        private void dtgView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex; //cần sửa
            if (index < 0 || index >= _listSP.Count)
            {
                return;
            }
            var obj = _listSP[index];
            _idwhenclick = obj.MaSanPham;
            txt_TenSanPham.Text = obj.SanPham.TenSanPham;
            cbx_HangSX.SelectedItem = obj.HangSanXuat.Name;
            cbx_MauSac.SelectedItem = obj.MauSac.Name;
            cbx_ChatLieu.SelectedItem = obj.ChatLieu.Name;
            cbx_LoaiRen.SelectedItem = obj.LoaiRen.Name;
            txt_SoLuong.Text = obj.SoLuong.ToString();
            txt_GiaBan.Text = obj.GiaBan.ToString();
            txt_ChieuDai.Text = obj.ChieuDai.ToString();
            txt_CanNang.Text = obj.CanNang.ToString();
            txt_LoaiSp.Text = obj.SanPham.LoaiSanPham.ToString();
            cbx_TrangThai.SelectedItem = obj.TrangThai.ToString();

            // Kiểm tra xem dữ liệu VARBINARY có tồn tại không
            if (obj.HinhAnh != null && obj.HinhAnh.Length > 0)
            {
                // Chuyển đổi dữ liệu byte thành hình ảnh
                using (MemoryStream ms = new MemoryStream(obj.HinhAnh))
                {
                    ptb_Anh.Image = Image.FromStream(ms);
                    ptb_Anh.BackgroundImage = null;
                }
            }
            else
            {
                // Nếu không có hình ảnh, gán hình mặc định hoặc hiển thị một thông báo khác
                ptb_Anh.Image = null; // Gán hình mặc định
                                      // Hoặc hiển thị một thông báo khác
                MessageBox.Show("Không có hình ảnh.");
            }


        }



        private void dtgView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (e.ColumnIndex == 13 && e.Value != null) // Kiểm tra nếu đây là cột hình ảnh và giá trị không null
            //{
            //    // Kiểm tra xem dữ liệu VARBINARY có tồn tại không
            //    byte[] imageData = (byte[])e.Value; // Cast giá trị của cột thành mảng byte hình ảnh
            //    if (imageData.Length > 0)
            //    {
            //        // Chuyển đổi dữ liệu byte thành hình ảnh
            //        using (MemoryStream ms = new MemoryStream(imageData))
            //        {
            //            // Gán hình ảnh vào ô cột tương ứng
            //            e.Value = Image.FromStream(ms);
            //            ;
            //        }
            //    }
            //    else
            //    {
            //        // Nếu không có hình ảnh, gán hình mặc định hoặc hiển thị một thông báo khác
            //        e.Value = null; // Gán hình mặc định hoặc null
            //    }
            //}
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Chức năng thêm ảnh chưa được hoàn thiện.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            // Tạo một hộp thoại mở tệp tin
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp|All files (*.*)|*.*";
            openFileDialog.Title = "Chọn ảnh sản phẩm";

            // Hiển thị hộp thoại mở tệp tin và kiểm tra kết quả
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Lấy đường dẫn đến tệp tin hình ảnh đã chọn
                    string imagePath = openFileDialog.FileName;

                    // Đọc hình ảnh từ đường dẫn và hiển thị trong PictureBox
                    ptb_Anh.Image = Image.FromFile(imagePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể tải hình ảnh: " + ex.Message);
                }
            }
        }
        private void ExportToExcel(List<SanPham> data)
        {
            //using (ExcelPackage excelPackage = new ExcelPackage()) //cần sửa
            //{
            //    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Danh sách sản phẩm");

            //    worksheet.Cells[1, 1].Value = "Mã Sản Phẩm";
            //    worksheet.Cells[1, 2].Value = "Tên Sản Phẩm";
            //    worksheet.Cells[1, 3].Value = "Hãng Sản Xuất";
            //    worksheet.Cells[1, 4].Value = "Thông Số Kỹ Thuật";
            //    worksheet.Cells[1, 5].Value = "Giá Nhập";
            //    worksheet.Cells[1, 6].Value = "Giá Bán";
            //    worksheet.Cells[1, 7].Value = "Trạng Thái";

            //    int row = 2;
            //    foreach (var sp in data)
            //    {
            //        worksheet.Cells[row, 1].Value = sp.MaSanPham;
            //        worksheet.Cells[row, 2].Value = sp.TenSanPham;
            //        worksheet.Cells[row, 3].Value = sp.HangSanXuat;
            //        worksheet.Cells[row, 4].Value = sp.ThongSoKyThuat;
            //        worksheet.Cells[row, 5].Value = sp.GiaNhap;
            //        worksheet.Cells[row, 6].Value = sp.GiaBan;
            //        worksheet.Cells[row, 7].Value = sp.TrangThai == 1 ? "Còn hàng" : "Hết hàng";

            //        row++;
            //    }

            //    // Lưu file Excel
            //    using (var stream = new MemoryStream())
            //    {
            //        excelPackage.SaveAs(stream);
            //        SaveFileDialog saveFileDialog = new SaveFileDialog();
            //        saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            //        saveFileDialog.FilterIndex = 1;
            //        saveFileDialog.RestoreDirectory = true;

            //        if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //        {
            //            if (Path.GetExtension(saveFileDialog.FileName).ToLower() != ".xlsx")
            //            {
            //                MessageBox.Show("Chỉ chấp nhận định dạng .xlsx cho tệp tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //            else
            //            {
            //                FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create);
            //                stream.WriteTo(fs);
            //                fs.Close();
            //            }
            //        }
            //    }
            //}
        }


        private void pn_XuatExcel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pn_XuatExcel_Click(object sender, EventArgs e)
        {
            //ExportToExcel(_listSP); //cần sửa
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pn_LamMoi_Click(sender, e);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            pn_LamMoi_Click(sender, e);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pn_ThemSP_Click(sender, e);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            pn_ThemSP_Click(sender, e);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pn_UpdateSP_Click(sender, e);
        }

        private void label10_Click(object sender, EventArgs e)
        {
            pn_UpdateSP_Click(sender, e);
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            pn_XoaSP_Click(sender, e);
        }

        private void label11_Click(object sender, EventArgs e)
        {
            pn_XoaSP_Click(sender, e);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            pn_XuatExcel_Click(sender, e);
        }

        private void label12_Click(object sender, EventArgs e)
        {
            pn_XuatExcel_Click(sender, e);
        }

        private void txt_GiaBan_TextChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem chuỗi có độ dài lớn hơn 0 không
            if (txt_GiaBan.Text.Length > 0)
            {
                // Lọc chuỗi chỉ giữ lại ký tự số và dấu "."
                string input = Regex.Replace(txt_GiaBan.Text, "[^0-9.]", "");

                // Kiểm tra xem sau khi lọc, chuỗi có độ dài lớn hơn 0 không
                if (input.Length > 0)
                {
                    // Kiểm tra xem chuỗi chỉ có duy nhất một dấu "."
                    if (input.Count(c => c == '.') <= 1)
                    {
                        // Chuyển đổi chuỗi thành số nguyên
                        if (decimal.TryParse(input, out decimal giaNhap))
                        {
                            // Định dạng số tiền và hiển thị lại trong textbox
                            txt_GiaBan.Text = giaNhap.ToString("#,##0");
                            // Di chuyển con trỏ về cuối textbox để người dùng có thể tiếp tục nhập
                            txt_GiaBan.SelectionStart = txt_GiaBan.Text.Length;
                        }
                        else
                        {
                            // Nếu người dùng nhập không phải là số, xóa ký tự vừa nhập và hiển thị thông báo
                            txt_GiaBan.Text = txt_GiaBan.Text.Substring(0, txt_GiaBan.Text.Length - 1);
                            MessageBox.Show("Vui lòng nhập số nguyên.");
                        }
                    }
                    else
                    {
                        // Nếu chuỗi chứa nhiều hơn một dấu ".", xóa ký tự vừa nhập và hiển thị thông báo
                        txt_GiaBan.Text = txt_GiaBan.Text.Substring(0, txt_GiaBan.Text.Length - 1);
                        MessageBox.Show("Số tiền không hợp lệ.");
                    }
                }
            }
        }

        private void pn_LamMoi_MouseEnter(object sender, EventArgs e)
        {

        }

        private void pn_LamMoi_MouseLeave(object sender, EventArgs e)
        {

        }

        private void txt_SoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập các ký tự số và điều khiển
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cbx_Fill_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGird(ApplyFilters());
        }

        private List<ChiTietSanPham> ApplyFilters()
        {
            Func<ChiTietSanPham, bool> dk1 = null;
            Func<ChiTietSanPham, bool> dk2 = null;
            Func<ChiTietSanPham, bool> dk3 = null;
            Func<ChiTietSanPham, bool> dk4 = null;

            if (cbx_FillHangSX.SelectedItem != null && cbx_FillHangSX.SelectedItem.ToString() != "Tất cả")
            {
                dk1 = sp => sp.MaHangSanXuat == _service.GetIdHangSX(cbx_FillHangSX.SelectedItem.ToString());
            }

            if (cbx_FillMauSac.SelectedItem != null && cbx_FillMauSac.SelectedItem.ToString() != "Tất cả")
            {
                dk2 = sp => sp.MaMauSac == _service.GetIdMauSac(cbx_FillMauSac.SelectedItem.ToString());
            }

            if (cbx_FillChatLieu.SelectedItem != null && cbx_FillChatLieu.SelectedItem.ToString() != "Tất cả")
            {
                dk3 = sp => sp.MaChatLieu == _service.GetIdChatLieu(cbx_FillChatLieu.SelectedItem.ToString());
            }

            if (cbx_FillLoaiRen.SelectedItem != null && cbx_FillLoaiRen.SelectedItem.ToString() != "Tất cả")
            {
                dk4 = sp => sp.MaLoaiRen == _service.GetIdLoaiRen(cbx_FillLoaiRen.SelectedItem.ToString());
            }

            var filterSP = _service.FillSP(_service.GetAll(), dk1, dk2, dk3, dk4);

            return filterSP;
        }

        //
        private void txt_SoLuong_TextChanged(object sender, EventArgs e)
        {
            CheckStatus(txt_SoLuong.Text);
        }

        private void cbx_TrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckStatus(txt_SoLuong.Text);
        }

        public void CheckStatus(string input)
        {
            if(int.TryParse(input, out int soluong))
            {
                if (soluong == 0)
                {
                    cbx_TrangThai.SelectedItem = "Hết hàng";
                }
                else
                {
                    cbx_TrangThai.SelectedItem = "Còn hàng";
                }
            }
            else
            {
                return;
            }
            
        }

        
    }

}

