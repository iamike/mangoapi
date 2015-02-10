using System;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using mangoeasy.weixin.APIModel;
using mangoeasy.weixin.Models;
using mangoeasy.weixin.Services;
using Newtonsoft.Json;

namespace mangoeasy.weixin.Controllers.API
{
    public class ServiceController : BaseApiController
    {
        private readonly CustomerService _customerService;

        public ServiceController()
        {
            var dbContext = new ApplicationDbContext();
            _customerService = new CustomerService(dbContext);
        }

        public object Get()
        {
            var id = new Guid(HttpContext.Current.Request.QueryString["id"]);
            var url = HttpContext.Current.Request.QueryString["url"];
            var customer = _customerService.GetCustomer(id);
            var model = new ResultModel
            {
                Result = new ResponseModel
                {
                    Error = false
                }
            };

            if (customer != null)
            {
                var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                model.AppId = customer.AppId;
                model.NonceStr = CreateNonceStr();
                model.Timestamp = ts.ToString();
                var ticket = GetTicket(customer);
                model.Signature = SHA1_Hash(string.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}", ticket, model.NonceStr, model.Timestamp, url));
            }
            else
            {
                model.Result.Error = true;
            }
            return model;



        }

        //创建随机字符串  
        private static string CreateNonceStr()
        {
            const int length = 16;
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var str = string.Empty;
            var rad = new Random();
            for (var i = 0; i < length; i++)
            {
                str += chars.Substring(rad.Next(0, chars.Length - 1), 1);
            }
            return str;
        }
        //得到ticket 如果文件里时间 超时则重新获取  
        private string GetTicket(Customer customer)
        {
            var ticket = string.Empty;
            var accessToken = string.Empty;

            if (customer.GetAccessTokenDateTime == null || DateTime.Now.Subtract(customer.GetAccessTokenDateTime.Value).Duration().TotalSeconds > 7000)
            {
                var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", customer.AppId, customer.AppSecret);
                accessToken = JsonConvert.DeserializeObject<AccessToken>(HttpGet(url)).access_token;
                customer.GetAccessTokenDateTime = DateTime.Now;
            }
            else
            {
                accessToken = customer.AccessToken;
            }
            var ticketUrl = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", accessToken);
            ticket = JsonConvert.DeserializeObject<Ticket>(HttpGet(ticketUrl)).ticket;
            customer.AccessToken = accessToken;
            _customerService.Update();
            return ticket;
        }
        private static string HttpGet(string url)
        {
            try
            {
                var myWebClient = new WebClient { Credentials = CredentialCache.DefaultCredentials };
                var pageData = myWebClient.DownloadData(url); //从指定网站下载数据  
                var pageHtml = System.Text.Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句
                return pageHtml;
            }

            catch (WebException webEx)
            {
                return webEx.Message;
            }
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