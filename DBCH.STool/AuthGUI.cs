using DBCH.STool.Manager;
using DBCH.STool.Object;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DBCH.STool.GUI
{
    public partial class AuthGUI : Form
    {
        public AuthGUI()
        {
            InitializeComponent();
        }

        private void button1Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Completa todos los campos requeridos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ManagerProvider.UserManager.IsUser(textBox1.Text))
            {
                User user = ManagerProvider.UserManager.GetUser(textBox1.Text);

                if (user.Password == textBox2.Text)
                {
                    MessageBox.Show("Iniciaste sesión correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    new MainGUI(user).Show();
                    return;
                }
            }

            MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void textBox1Click(object sender, EventArgs e)
        {
            panel2.BackColor = Color.White;
            textBox1.BackColor = Color.White;
            panel3.BackColor = SystemColors.Control;
            textBox2.BackColor = SystemColors.Control;
        }

        private void textBox2Click(object sender, EventArgs e)
        {
            panel3.BackColor = Color.White;
            textBox2.BackColor = Color.White;
            panel2.BackColor = SystemColors.Control;
            textBox1.BackColor = SystemColors.Control;
        }

        private void pictureBox2MouseLeave(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }

        private void pictureBox2MouseEnter(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = false;
        }
    }
}
