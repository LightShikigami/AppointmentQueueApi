using AppointmentQueueApi.Models;

namespace AppointmentQueueApi.Services
{
    public interface IAppointmentServices
    {
        Appointment CreateAppointment(Customer customer, DateTime requestedDate);
        List<Appointment> GetAppointmentsByDate(DateTime date);
    }
}
