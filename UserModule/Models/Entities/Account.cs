﻿namespace UserAccountManagement.UserModule.Models.Entities;

public class Account
{
    public Guid Id { get; set; }

    public int CustomerId { get; set; }
    
    public double Balance { get; set; }
}