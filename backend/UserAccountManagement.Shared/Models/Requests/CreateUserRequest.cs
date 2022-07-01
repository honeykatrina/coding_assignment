﻿namespace UserAccountManagement.Shared.Models;

public class CreateUserRequest
{
    public int CustomerId { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public double InitialCredit { get; set; }
}