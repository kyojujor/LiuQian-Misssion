using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DataModel;

namespace DataAcess
{
    public class DataSource
    {
        public static int ID { get; set; }

        public static List<string> DocNameList
        {
            get
            {
                var dir = new List<string> { "电子银行", "对公存款", "国际金融", "理财与代理业务", "零售银行", "内控合规", "信用卡", "业务运营", "支付结算", "资产业务" };
                return dir;
            }
        }

        /// <summary>
        /// 选择题都是同一个方法，无论单选还是多选
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<SubjectModel> GetExcelDataSingle(string path)
        {
            var res = new List<SubjectModel>();

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.GetEncoding("GBK"));

            string str = "";
            str = sr.ReadLine();
            var i = 0;
            while (str != null)
            {
                var test1 = str.Split(new[] { "\t" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (test1 != null && test1.Count == 7)
                {
                    var model = new SubjectModel();
                    model.Id = i;
                    //model.Type = test1[0];
                    model.Title = test1[1];
                    model.RightSelect = Convert.ToInt32(test1[2]);
                    model.Select1 = test1[3];
                    model.Select2 = test1[4];
                    model.Select3 = test1[5];
                    model.Select4 = test1[6];
                    res.Add(model);
                    i++;
                }
                str = sr.ReadLine();
            }
            sr.Close();
            fs.Close();
            return res;
        }

        /// <summary>
        /// 判断题
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<SubjectModel> GetExcelDataJudge(string path)
        {
            var res = new List<SubjectModel>();

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.GetEncoding("GBK"));

            string str = "";
            str = sr.ReadLine();
            var i = 0;
            while (str != null)
            {
                var test1 = str.Split(new[] { "\t" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (test1 != null && test1.Count == 3)
                {
                    var model = new SubjectModel();
                    model.Id = i;
                    //model.Type = test1[0];
                    model.Title = test1[1];
                    model.RightSelect = Convert.ToInt32(test1[2]);
                    //model.Select1 = test1[3];
                    //model.Select2 = test1[4];
                    //model.Select3 = test1[5];
                    //model.Select4 = test1[6];
                    res.Add(model);
                    i++;
                }
                str = sr.ReadLine();
            }
            sr.Close();
            fs.Close();
            return res;
        }

        /// <summary>
        /// 获得所有题目,无论题型
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<SubjectModel> GetAllTxttopic(string dirPath,string range)
        {
            var res = new List<SubjectModel>();
            var path = string.Format("{0}/{1}.txt", dirPath, range);
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.GetEncoding("GBK"));

            string str = "";

            while (!sr.EndOfStream)
            {
                str = sr.ReadLine().Trim();
                if (!string.IsNullOrWhiteSpace(str))
                {
                    if (str.StartsWith(@"试题类型"))
                    {
                        continue;
                    }
                    var topicStr = str.Split(new[] { "\t" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (topicStr.Count > 0)
                    {
                        var resCell = new SubjectModel();
                        //判断题
                        if (topicStr[0].Contains("判断") && topicStr.Count == 5)
                        {
                            resCell.Type = TopicType.judge;
                            resCell.BasicSub = topicStr[1];
                            resCell.Title = topicStr[2];
                            resCell.diffcult = topicStr[3];
                            resCell.RightSelect = int.Parse(topicStr[4]);
                        }
                        else if (topicStr.Count == 9)
                        {
                            if (topicStr[0].Contains("多选"))
                                resCell.Type = TopicType.doubleSelect;
                            else
                                resCell.Type = TopicType.single;

                            resCell.BasicSub = topicStr[1];
                            resCell.Title = topicStr[2];
                            resCell.diffcult = topicStr[3];
                            resCell.RightSelect = int.Parse(topicStr[4]);

                            resCell.Select1 = topicStr[5];
                            resCell.Select2 = topicStr[6];
                            resCell.Select3 = topicStr[7];
                            resCell.Select4 = topicStr[8];
                        }
                        resCell.Id = ID;
                        resCell.Range = range;
                        res.Add(resCell);
                        ID++;
                    }
                }
            }
            if (res.Count == 0)
            {
                return null;
            }
            sr.Close();
            fs.Close();
            return res;
        }
    }
}
