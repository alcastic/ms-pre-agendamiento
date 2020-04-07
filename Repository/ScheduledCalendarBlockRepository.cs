using System;
using System.Collections.Generic;
using ms_pre_agendamiento.Models;

namespace ms_pre_agendamiento
{
    public class ScheduledCalendarBlockRepository:IScheduledCalendarBlockRepository
    {
    public List<ScheduledCalendarBlock> GetScheduledBlocksMock()
    {
        return new List<ScheduledCalendarBlock>
        {
            new ScheduledCalendarBlock() {Date = DateTime.Now, HourFrom = 13, HourTo = 14}
        };
    }
    }
}