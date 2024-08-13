namespace RepositoryPattern.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Createby { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public string? Updateby { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public bool? DelFlag { get; set; }
    }
}
