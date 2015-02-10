using System;

namespace mangoeasy.weixin.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string WebSite { get; set; }
        public string AppId { get; set; }
        public string AppSecret { get; set; }
        /// <summary>
        /// 微信返回的Token
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 客户Token
        /// </summary>
        public string Token { get; set; }
        public DateTime? GetAccessTokenDateTime { get; set; }
    }
}