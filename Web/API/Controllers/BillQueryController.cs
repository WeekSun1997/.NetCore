using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class BillQueryController : Controller
    {
        public BillQueryController(IDBServices dB)
        {
            services = dB;
        }

        public IDBServices services;

        /// <summary>
        /// 
        /// </summary>
        [HttpPost("Query")]
        public IActionResult Query()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="Channel"></param>
        /// <param name="Stuts"></param>
        /// <param name="Group"></param>
        /// <returns></returns>
        [HttpPost("Vip")]
        public IActionResult Vip(string BeginDate, string EndDate, string Channel, string Stuts, string Group, string JYZT)
        {
            if (string.IsNullOrEmpty(Channel))
            {
                Channel = "-1";
            }
            if (string.IsNullOrEmpty(Stuts))
            {
                Stuts = "-1";
            }
            if (string.IsNullOrEmpty(Group))
            {
                Group = "CreateTime,Channel";
            }
            if (string.IsNullOrEmpty(JYZT))
            {
                JYZT = "-1";
            }
            string msg = "";
            bool IsSuccess = true;
            string sql = $@"SELECT COUNT(1)Count,SUM(case when TransactionType=33 THEN Amount else -Amount end)Amount,{Group} FROM(
                             SELECT  sd.Value Channel, sc.Value Stuts,CreateTime,Amount,TransactionType,dt.value jyzt  FROM viptransaction 
                             JOIN sysdropdwondt sd on Channel=sd.Id
                             join sysdropdwondt sc on Stuts=sc.id
                             JOIN billvipinfo bv on bv.Phone=viptransaction.Phone
                             join sysdropdwondt dt on dt.id=bv.Statc
                             WHERE CreateTime BETWEEN '{BeginDate}' and '{EndDate}' and 
                            (viptransaction.Channel in ({Channel}) or  -1 in ({Channel}))  
                            and( bv.statc in ({JYZT}) OR -1 IN ({JYZT}))and
                            (viptransaction.Stuts in ({Stuts}) or -1 IN({Stuts}))
                            )r GROUP BY {Group}";
            var t = services.QueryTable(sql);
            if (!string.IsNullOrEmpty(t.Exception?.Message))
            {
                IsSuccess = false;
                msg = t.Exception?.Message;
            }
    
            return Json(new { IsSuccess = IsSuccess, msg = msg, data = t.Result });
        }
    }
}