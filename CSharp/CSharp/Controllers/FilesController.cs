using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace server.Controllers
{
    public class FilesController : ApiController
    {
        [Route("upload")]        
        public HttpResponseMessage Post()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var uploadResult = SaveFile(httpRequest);
                return Request.CreateResponse(HttpStatusCode.OK, uploadResult);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        private static UploadResult SaveFile(HttpRequest httpRequest)
        {
            httpRequest.Files.Get(0);
            var guid = System.Guid.NewGuid().ToString();
            var postedFile = httpRequest.Files.Get(0);
            var DirectoryPath = HttpContext.Current.Server.MapPath("~/uploads");
            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }
            var filePath = DirectoryPath + '\\' + guid;
            postedFile.SaveAs(filePath);
            return new UploadResult() { file_name = guid };
        }
    }

    public class UploadResult
    {
        public string file_name { get; set; }
    }
}