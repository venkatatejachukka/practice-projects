﻿namespace MVC_TimeTracker.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public byte permission { get; set; }
    }
}
