using System;
using System.Collections.Generic;
using System.Text;

namespace LetsPaint.ModelAccess.Common
{
   public class PagingOutModel: PagingInModel
    {
        public int TotalRecord { get; set; }
        public object Data { get; set; }
    }
    public class PagingInModel
    {
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int Id { get; set; }
    }
}
