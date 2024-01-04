namespace TODOApi.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate {  get; set; }
        public DateTime? Deadline {  get; set; }
        public bool Checked { get; set; }
    }
}
