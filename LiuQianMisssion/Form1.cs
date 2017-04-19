using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAcess;
using DataModel;

namespace LiuQianMisssion
{
    public partial class Form1 : Form
    {
        public static List<SubjectModel> res { get; set; }
        public static SubjectModel selModel { get; set; }
        public static int selectId { get; set; }
      

        private static readonly string testFileDriUrl = @"../../TxtFileSource";   //文件夹地址

        public Form1()
        {
            InitializeComponent();
            res = new List<SubjectModel>();
            var dir = new List<string> { "电子银行", "对公存款", "国际金融", "理财与代理业务", "零售银行", "内控合规", "信用卡", "业务运营", "支付结算", "资产业务" };
            foreach (var item in dir)
            {
                res.AddRange(DataSource.GetAllTxttopic(testFileDriUrl,item));
            }

            selectId = 0;
            selModel = RandomSelect();
            InJectForm();
        }

        public SubjectModel RandomSelect()
        {
            if (res != null && res.Count > 0)
                selectId = new Random().Next(0, res.Count);

            var ret = res[selectId];
            res = res.FindAll(x => x.Id != ret.Id);

            return ret;
        }

        /// <summary>
        /// model注入到页面上
        /// </summary>
        public void InJectForm()
        {
            var model = RandomSelect();
            richTextBox1.Text = selModel.Title;
            Sel_Single_1.Text = selModel.Select1;
            Sel_Single_2.Text = selModel.Select2;
            Sel_Single_3.Text = selModel.Select3;
            Sel_Single_4.Text = selModel.Select4;
        }

        private void Sel_Single_2_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Sel1_Click(object sender, EventArgs e)
        {
            var isSelect = false;
            var selectedValue = 0;
            # region 单选
            if (selModel.Type ==TopicType.single)
            {
                SetSingleEnable(1, true);
                SetSingleEnable(2, false);
                foreach (var item in GroupSingleBox1.Controls)
                {
                    if (item is RadioButton)
                    {
                        var temp = (RadioButton)item;
                        if (temp.Checked)
                        {
                            isSelect = true;
                            selectedValue = Convert.ToInt32(temp.Name.Split('_')[2]);
                        }
                    }
                }
            }
            #endregion

        }

        private void SetSingleEnable(int num, bool bl)
        {
            if (num == 1)
            {
                Sel_Single_1.Enabled = Sel_Single_2.Enabled = Sel_Single_3.Enabled = Sel_Single_4.Enabled = bl;
                Sel_Single_1.Visible = Sel_Single_2.Visible = Sel_Single_3.Visible = Sel_Single_4.Visible = bl;
            }
            if (num == 2)
            {
                Sel_dou_1.Enabled = Sel_dou_2.Enabled = Sel_dou_3.Enabled = Sel_dou_4.Enabled = bl;
                Sel_dou_1.Visible = Sel_dou_2.Visible = Sel_dou_3.Visible = Sel_dou_4.Visible = bl;
            }
            if (num == 3)
            {
                Sel_dou_1.Enabled = Sel_dou_2.Enabled = Sel_dou_3.Enabled = Sel_dou_4.Enabled = bl;
                Sel_dou_1.Visible = Sel_dou_2.Visible = Sel_dou_3.Visible = Sel_dou_4.Visible = bl;
            }
        }
    }
}
