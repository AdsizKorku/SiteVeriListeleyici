using System.Collections.Generic;
using System.IO;
using System.Net;

namespace sitedencek
{
    public class ono : Form1
    {
        public ono()
        {
        }
        public string urldenver(string url)
        {

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            using (Stream stream = request.GetResponse().GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string response = reader.ReadToEnd();
                    return response;
                }
            }

        }
        
        public List<string> Listele(string sc)
        {
            List<string> vs = new List<string>();
            int tırnak1 = 0;
            //1 tırnak aç , 2 tırnak kapat
            string temp = "";
            for (int i = 0; i < sc.Length; i++)
            {
                if (sc[i]=='"')
                {
                    if(tırnak1==0)
                        tırnak1 = 1;
                    else if(tırnak1==1)
                    {
                       tırnak1 = 0;
                        vs.Add(temp);
                        temp = "";

                    }
                  
                }
                else
                {
                    if (tırnak1==1)
                    {
                        temp += sc[i];
                    }
                }
                
            }
            return vs;
        }
          
    }
}
