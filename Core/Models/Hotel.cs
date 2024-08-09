namespace Core.Models
{
    public class Hotel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        List<Branch> Branches { get; set; } = new List<Branch>();
    }
}
