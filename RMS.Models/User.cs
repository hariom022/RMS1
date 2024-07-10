namespace RMS.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int PlantID { get; set; } // Reference to the associated plant
        public Store? Plant { get; set; } // Navigation property
    }

}
