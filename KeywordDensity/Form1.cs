using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeywordDensity
{
    public partial class Form1 : Form
    {
        string _text;
        List<string> keywords = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _text = richTextBox1.Text;
            densityCalcul(_text);
        }

        private void densityCalcul(string txt)
        {
            char[] delimiters = new char[] { ' ', '\r', '\n', '.', '?', '!', ';', ':', ',' };
            string[] source = txt.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            int wordCount = source.Count();

            int[] FrDe = new int[wordCount];

            keywords.Add(source[0]);
            FrDe[0] = wordCounter(source[0], source);

            for (int i = 0; i < wordCount; i++)
            {
                if (!keywords.Contains(source[i]) && ignoreWord(source[i]))
                {
                    keywords.Add(source[i]);
                    FrDe[i] = wordCounter(source[i], source);
                }
            }

            List<myData> _data = new List<myData>();
            for (int i = 0; i < keywords.Count(); i++)
            {
                float _densi = (FrDe[i] * 100) / wordCount;
                _data.Add(new myData(keywords[i], FrDe[i], _densi));
            }
            //var BindedData = new BindingSource(_data, null);
            dataGridView1.DataSource = _data;
        }

        private int wordCounter(string searchWord, string[] source)
        {
            var matchQuery = from word in source
                             where word.Equals(searchWord, StringComparison.InvariantCultureIgnoreCase)
                             select word;
            return matchQuery.Count();
        }

        private bool ignoreWord(string wrd)
        {
            string ignored = "in, to, for, at, is , are";
            if (ignored.Contains(wrd))
                return false;
            else
                return true;
        }
    }
}
