using Microsoft.AspNetCore.Http;

namespace ProjecteV2.ApiMongoDB{
    public class AudioUploadModel
    {
        public string Uid { get; set; }
        public IFormFile Audio { get; set; }
    }
}