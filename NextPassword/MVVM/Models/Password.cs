﻿namespace NextPassword.MVVM.Models
{
    public class Password : PasswordBase
    {
        public string? Notes { get; set;}
        public string? Username { get; set; }
        public string? Url { get; set; }
        public Token? Token { get; set; }
        public Password(string? id, string title, string password, string? notes, string? username, string? url, Token? token) : base(id, title, password)
        {
            Notes = notes;
            Username = username;
            Url = url;
            Token = token;
        }
    }
}
