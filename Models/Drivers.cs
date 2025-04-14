using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace allocation.Models
{
    public class Drivers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Driverid { get; set; }

        [Required]
        public string Drivername { get; set; } = null;

        [Required]
        public string DriverVehicalNumber { get; set; } = null;
        
        [Required]
        public bool IsAllocated { get; set; }

    }
}
