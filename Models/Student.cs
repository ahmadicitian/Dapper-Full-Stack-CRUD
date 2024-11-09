namespace DapperFullStack.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Marks { get; set; }
        public string Image { get; set; }
        public IFormFile file { get; set; }
    }
}
