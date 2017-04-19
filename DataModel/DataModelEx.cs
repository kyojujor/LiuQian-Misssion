using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class SubjectModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Select1 { get; set; }
        public string Select2 { get; set; }
        public string Select3 { get; set; }
        public string Select4 { get; set; }
        public int RightSelect { get; set; }
        public string Range { get; set; }
        public TopicType Type { get; set; }
        public string BasicSub { get; set; }
        public string diffcult { get; set; }
    }

    public enum TopicType
    {
        judge = 1,
        single = 2,
        doubleSelect = 3
    }
    /// <summary>
    /// "电子银行", "对公存款", "国际金融", "理财与代理业务", "零售银行", "内控合规", "信用卡", "业务运营", "支付结算", "资产业务"
    /// </summary>
    public enum topicRange
    {
       
    }

}
