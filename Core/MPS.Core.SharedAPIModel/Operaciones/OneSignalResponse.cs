using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.SharedAPIModel.Operaciones
{
    public class OneSignalResponse
    {
        public int Item1 { get; set; }
        public Response Item2 { get; set; }
    }
    public class Response
    {
        public string Id { get; set; }
        public int Recipients { get; set; }
        public string External_Id { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
