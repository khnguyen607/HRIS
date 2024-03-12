using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WS.MultiTenancy.HostDashboard.Dto;

namespace WS.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}