using DBCH.STool.Object;
using MySqlConnector;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;

namespace DBCH.STool.Manager
{
    static class ManagerProvider
    {
        private static readonly UserManager _userManager = new();

        private static readonly ActionManager _actionManager = new();

        private static readonly ServerManager _serverManager = new();

        public static UserManager UserManager => _userManager;

        public static ActionManager ActionManager => _actionManager;    

        public static ServerManager ServerManager => _serverManager;
    }

    public abstract class Manager
    {
        private readonly MySqlConnection _connection;

        protected Manager()
        {
            try
            {
                _connection = new(ConfigurationManager.AppSettings["connection"]);
                _connection.Open();
            }
            catch (MySqlException ex)
            {
                _connection.Close();
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        protected MySqlConnection Connection => _connection;
    }

    public class UserManager : Manager
    {
        private readonly Dictionary<string, User> _users;

        internal UserManager()
        {
            _users = new();
            UpdateUsers();
        }

        public void UpdateUsers()
        {
            using MySqlCommand command = new("SELECT * FROM Users", base.Connection);
            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string username = reader.GetString("username");
                _users[username] = new User(username, reader.GetString("password"));
            }
        }

        public bool IsUser(string username) => _users.ContainsKey(username);

        public User GetUser(string username) => _users[username];
    }

    public class ActionManager : Manager
    {
        internal ActionManager() { }

        public void ExecuteScript(string script) =>
            insertAction(new()
            {
                { "type", "SCRIPT" },
                { "value", script }
            });

        public void ExecuteConsoleCommand(string command) =>
            insertAction(new()
            {
                { "type", "CONSOLE_COMMAND" },
                { "value", command }
            });

        public void ExecutePlayerCommand(string player, string command) =>
            insertAction(new()
            {
                { "type", "PLAYER_COMMAND" },
                { "player", player },
                { "value", command }
            });

        public void SendPlayerMessage(string player, string message) =>
            insertAction(new()
            {
                { "type", "PLAYER_MESSAGE" },
                { "player", player },
                { "value", message }
            });

        public void SendGlobalMessage(string message) =>
            insertAction(new()
            {
                { "type", "GLOBAL_MESSAGE" },
                { "value", message }
            });

        private void insertAction(JObject jsonObject)
        {
            using MySqlCommand sqlCommand = new("INSERT INTO Actions (json) VALUES (@json)", Connection);

            sqlCommand.Parameters.Add(new("@json", jsonObject.ToString()));
            sqlCommand.ExecuteNonQuery();
        }
    }

    public class ServerManager : Manager
    {
        internal ServerManager() { }

        public object GetProperty(string property)
        {
            using MySqlCommand command = new("SELECT json FROM Server WHERE property = @property", Connection);

            command.Parameters.AddWithValue("@property", property);
            object result = command.ExecuteScalar();

            return result != null ? JObject.Parse((string) result)["value"] : null;
        }
    }
}