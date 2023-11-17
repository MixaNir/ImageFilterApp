using System.Drawing;
using System.Drawing.Imaging;
using WebApplication2.ImageFilters.FilterModels;
using WebApplication2.ImageFilters.Interfaces;
using WebApplication2.Models;

namespace WebApplication2
{
    public static class ImageWorker
    {
        private static List<FileUploadData> _imagesInfo = new List<FileUploadData>();

        public static void AddImageInfo(FileUploadData data) => _imagesInfo.Add(data);


        public static void ApplyFilters(int imageID, IEnumerable<IFilter> filters)
        {
            string imagePath = GetPathFromId(imageID);
            Image currentImage = Image.FromFile(imagePath);
            foreach (var filter in filters)
            {
                currentImage = filter.Apply(currentImage);
            }

            currentImage.Save(imagePath, ImageFormat.Jpeg);
        }

        private static string GetPathFromId(int id) => _imagesInfo.FirstOrDefault(item => item.ImgID == id).ImgName;

        public static ResizeModel PrepareResizeModel(Size size) => new ResizeModel(size);
    }
}
