using System.Drawing;

namespace WebApplication2.ImageFilters.Interfaces
{
    public interface IFilter
    {
        public Image Apply(Image img);
    }
}
