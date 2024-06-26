﻿using System.Threading.Tasks;
using Abp.Application.Services;

namespace WS.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task DisableRecurringPayments();

        Task EnableRecurringPayments();
    }
}
