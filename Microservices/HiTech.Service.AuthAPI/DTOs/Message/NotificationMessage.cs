﻿namespace HiTech.Service.AuthAPI.DTOs.Message
{
    public class NotificationMessage
    {
        public string? Type { get; set; }
        public string? Content { get; set; }
        public int? UserId { get; set; }
    }
}