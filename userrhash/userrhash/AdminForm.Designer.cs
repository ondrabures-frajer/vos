namespace userrhash
{
    partial class AdminForm
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
            lblUsers = new Label();
            listUsers = new ListBox();
            btnReset = new Button();
            btnAdd = new Button();
            btnDelete = new Button();
            SuspendLayout();

            // lblUsers
            lblUsers.AutoSize = true;
            lblUsers.Font = new Font("Segoe UI", 10F);
            lblUsers.Location = new Point(12, 10);
            lblUsers.Text = "Uživatelé:";

            // listUsers
            listUsers.Font = new Font("Segoe UI", 11F);
            listUsers.FormattingEnabled = true;
            listUsers.ItemHeight = 20;
            listUsers.Location = new Point(12, 32);
            listUsers.Size = new Size(260, 160);
            listUsers.TabIndex = 0;

            // btnReset
            btnReset.Font = new Font("Segoe UI", 11F);
            btnReset.Location = new Point(12, 205);
            btnReset.Size = new Size(260, 36);
            btnReset.TabIndex = 1;
            btnReset.Text = "Resetovat heslo";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += new EventHandler(btnReset_Click);

            // btnAdd
            btnAdd.Font = new Font("Segoe UI", 11F);
            btnAdd.Location = new Point(12, 250);
            btnAdd.Size = new Size(125, 36);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Přidat";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += new EventHandler(btnAdd_Click);

            // btnDelete
            btnDelete.Font = new Font("Segoe UI", 11F);
            btnDelete.Location = new Point(147, 250);
            btnDelete.Size = new Size(125, 36);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Smazat";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += new EventHandler(btnDelete_Click);

            // AdminForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 302);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            Controls.Add(btnReset);
            Controls.Add(listUsers);
            Controls.Add(lblUsers);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "AdminForm";
            Text = "Správa uživatelů";
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblUsers;
        private ListBox listUsers;
        private Button btnReset;
        private Button btnAdd;
        private Button btnDelete;
    }
}
