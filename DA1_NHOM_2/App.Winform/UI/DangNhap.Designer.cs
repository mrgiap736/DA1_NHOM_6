﻿namespace App.Winform.UI
{
    partial class Form_DangNhap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_DangNhap));
            pn_FormLogin = new Panel();
            btn_ForgotPass = new Button();
            pn_passicon = new Panel();
            pn_iconus = new Panel();
            tbx_pass = new TextBox();
            label2 = new Label();
            btn_DangNhap = new Button();
            tbx_usn = new TextBox();
            label1 = new Label();
            pn_FormLogin.SuspendLayout();
            SuspendLayout();
            // 
            // pn_FormLogin
            // 
            pn_FormLogin.BackgroundImage = (Image)resources.GetObject("pn_FormLogin.BackgroundImage");
            pn_FormLogin.BackgroundImageLayout = ImageLayout.Stretch;
            pn_FormLogin.Controls.Add(btn_ForgotPass);
            pn_FormLogin.Controls.Add(pn_passicon);
            pn_FormLogin.Controls.Add(pn_iconus);
            pn_FormLogin.Controls.Add(tbx_pass);
            pn_FormLogin.Controls.Add(label2);
            pn_FormLogin.Controls.Add(btn_DangNhap);
            pn_FormLogin.Controls.Add(tbx_usn);
            pn_FormLogin.Controls.Add(label1);
            pn_FormLogin.Location = new Point(0, 0);
            pn_FormLogin.Name = "pn_FormLogin";
            pn_FormLogin.Size = new Size(482, 253);
            pn_FormLogin.TabIndex = 8;
            // 
            // btn_ForgotPass
            // 
            btn_ForgotPass.BackColor = Color.FromArgb(255, 128, 0);
            btn_ForgotPass.Font = new Font("Times New Roman", 11F);
            btn_ForgotPass.ForeColor = Color.White;
            btn_ForgotPass.Location = new Point(236, 182);
            btn_ForgotPass.Name = "btn_ForgotPass";
            btn_ForgotPass.Size = new Size(150, 40);
            btn_ForgotPass.TabIndex = 15;
            btn_ForgotPass.Text = "Quên mật khẩu";
            btn_ForgotPass.UseVisualStyleBackColor = false;
            btn_ForgotPass.Click += btn_ForgotPass_Click;
            // 
            // pn_passicon
            // 
            pn_passicon.BackColor = Color.Transparent;
            pn_passicon.BackgroundImage = (Image)resources.GetObject("pn_passicon.BackgroundImage");
            pn_passicon.BackgroundImageLayout = ImageLayout.Stretch;
            pn_passicon.Location = new Point(65, 132);
            pn_passicon.Name = "pn_passicon";
            pn_passicon.Size = new Size(30, 27);
            pn_passicon.TabIndex = 14;
            // 
            // pn_iconus
            // 
            pn_iconus.BackColor = Color.Transparent;
            pn_iconus.BackgroundImage = (Image)resources.GetObject("pn_iconus.BackgroundImage");
            pn_iconus.BackgroundImageLayout = ImageLayout.Stretch;
            pn_iconus.ForeColor = SystemColors.ActiveCaptionText;
            pn_iconus.Location = new Point(65, 67);
            pn_iconus.Name = "pn_iconus";
            pn_iconus.Size = new Size(30, 27);
            pn_iconus.TabIndex = 13;
            // 
            // tbx_pass
            // 
            tbx_pass.Location = new Point(101, 132);
            tbx_pass.Name = "tbx_pass";
            tbx_pass.PasswordChar = '*';
            tbx_pass.PlaceholderText = "Nhập mật khẩu";
            tbx_pass.Size = new Size(305, 27);
            tbx_pass.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.ForeColor = Color.White;
            label2.Location = new Point(101, 109);
            label2.Name = "label2";
            label2.Size = new Size(70, 20);
            label2.TabIndex = 11;
            label2.Text = "Mật khẩu";
            // 
            // btn_DangNhap
            // 
            btn_DangNhap.BackColor = Color.FromArgb(255, 128, 0);
            btn_DangNhap.Font = new Font("Times New Roman", 11F);
            btn_DangNhap.ForeColor = Color.White;
            btn_DangNhap.Location = new Point(120, 182);
            btn_DangNhap.Name = "btn_DangNhap";
            btn_DangNhap.Size = new Size(110, 40);
            btn_DangNhap.TabIndex = 12;
            btn_DangNhap.Text = "Đăng nhập";
            btn_DangNhap.UseVisualStyleBackColor = false;
            btn_DangNhap.Click += btn_DangNhap_Click;
            // 
            // tbx_usn
            // 
            tbx_usn.Location = new Point(101, 67);
            tbx_usn.Name = "tbx_usn";
            tbx_usn.PlaceholderText = "Nhập tài khoản";
            tbx_usn.Size = new Size(305, 27);
            tbx_usn.TabIndex = 9;
            tbx_usn.KeyDown += tbx_usn_KeyDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.White;
            label1.Location = new Point(101, 44);
            label1.Name = "label1";
            label1.Size = new Size(71, 20);
            label1.TabIndex = 8;
            label1.Text = "Tài khoản";
            // 
            // Form_DangNhap
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(482, 253);
            Controls.Add(pn_FormLogin);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "Form_DangNhap";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập";
            Click += btn_DangNhap_Click;
            KeyDown += Form_DangNhap_KeyDown;
            pn_FormLogin.ResumeLayout(false);
            pn_FormLogin.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pn_FormLogin;
        private Button btn_ForgotPass;
        private Panel pn_passicon;
        private Panel pn_iconus;
        private TextBox tbx_pass;
        private Label label2;
        private Button btn_DangNhap;
        private TextBox tbx_usn;
        private Label label1;
    }
}