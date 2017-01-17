﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThoughtWorks.QRCode.Codec;
using WxPayAPI;
using System.Threading;
namespace Wechat
{
    public partial class NativePayUI : Form
    {
        private int page = 1;
        private string a = null, b = null;
        int c = 0;
        bool flag1 = true;
        WxPayData queryOrderInput = new WxPayData();
        WxPayData result;

        private string out_trade_no1 = null;//用来接收商品号

        public NativePayUI()
        {
            InitializeComponent();
        }
        private void ThreadMethod()
        {
            while (flag1)
            {
                queryOrderInput.SetValue("out_trade_no", out_trade_no1);
                result = WxPayApi.OrderQuery(queryOrderInput);

                if (result.GetValue("return_code").ToString() == "SUCCESS" && result.GetValue("result_code").ToString() == "SUCCESS")
                {
                    //支付成功
                    //------------暂时未写付款后的操作-------------------
                    if (result.GetValue("trade_state").ToString() == "SUCCESS")
                    {
                          flag1 = false;
                        MessageBox.Show("支付成功");
                    }
                }
            }
        }
  
        private void NativePayUI_Load(object sender, EventArgs e)
        {
            NativePay nativePay = new NativePay();
            //生成扫码支付模式二url
            //page * 15 为了测试改成1
            string url2 = nativePay.GetPayUrl(1, "123456789", "商品名称", "商品标记", "商品描述");


            out_trade_no1 = nativePay.getout_trade_no();
            //初始化二维码生成工具
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeScale = 4;

            //将字符串生成二维码图片
            pictureBox1.Image = qrCodeEncoder.Encode(url2, Encoding.Default);
            label2.Text = "页数:" + page + "\n" + "总价：" + page * 0.15 + "元";
            //回调结果

            Thread t = new Thread(new ThreadStart(ThreadMethod));
            t.Start();
            Thread.Sleep(2000);
        }
        public void setpage(int sum)
        {
            this.page = sum;
        }
        public int getpage()
        {
            return this.page;
        }
        public void payset1(string a)
        {
            this.a = a;
        }
        public string payget1()
        {
            return this.a;
        }
        public void payset2(string b)
        {
            this.b = b;
        }
        public string payget2()
        {
            return this.b;
        }

        public void payset3(int c)
        {
            this.c = c;
        }

        public int payget3()
        {
            return this.c;
        }
    }
}