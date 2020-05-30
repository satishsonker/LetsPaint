using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LetsPaint.Models;
using Microsoft.AspNetCore.Mvc;

namespace LetsPaint.Controllers
{
    public class QueryController : Controller
    {
        private readonly IEmailSender _emailSender;
        public QueryController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        [HttpPost]
        public JsonResult SendQuery([FromBody] QueryModel queryModel)
        {
            var temp =System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(),"emailTemplate", "query.html"));
            var message = new Message(new string[] { "btech.csit@gmail.com", "sonkarpoonam41@gmail.com" }, "User Query from Let's Paint - "+queryModel.UserName, temp.Replace("##querytype##", queryModel.QueryType).Replace("##email##", queryModel.UserEmail).Replace("##name##", queryModel.UserName).Replace("##userquery##", queryModel.Query).Replace("##mobile##", queryModel.UserMobile));
            _emailSender.SendEmail(message);

            return Json(new { });
        }
    }
}