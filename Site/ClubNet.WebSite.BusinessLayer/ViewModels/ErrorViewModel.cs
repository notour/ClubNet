using System;

namespace ClubNet.WebSite.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId
        {
            get { return !string.IsNullOrEmpty(RequestId); }
        }
    }
}