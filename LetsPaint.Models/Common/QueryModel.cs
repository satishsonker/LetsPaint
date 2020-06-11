using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsPaint.Models
{
    public class QueryModel
    {
        public string UserName { get; set; }
        public string UserMobile { get; set; }
        public string UserEmail { get; set; }
        public string QueryType { get; set; }
        public string Query { get; set; }
    }
}
