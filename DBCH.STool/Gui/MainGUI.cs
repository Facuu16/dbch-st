using DBCH.STool.Manager;
using DBCH.STool.Object;
using System.Drawing;
using System.Windows.Forms;

namespace DBCH.STool
{
    public partial class MainGUI : Form
    {
        private readonly User user;

        public MainGUI(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        public User User { get { return user; } }

        private void button1Click(object sender, MouseEventArgs e)
        {
            new PromptGUI().Input("Establece el tipo de comando: Jugador, Consola", input =>
            {
                switch (input.ToLower())
                {
                    case "jugador":
                        new PromptGUI().Input("Establece el comando con la siguiente sintaxis: JUGADOR;COMANDO", input =>
                        {
                            string[] split = input.Split(';');

                            ManagerProvider.ActionManager.ExecutePlayerCommand(split[0], split[1]);
                            MessageBox.Show("¡Comando añadido a la cola!", "Información", MessageBoxButtons.OK);
                        });
                        break;

                    case "consola":
                        new PromptGUI().Input("Establece el comando", input =>
                        {
                            ManagerProvider.ActionManager.ExecuteConsoleCommand(input);
                            MessageBox.Show("¡Comando añadido a la cola!", "Información", MessageBoxButtons.OK);
                        });
                        break;
                }
            });
        }

        private void button2Click(object sender, System.EventArgs e)
        {
            new PromptGUI().Input("Establece el tipo de mensaje: Jugador, Global", input =>
            {
                switch (input.ToLower())
                {
                    case "jugador":
                        new PromptGUI().Input("Establece el mensaje con la siguiente sintaxis: JUGADOR;MENSAJE", input =>
                        {
                            string[] split = input.Split(';');

                            ManagerProvider.ActionManager.SendPlayerMessage(split[0], split[1]);
                            MessageBox.Show("¡Mensaje añadido a la cola!", "Información", MessageBoxButtons.OK);
                        });
                        break;

                    case "global":
                        new PromptGUI().Input("Establece el mensaje", input =>
                        {
                            ManagerProvider.ActionManager.SendGlobalMessage(input);
                            MessageBox.Show("¡Mensaje añadido a la cola!", "Información", MessageBoxButtons.OK);
                        });
                        break;
                }
            });
        }

        private void pictureBox1MouseEnter(object sender, System.EventArgs e) => 
            pictureBox1.BackColor = Color.Orange;

        private void pictureBox1MouseLeave(object sender, System.EventArgs e) => 
            pictureBox1.BackColor = Color.DarkOrange;

        private void pictureBox1Click(object sender, System.EventArgs e) =>
            MessageBox.Show($"Actualmente hay {ManagerProvider.ServerManager.GetProperty("ONLINE_PLAYERS")} usuarios conectados.", "Información", MessageBoxButtons.OK);

        private void button3Click(object sender, System.EventArgs e) =>
            new PromptGUI().Input("Establece el script", ManagerProvider.ActionManager.ExecuteScript);

        private void MainGUIFormClosed(object sender, FormClosedEventArgs e) => Application.Exit();
    }
}
