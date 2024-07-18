namespace Edtech.DTOs
{
    public class StudentImage
    {
        public int Id { get; set; }
        public string CourseTitle { get; set; }
        public string InstructorName { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public int Price { get; set; }
        public IFormFile ProfileImage { get; set; }
        public IFormFile ProfileVideo1 { get; set; }
        public IFormFile ProfileVideo2 { get; set; }
        public IFormFile ProfileVideo3 { get; set; }
        public IFormFile ProfileVideo4 { get; set; }
        public IFormFile ProfileVideo5 { get; set; }
    }
}
