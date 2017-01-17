﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wechat
{
   
    public partial class PrintUI : Form
    { private int page1=0;
        public PrintUI()
        {
            InitializeComponent();
        }
       
        public void set(int sum)
        {
            this.page1 = sum;
        }
        public int get()
        {
            return this.page1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || comboBox2.Text == "" || comboBox3.Text == "")
            {
                MessageBox.Show("您还有信息未填写完，请仔细填写！");
            }
            else
            {
                this.set1(this.comboBox1.Text);
                this.set2(this.comboBox2.Text);
                this.set3(Convert.ToInt32(this.comboBox3.Text));
                NativePayUI pay1 = new NativePayUI();
                pay1.payset1(this.get1());
                pay1.payset2(this.get2());
                pay1.payset3(this.get3());
                pay1.setpage(this.get() * this.get3());
                pay1.Show();
                this.Hide();
            }
        }
        string a = null, b = null;
        int c = 0;
        public void set1(string a)
        {
            this.a = a;
        }
        public string get1()
        {
            return this.a;
        }
        public void set2(string b)
        {
            this.b = b;
        }
        public string get2()
        {
            return this.b;
        }
        public void set3(int c)
        {
            this.c = c;
        }
        public int get3()
        {
            return this.c;
        }
    }
}