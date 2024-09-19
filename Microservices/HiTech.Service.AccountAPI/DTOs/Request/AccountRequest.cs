﻿namespace HiTech.Service.AccountAPI.DTOs.Request
{
    public class AccountRequest
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string? FirstName { get; set; } = string.Empty;

        public string? LastName { get; set; } = string.Empty;

        public string? Phone { get; set; } = string.Empty;

        public string? Address { get; set; } = string.Empty;
    }
}
