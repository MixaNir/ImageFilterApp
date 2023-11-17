namespace WebApplication2.Models
{
    public class FileUploadData
    {
        public int ImgID { get; set; }
        public int UserID { get; set; }
        public IFormFile file { get; set; }
        public string ImgName { get; set; }
    }
}
