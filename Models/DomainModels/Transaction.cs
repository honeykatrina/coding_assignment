﻿namespace UserAccountManagement.Models.DomainModels;

public class Transaction
{
    public Guid Id { get; set; }

    public Guid AccountId { get; set; }

    public double Amount { get; set; }

    public DateTimeOffset CreationDate { get; set; }
}