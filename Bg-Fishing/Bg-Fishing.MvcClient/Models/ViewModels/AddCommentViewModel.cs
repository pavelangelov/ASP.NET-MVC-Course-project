using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bg_Fishing.MvcClient.Models.ViewModels
{
    public class AddCommentViewModel
    {
        public string LakeName { get; set; }

        public string Username { get; set; }

        public string Content { get; set; }
    }
}