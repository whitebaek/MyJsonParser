using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMi9Code.Models
{
    public class ResponseData
    {
        public List<Response> response { get; set; }
    }

    public class Response
    {
        public string image { get; set; }
        public string slug { get; set; }
        public string title { get; set; }
    }
}