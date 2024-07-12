﻿using App.Services.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.Winform.UI
{
    public partial class Form_DangNhap : Form
    {
        Login_Services loginsv;
        public Form_DangNhap()
        {
            InitializeComponent();
            loginsv = new Login_Services();
        }

        private void btn_DangNhap_Click(object sender, EventArgs e)
        {
            string us = tbx_usn.Text;
            string pw = tbx_pass.Text;

            if (loginsv.GetUS_PW(us, pw) != null)
            {
                Form_BanHang tt = new Form_BanHang(loginsv.GetUS_PW(us, pw));

                this.Hide();

                tt.Show();



            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại !");
            }
        }

        private void Form_DangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_DangNhap_Click(sender, e);
            }
        }

        private void tbx_usn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbx_pass.Focus();
            }
        }
    }
}
