namespace TennisCourtManager
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            panel1 = new Panel();
            ClosePb = new PictureBox();
            AdminLb = new Label();
            LoginBtn = new Button();
            PasswordTb = new TextBox();
            UserNameTb = new TextBox();
            label15 = new Label();
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ClosePb).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLightLight;
            panel1.Controls.Add(ClosePb);
            panel1.Controls.Add(AdminLb);
            panel1.Controls.Add(LoginBtn);
            panel1.Controls.Add(PasswordTb);
            panel1.Controls.Add(UserNameTb);
            panel1.Controls.Add(label15);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(776, 426);
            panel1.TabIndex = 0;
            // 
            // ClosePb
            // 
            ClosePb.Image = (Image)resources.GetObject("ClosePb.Image");
            ClosePb.Location = new Point(723, 14);
            ClosePb.Name = "ClosePb";
            ClosePb.Size = new Size(40, 39);
            ClosePb.SizeMode = PictureBoxSizeMode.Zoom;
            ClosePb.TabIndex = 9;
            ClosePb.TabStop = false;
            ClosePb.Click += ClosePb_Click;
            // 
            // AdminLb
            // 
            AdminLb.AutoSize = true;
            AdminLb.Font = new Font("Calibri", 10F, FontStyle.Underline, GraphicsUnit.Point, 238);
            AdminLb.Location = new Point(422, 341);
            AdminLb.Name = "AdminLb";
            AdminLb.Size = new Size(195, 24);
            AdminLb.TabIndex = 8;
            AdminLb.Text = "Kontynuuj jako Admin";
            AdminLb.Click += AdminLb_Click;
            // 
            // LoginBtn
            // 
            LoginBtn.BackColor = SystemColors.ActiveCaption;
            LoginBtn.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            LoginBtn.Location = new Point(430, 262);
            LoginBtn.Name = "LoginBtn";
            LoginBtn.Size = new Size(179, 49);
            LoginBtn.TabIndex = 7;
            LoginBtn.Text = "Zaloguj";
            LoginBtn.UseVisualStyleBackColor = false;
            LoginBtn.Click += LoginBtn_Click;
            // 
            // PasswordTb
            // 
            PasswordTb.BorderStyle = BorderStyle.FixedSingle;
            PasswordTb.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            PasswordTb.Location = new Point(384, 184);
            PasswordTb.Name = "PasswordTb";
            PasswordTb.PasswordChar = '*';
            PasswordTb.PlaceholderText = "Hasło";
            PasswordTb.Size = new Size(277, 37);
            PasswordTb.TabIndex = 6;
            // 
            // UserNameTb
            // 
            UserNameTb.BorderStyle = BorderStyle.FixedSingle;
            UserNameTb.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            UserNameTb.Location = new Point(384, 130);
            UserNameTb.Name = "UserNameTb";
            UserNameTb.PlaceholderText = "Nazwa Użytkownika";
            UserNameTb.Size = new Size(277, 37);
            UserNameTb.TabIndex = 5;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.BackColor = SystemColors.ControlLightLight;
            label15.Font = new Font("Calibri", 14F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label15.ForeColor = SystemColors.ActiveCaptionText;
            label15.Location = new Point(290, 69);
            label15.Name = "label15";
            label15.Size = new Size(464, 35);
            label15.TabIndex = 4;
            label15.Text = "System zarządzania kortami tenisowymi";
            label15.Click += label15_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(24, 23);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(245, 345);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.HotTrack;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            ForeColor = SystemColors.InfoText;
            FormBorderStyle = FormBorderStyle.None;
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ClosePb).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox1;
        private Label label15;
        private TextBox UserNameTb;
        private Button LoginBtn;
        private TextBox PasswordTb;
        private Label AdminLb;
        private PictureBox ClosePb;
    }
}