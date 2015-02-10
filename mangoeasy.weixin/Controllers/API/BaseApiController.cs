using System.Web.Http;

namespace mangoeasy.weixin.Controllers.API
{
    
    public class BaseApiController : ApiController
    {
        protected ResponseModel Success()
        {
            return new ResponseModel
            {
                ErrorCode = 0,
                Message = "success",
                Error = false
            };
        }
    }
    public class ResponseModel
    {
        public int Id { get; set; }
        public int ErrorCode { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
        public string DebugMessage { get; set; }
    }
}