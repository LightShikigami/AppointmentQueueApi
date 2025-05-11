namespace AppointmentQueueApi.DTOs
{
	public class AppointmentRequest
	{
		public string CustomerName { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public DateTime RequestedDate { get; set; }
	}
}
