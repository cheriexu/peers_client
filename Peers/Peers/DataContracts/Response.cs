using System;
using System.Collections.Generic;
using System.Text;

namespace Peers.DataContracts
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public String Message { get; set; }
        public int Count { get; set; }
    }
}
