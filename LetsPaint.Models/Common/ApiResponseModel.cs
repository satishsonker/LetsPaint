using System;
using System.Collections.Generic;
using System.Text;

namespace LetsPaint.ModelAccess.Common
{
  public  class ApiResponseModel
    {
        public object Data { get; set; }
        public string Message { get; set; }
        public ApiResponseMessageType MessageType { get; set; }
    }

    public enum ApiResponseMessageType
    {
        success,
        error,
        warning,
        info
    }
}
