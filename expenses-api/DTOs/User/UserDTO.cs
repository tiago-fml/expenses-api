﻿using expenses_api.Enums;

namespace expenses_api.DTOs.User;

public class UserDTO
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? BirthDate { get; set; }
}