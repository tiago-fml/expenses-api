﻿namespace expenses_api.DTOs.User;

public class UserUpdateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
}