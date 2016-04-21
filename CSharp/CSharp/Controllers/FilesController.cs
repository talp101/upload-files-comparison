using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/uploads/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    // NOTE: To store in memory use postedFile.InputStream
                }
                
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}