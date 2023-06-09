﻿namespace DBCH.STool.Object
{
    public class User
    {
        private readonly string _username;

        private readonly string _password;

        public User(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public string Username { get { return _username; } }

        public string Password { get { return _password; } }
    }
}