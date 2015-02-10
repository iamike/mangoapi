using mangoeasy.weixin.Services;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace mangoeasy.weixin.Controllers.API
{
    public class WebSettingController : BaseApiController
    {
        private readonly CustomerService _customerService;

        public WebSettingController()
        {
            var dbContext = new ApplicationDbContext();
            _customerService = new CustomerService(dbContext);
        }

        public HttpResponseMessage Get()
        {
            var id = new Guid(HttpContext.Current.Request.QueryString["id"]);
            var token = HttpContext.Current.Request.QueryString["token"];
            var customer = _customerService.GetCustomer(id, token);

            var signature = HttpContext.Current.Request.QueryString["signature"];
            var timestamp = HttpContext.Current.Request.QueryString["timestamp"];
            var nonce = HttpContext.Current.Request.QueryString["nonce"];
            var echostr = HttpContext.Current.Request.QueryString["echostr"];

           
            if (customer != null)
            {
                return new HttpResponseMessage { Content = new StringContent(echostr, Encoding.GetEncoding("UTF-8"), "application/json") };
            }
            
            return null;
        }


        private static string SHA1_Hash(string strSha1In)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var bytesSha1In = System.Text.Encoding.Default.GetBytes(strSha1In);
            var bytesSha1Out = sha1.ComputeHash(bytesSha1In);
            var strSha1Out = BitConverter.ToString(bytesSha1Out);
            strSha1Out = strSha1Out.Replace("-", "").ToLower();
            return strSha1Out;
        }

    }
}