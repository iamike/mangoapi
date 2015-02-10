using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mangoeasy.weixin.Controllers.API;

namespace mangoeasy.weixin.Models
{
    public class ResultModel
    {
        public ResponseModel Result { get; set; }
        public string AppId { get; set; }
        public string Timestamp { get; set; }
        public string NonceStr { get; set; }
        public string Signature { get; set; }
    }
}