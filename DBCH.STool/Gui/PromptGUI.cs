using System;
using System.Windows.Forms;

namespace DBCH.STool
{
    public partial class PromptGUI : Form
    {
        private Action<string> _action;

        public PromptGUI() => InitializeComponent();

        public void Input(string message, Action<string> action)
        {
            MessageBox.Show(message, "Información", MessageBoxButtons.OK);
            Show();
            _action = action;
        }

        private void button1Click(object sender, EventArgs e)
        {
            _action.Invoke(textBox1.Text);
            Close();
        }

        private void button2Click(object sender, EventArgs e) => Close();
    }
}
