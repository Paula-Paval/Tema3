using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Cinerva.Web.Blob
{
    public interface IBlobService
    {
        public  Task<string> Upload(IFormFile image);
        public  Task Delete(string url);
    }
}
