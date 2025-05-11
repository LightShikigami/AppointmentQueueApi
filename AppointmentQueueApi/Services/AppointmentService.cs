using AppointmentQueueApi.Data;
using AppointmentQueueApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentQueueApi.Services
{
    public class AppointmentService : IAppointmentServices
    {
        private readonly AppDbContext _context;
        private const int MaxPerDay = 3;

        private readonly List<DateTime> _customHolidays = new List<DateTime>
        {
            new DateTime(2025, 5, 17),
            new DateTime(2025, 6, 1)
        };

        public AppointmentService(AppDbContext context) 
        {
            _context = context;
        }

        private bool IsHoliday(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday
                || date.DayOfWeek == DayOfWeek.Sunday
                || _customHolidays.Contains(date.Date);
        }

        public Appointment CreateAppointment(Customer customer, DateTime requestedDate)
        {
            int attempt = 0;
            int maxAttempt = 30;

            while (_context.Appointments.Count(a => a.Date.Date == requestedDate.Date) >= MaxPerDay || IsHoliday(requestedDate))
            {
                requestedDate = requestedDate.AddDays(1);
                attempt++;

                if (attempt >= maxAttempt)
                {
                    throw new Exception("no available slot within 30 days.");
                }
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();

            int count = _context.Appointments.Count(a => a.Date.Date == requestedDate.Date) + 1;
            string token = requestedDate.ToString("yyyyMMdd") + "-" + count.ToString("D2");

            var appointment = new Appointment
            {
                CustomerId = customer.Id,
                Date = requestedDate.Date,
                TokenNumber = token
            };

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return appointment;
        }

        public List<Appointment> GetAppointmentsByDate(DateTime date) 
        { 
            return _context.Appointments.Include(a => a.Customer).Where(a => a.Date.Date == date.Date).ToList();
        }
    }
}
