using System;
using System.Collections.Generic;

namespace DENMAP_SERVER.Controller.request
{
    internal class CommentRequest
    {
        public int userId { get; set; }
        public int postId { get; set; }
        public double rating { get; set; }
        public string message { get; set; }
    }
}
