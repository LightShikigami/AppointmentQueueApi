namespace AppointmentQueueApi.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime Date { get; set; }
        public String TokenNumber {get; set;}
        public string status { get; set; } = "schduled" ;

    }
}
