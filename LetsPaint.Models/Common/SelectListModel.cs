using System;
using System.Collections.Generic;
using System.Text;

namespace LetsPaint.ModelAccess.Common
{
  public  class SelectListModel
    {
        public int Value { get; set; }
        public string Text { get; set; }
        public bool Selected { get; set; }
    }
    public class RefLookupModel
    {
        public int RefId { get; set; }
        public string RefKey { get; set; }
        public string RefValue { get; set; }
    }
}
