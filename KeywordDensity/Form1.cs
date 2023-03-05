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
        List<int> Frequencies = new List<int>();
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

            for (int i = 0; i < source.Count(); i++)
                source[i] = source[i].ToLower();

            int wordCount = source.Count();


            for(int i = 0; i < wordCount; i++)
            {
                if (ignoreWord(source[i]))
                {
                    keywords.Add(source[i]);
                    Frequencies.Add(wordCounter(source[i], source));
                    Console.WriteLine("found first word = " + keywords[0] + "freq " + Frequencies[0]);
                    break;
                }
            }

            for (int i = 0; i < wordCount; i++)
            {
                if (!keywords.Contains(source[i]) && ignoreWord(source[i]))
                {
                    keywords.Add(source[i]);
                    Frequencies.Add(wordCounter(source[i], source));
                }
            }

            List<myData> _data = new List<myData>();
            for (int i = 0; i < keywords.Count(); i++)
            {
                float _densi = (Frequencies[i] * 100) / wordCount;
                _data.Add(new myData(keywords[i], Frequencies[i], _densi));
            }
            //var BindedData = new BindingSource(_data, null);
            dataGridView1.DataSource = _data;
        }

        private int wordCounter(string searchWord, string[] source)
        {
            int counter = 0;
            for (int i = 0; i < source.Length; i++)
            {
                if (searchWord == source[i])
                    counter++;
            }
            return counter;
        }

        private bool ignoreWord(string wrd)
        {
            string[] ignored = { "a", "you","in", "of", "it", "for", "and", "the", "at", "on", "all", "if",
                            "else", "or", "to", "that", "there", "out", "he", "she", "it", "us", "then",
                            "your", "this", "from", "is"};
            if (ignored.Contains(wrd))
                return false;
            else
                return true;
        }
    }
}
