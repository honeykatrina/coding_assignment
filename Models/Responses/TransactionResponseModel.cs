﻿namespace UserAccountManagement.Models.Responses;

public class TransactionResponseModel
{
    public double Amount { get; set; }

    public DateTimeOffset CreationDate { get; set; }
}