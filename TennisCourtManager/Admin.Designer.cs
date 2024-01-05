namespace TennisCourtManager
{
    partial class Admin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Admin));
            panel1 = new Panel();
            ClosePb = new PictureBox();
            AdminLb = new Label();
            LoginBtn = new Button();
            PasswordTb = new TextBox();
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
            panel1.Controls.Add(label15);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(776, 426);
            panel1.TabIndex = 1;
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
            AdminLb.Location = new Point(485, 344);
            AdminLb.Name = "AdminLb";
            AdminLb.Size = new Size(65, 24);
            AdminLb.TabIndex = 8;
            AdminLb.Text = "Anuluj";
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
            PasswordTb.Location = new Point(378, 178);
            PasswordTb.Name = "PasswordTb";
            PasswordTb.PasswordChar = '*';
            PasswordTb.PlaceholderText = "Hasło";
            PasswordTb.Size = new Size(277, 37);
            PasswordTb.TabIndex = 6;
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
            // Admin
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.HotTrack;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Admin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Admin";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ClosePb).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox ClosePb;
        private Label AdminLb;
        private Button LoginBtn;
        private TextBox PasswordTb;
        private Label label15;
        private PictureBox pictureBox1;
    }
}