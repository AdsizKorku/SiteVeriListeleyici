using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;
namespace sitedencek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ono nn;
        private void button1_Click(object sender, EventArgs e)
        {
            nn = new ono();
            listBox1.Items.Clear();
            string kaynakkod = nn.urldenver(textBox1.Text);
            // tabcontrol ve richtxb eklendi. bu sayede taratılan 
            //kaynak koduna bakılabilir.
            //urldenver fonksiyonu kaynak kodunu alır
            //Listele fonksiyonu tırnaklıları listeler
            //listedenlisteye fonksiyonu diğer 
            //classtaki listeyi listboxa yollama için
            toolStripProgressBar1.Value = 0;
            richTextBox1.Text = kaynakkod;
            listedenlisteye(nn.Listele(kaynakkod));
            toolStripProgressBar1.Value = 100;
           
        }
        public void listedenlisteye(List<string> list)
        {
            
            for (int i = 0; i < list.Count; i++)
            {
                listBox1.Items.Add(list[i]);
            }
        }
        //tls hatalarımı gideren kodlar
        private void Form1_Load(object sender, EventArgs e)
        {
           
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            
        }
        // tıklanan resimi gösterir
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.Text.EndsWith(".jpg") || listBox1.Text.EndsWith(".png") || listBox1.Text.EndsWith(".gif") )
            {
                pictureBox1.ImageLocation = listBox1.Text;
            }
        }
        //uzun bir urlden makinenin adresini çıkarır
        public string urlal(string url)
        {
            int sayac = 0,tut=0;
            for (int i = 0; i < url.Length; i++)
            {
                if (url[i]=='/')
                {
                    ++sayac;
                }
                if (sayac==3)
                {
                    tut = i;
                    break;
                }
            }
            return url.Substring(0,tut);
        }
        // button3 aslında listede link olanları eleme tuşu. yani 2. aşama
        private void button3_Click(object sender, EventArgs e)
        {
            toolStripProgressBar1.Value = 0;
            List<string> vs =new List<string>();
            foreach (string item in listBox1.Items)
            {
                
                if (item.TrimEnd().TrimStart().EndsWith(".jpg") || item.TrimEnd().TrimStart().EndsWith(".png") || item.TrimEnd().TrimStart().EndsWith(".gif") || item.TrimEnd().TrimStart().EndsWith(".swf"))
                {
                    vs.Add(item);
                }

            }
            listBox1.Items.Clear();
            string url = textBox1.Text;
            for (int i = 0; i < vs.Count; i++)
            {
                if (vs[i].StartsWith("//"))
                    vs[i]=vs[i].Replace("//", "http://");
                if (vs[i].StartsWith("/")) {
                    //url al
                    vs[i] = urlal(url) + vs[i];
                    
                }
                    
                listBox1.Items.Add(vs[i]);
            }
            toolStripProgressBar1.Value = 100;
        }
        // 3. aşama download
        private void button2_Click(object sender, EventArgs e)
        {
            WebClient myWebClient = new WebClient();
            toolStripProgressBar1.Value = 0;
            saveFileDialog1.Filter = ".gif File |*.gif  | All Files(*.*); |*.* |  .swf File |*.swf  | .png File |*.png";
                saveFileDialog1.ShowDialog();
            myWebClient.DownloadFile(listBox1.Text,saveFileDialog1.FileName);
            toolStripProgressBar1.Value = 100;
        }

        private void Form1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show(listBox1.Text,listBox1);
        }
    }
}
