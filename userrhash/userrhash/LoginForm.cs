using System;
using System.Windows.Forms;

namespace userrhash
{
    public partial class LoginForm : Form
    {
        UserManager manager = new UserManager();

        public LoginForm()
        {
            InitializeComponent();

            manager.Load();

            // Pokud neexistuje žádný uživatel, vytvoříme výchozího admina
            if (manager.Users.Count == 0)
            {
                Admin a = new Admin();
                a.Username = "admin";
                a.PasswordHash = UserManager.Hash("admin");

                manager.Users.Add(a);
                manager.Save();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text))
            {
                MessageBox.Show("Zadejte uživatelské jméno.", "Chyba",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User? u = manager.Login(txtUser.Text, txtPass.Text);

            if (u == null)
            {
                MessageBox.Show("Špatné uživatelské jméno nebo heslo.", "Přihlášení selhalo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPass.Clear();
                txtPass.Focus();
                return;
            }

            MainForm f = new MainForm(u, manager);
            f.FormClosed += (s, args) => this.Show(); // Po zavření MainForm se LoginForm znovu zobrazí
            f.Show();
            this.Hide();
        }

        // Přihlášení také po stisku Enter v poli pro heslo
        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogin_Click(sender, e);
        }
    }
}
