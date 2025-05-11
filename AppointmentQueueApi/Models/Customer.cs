using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentQueueApi.Models
{
    public class Customer
    {
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
