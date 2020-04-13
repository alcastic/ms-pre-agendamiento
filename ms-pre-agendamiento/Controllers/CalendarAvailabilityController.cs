using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ms_pre_agendamiento.Models;

namespace ms_pre_agendamiento.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarAvailabilityController
    {
        private readonly ICalendarAvailabilityService _calendarAvailabilityService;

        public CalendarAvailabilityController(ICalendarAvailabilityService calendarAvailabilityService)
        {
            _calendarAvailabilityService =
                calendarAvailabilityService ?? throw new ArgumentNullException("CalendarAvailabilityService");
        }

        public IEnumerable<TimeSlot> GetAvailableSlotsFromService()
        {
            return _calendarAvailabilityService.GetAvailableBlocks();
        }
        
        [HttpGet]
        public IActionResult GetAvailableSlots()
        {
            IEnumerable<TimeSlot> availableBlocks = _calendarAvailabilityService.GetAvailableBlocks();
            if (!availableBlocks.Any())
            {
                return new NoContentResult();
            }
            return new ObjectResult(availableBlocks);
        }

        [HttpPost]
        public Calendar GetCalendarWithAvailableTimeSlots([FromBody] CalendarRequest calendarRequest)
        {
            return new Calendar(
                calendarRequest.From,
                calendarRequest.To,
                GetAvailableSlotsFromService().ToList()
            );
        }
    }
}