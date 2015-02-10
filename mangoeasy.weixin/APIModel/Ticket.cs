using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mangoeasy.weixin.APIModel
{
    public class Ticket
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
        public string ticket { get; set; }
        public string expires_in { get; set; }
    }
}