using Microsoft.AspNetCore.Mvc;
using AppointmentQueueApi.Models;
using AppointmentQueueApi.Services;
using AppointmentQueueApi.DTOs;

namespace AppointmentQueueApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentServices _services;

        public AppointmentController(IAppointmentServices services)
        {
            _services = services;
        }

        [HttpPost]
        public IActionResult CreateAppointment([FromBody] AppointmentRequest request) 
        {
            var customer = new Customer
            {
				Name = request.CustomerName,
				Phone =request.Phone,
				Email = request.Email
			};

            var appointment = _services.CreateAppointment(customer, request.RequestedDate);
            return Ok(appointment);
        }

        [HttpGet("today")]
        public IActionResult GetTodayAppointments() 
        {
            var today = DateTime.Now.Date;
            var result = _services.GetAppointmentsByDate(today);
            return Ok(result);
        }

        [HttpGet("tomorrow")]
        public IActionResult GetTommorrwAppointments()
        {
            var today = DateTime.Now.Date;
            var result = _services.GetAppointmentsByDate(DateTime.Today.AddDays(1));
            return Ok(result);
        }
    }
}
