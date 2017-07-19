using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Peers.Models
{
    public class LinkedInProfile
    {
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String emailAddress { get; set; }
        public String pictureUrl { get; set; }
    }
}
