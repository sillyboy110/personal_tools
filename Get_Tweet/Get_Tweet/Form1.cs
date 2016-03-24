using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using HtmlAgilityPack;
using System.Threading;

namespace Get_Tweet
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            InitBrowser();
        }

        public ChromiumWebBrowser browser;
        public void InitBrowser()
        {
            Cef.Initialize(new CefSettings());
            browser = new ChromiumWebBrowser("https://twitter.com/");
            this.panel1.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            var htmlSource = browser.GetSourceAsync();
            string xx = (string)htmlSource.Result;
        }

        private void button_crawel_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(Begin_Hand);
            t.Start();

        }

        void Begin_Hand()
        {

            int refresh_time = 0;
            bool have_more_data = true;

            int max_count = 5000;
            int count = 0;

            List<string> data = new List<string>();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            while (count < max_count)
            {
                List<string> tw_text = new List<string>();
                var htmlSource = browser.GetSourceAsync();
                string page_source = (string)htmlSource.Result;
                if (page_source == null)
                {
                    Thread.Sleep(5);
                }
                doc.LoadHtml(page_source);

                HtmlNodeCollection li_collection = doc.DocumentNode.SelectNodes("//li");
                List<HtmlNode> valid_li = new List<HtmlNode>();
                int li_count = li_collection.Count();
                for (int j = 0; j < li_count; ++j)
                {
                    if (li_collection[j].Attributes.Contains("class") && li_collection[j].Attributes["class"].Value.Contains("js-stream-item stream-item stream-item expanding-stream-item"))
                        valid_li.Add(li_collection[j]);
                }
                //<div class="js-tweet-text-container"
                //<div class="QuoteTweet
                //<div class="QuoteTweet-text tweet-text u-dir" 
                if (valid_li.Count == 0)
                {
                    Thread.Sleep(3);
                    continue;
                }
                for (int j = 0; j < valid_li.Count; ++j)//对每个li定位三个内容
                {
                    HtmlNodeCollection temp_text = valid_li[j].SelectNodes(".//div");
                    int pos_text = -1, pos_ori_text = -1, pos_time = -1;
                    for (int k = 0; k < temp_text.Count; ++k)
                    {
                        if (temp_text[k].Attributes.Contains("class") && temp_text[k].Attributes["class"].Value.Equals("js-tweet-text-container"))
                            pos_text = k;
                        else if (temp_text[k].Attributes.Contains("class") && temp_text[k].Attributes["class"].Value.Equals("QuoteTweet-container"))
                            pos_ori_text = k;

                    }
                    HtmlNode node_text = temp_text[pos_text].SelectSingleNode(".//p");
                    tw_text.Add("\n\n" + node_text.InnerText.Replace("...", " "));

                    if (pos_ori_text > -1)//have quote
                    {
                        //QuoteTweet-text tweet-text u-dir
                        HtmlNodeCollection div_collection = temp_text[pos_ori_text].SelectNodes(".//div");
                        HtmlNode quote = null;
                        for (int k = 0; k < div_collection.Count; ++k)
                        {
                            if (div_collection[k].Attributes.Contains("class") && div_collection[k].Attributes["class"].Value.Contains("QuoteTweet-text tweet-text u-dir"))
                            {
                                quote = div_collection[k];
                                break;
                            }
                        }
                        tw_text.Add(quote.InnerHtml);
                    }

                }
                int old_count = data.Count;

                //save reslt
                System.IO.StreamWriter file = new System.IO.StreamWriter("result.txt", true);
                string str_data = null;

                for (int k = 0; k < tw_text.Count; ++k)
                {
                    if (!data.Contains(tw_text[k]))
                    {
                        data.Add(tw_text[k]);
                        count++;
                        str_data += tw_text[k] + "\n";
                    }
                }
                if (str_data != null)
                    file.Write(str_data);
                file.Close();
                if (old_count == data.Count)
                {
                    refresh_time++;
                    have_more_data = false;
                }
                else
                {
                    refresh_time = 0;
                    have_more_data = true;
                }
                if ((have_more_data == false) && (refresh_time == 4))
                    break;
                tw_text.Clear();

                if (data.Count > 4000)
                {
                    if (MessageBox.Show("数目操作4000,是否结束", "Tips", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        break;
                    }
                }
                else if (data.Count > 3000)
                {
                    if (MessageBox.Show("数目操作3000,是否结束", "Tips", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        break;
                    }
                }
                else if (data.Count > 2000)
                {
                    if (MessageBox.Show("数目操作2000,是否结束", "Tips", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        break;
                    }
                }
                else if (data.Count > 1000)
                {
                    if (MessageBox.Show("数目操作1000,是否结束", "Tips", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        break;
                    }
                }
                //refresh
                string script = "window.scrollTo(0,document.body.scrollHeight);";
                browser.ExecuteScriptAsync(script);
                Thread.Sleep(5);
            }
            MessageBox.Show("finished!");
        }
    }
}
