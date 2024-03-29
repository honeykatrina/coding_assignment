﻿using UserAccountManagement.Shared.Models;

namespace UserAccountManagement.Shared.ServiceBusServices;

public interface IProcessDataService<T> where T : CustomMessage
{
    bool Process(T message);
}