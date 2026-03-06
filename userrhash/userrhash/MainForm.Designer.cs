namespace userrhash
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btnChange = new Button();
            btnAdmin = new Button();
            btnLogout = new Button();
            SuspendLayout();

            // btnChange
            btnChange.Font = new Font("Segoe UI", 12F);
            btnChange.Location = new Point(20, 20);
            btnChange.Size = new Size(200, 40);
            btnChange.TabIndex = 0;
            btnChange.Text = "Změnit heslo";
            btnChange.UseVisualStyleBackColor = true;
            btnChange.Click += new EventHandler(btnChange_Click);

            // btnAdmin
            btnAdmin.Font = new Font("Segoe UI", 12F);
            btnAdmin.Location = new Point(20, 72);
            btnAdmin.Size = new Size(200, 40);
            btnAdmin.TabIndex = 1;
            btnAdmin.Text = "Správa uživatelů";
            btnAdmin.UseVisualStyleBackColor = true;
            btnAdmin.Click += new EventHandler(btnAdmin_Click);

            // btnLogout
            btnLogout.Font = new Font("Segoe UI", 12F);
            btnLogout.Location = new Point(20, 124);
            btnLogout.Size = new Size(200, 40);
            btnLogout.TabIndex = 2;
            btnLogout.Text = "Odhlásit se";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += new EventHandler(btnLogout_Click);

            // MainForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(240, 185);
            Controls.Add(btnLogout);
            Controls.Add(btnAdmin);
            Controls.Add(btnChange);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Hlavní menu";
            StartPosition = FormStartPosition.CenterScreen;
            ResumeLayout(false);
        }

        private Button btnChange;
        private Button btnAdmin;
        private Button btnLogout;
    }
}
