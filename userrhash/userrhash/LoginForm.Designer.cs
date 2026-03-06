namespace userrhash
{
    partial class LoginForm
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
            lblUsername = new Label();
            txtUser = new TextBox();
            lblPassword = new Label();
            txtPass = new TextBox();
            btnLogin = new Button();
            SuspendLayout();

            // lblUsername
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 10F);
            lblUsername.Location = new Point(12, 15);
            lblUsername.Text = "Uživatelské jméno:";

            // txtUser
            txtUser.Font = new Font("Segoe UI", 12F);
            txtUser.Location = new Point(12, 38);
            txtUser.Size = new Size(210, 29);
            txtUser.TabIndex = 0;

            // lblPassword
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10F);
            lblPassword.Location = new Point(12, 80);
            lblPassword.Text = "Heslo:";

            // txtPass
            txtPass.Font = new Font("Segoe UI", 12F);
            txtPass.Location = new Point(12, 103);
            txtPass.Size = new Size(210, 29);
            txtPass.TabIndex = 1;
            txtPass.UseSystemPasswordChar = true;
            txtPass.KeyDown += new KeyEventHandler(txtPass_KeyDown);

            // btnLogin
            btnLogin.Font = new Font("Segoe UI", 12F);
            btnLogin.Location = new Point(60, 155);
            btnLogin.Size = new Size(114, 36);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Přihlásit";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += new EventHandler(btnLogin_Click);

            // LoginForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(234, 210);
            Controls.Add(btnLogin);
            Controls.Add(txtPass);
            Controls.Add(lblPassword);
            Controls.Add(txtUser);
            Controls.Add(lblUsername);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "LoginForm";
            Text = "Přihlášení";
            StartPosition = FormStartPosition.CenterScreen;
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblUsername;
        private TextBox txtUser;
        private Label lblPassword;
        private TextBox txtPass;
        private Button btnLogin;
    }
}
