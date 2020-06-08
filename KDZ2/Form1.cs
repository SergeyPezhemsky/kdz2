using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KDZ2
{
    public partial class Form1 : Form
    {
        string[] operators = {"inline", "register", "static", "typedef", "virtual", "mutable",
                                    "const", "friend", "volatile", "asm", "break", "case", "class",
                                    "continue", "default", "delete", "do", "else", "enum", "for", "goto",
                                    "if", "new", "operator", "private", "protected", "public", "return",
                                    "sizeof", "struct", "switch", "this", "union", "while", "namespace",
                                    "using", "try", "catch", "throw", "const_cast", "static_cast",
                                    "dynamic_cast", "reinterpret_cast", "typeid", "template", "explicit",
                                    "true", "false", "typename", "!", "!=", "%", "%=", "&&", "||", "&=",
                                    "(", ")", "*", "*=", "+", "++", "=", "+=", "-", "-=", "/", "for", "switch",
                                    "if", "while", "while for"};
        public Form1()
        {
            Array.Sort(operators);
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            uint w = uint.Parse(specialErrors.Text);
            uint v = uint.Parse(findErrors.Text);
            uint s = uint.Parse(totalFind.Text) - v < 0 ? 0 : uint.Parse(totalFind.Text) - v;
            float n = (float)(w * s) / v;
            uint r = uint.Parse(hopeErrors.Text);
            float C = s > r ? 1 : (float)w / (w + r + 1);
            MillsResult.Text = n.ToString() + ", " + C.ToString();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void Holsted_Click(object sender, EventArgs e)
        {
            int[] opers = { 0, 0 };
            List<string>uniqOperators = new List<string>();
            List<string> uniqOperands = new List<string>();
            using (StreamReader sr = new StreamReader("../../Form1.cs"))
            {
                // Read the stream to a string, and write the string to the console.
                string line = sr.ReadToEnd();
                string[] words = line.Split(' ');
                int[] timeOpers = analyz(words, ref uniqOperators, ref uniqOperands);
                opers[0] += timeOpers[0];
                opers[1] += timeOpers[1];
            }
            int uniqOperandsCount = uniqOperators.Count; 
            int uniqOperatorsCount = uniqOperands.Count;
            int operatorsCount = opers[1]; 
            int operandsCount = opers[0];
            AllOperators.Text = operatorsCount.ToString();
            allOperands.Text = operandsCount.ToString();
            UniqOperands.Text = uniqOperandsCount.ToString();
            UniqOperators.Text = uniqOperatorsCount.ToString();

            int alphabet = uniqOperatorsCount + uniqOperandsCount;
            AlphabetTextBox.Text = alphabet.ToString();

            int expLenght = operandsCount + operatorsCount;
            expLengthTextBox.Text = expLenght.ToString();

            double teorLenght = uniqOperandsCount * (Math.Log(uniqOperandsCount) / Math.Log(10)) + uniqOperatorsCount * (Math.Log(uniqOperatorsCount) / Math.Log(10));
            theorLengthTextBox.Text = teorLenght.ToString();

            double vol = expLenght * (Math.Log(alphabet) / Math.Log(2));
            volTextBox.Text = vol.ToString();

            double potentialVol = (operandsCount * operatorsCount) * (Math.Log(uniqOperandsCount + uniqOperatorsCount) / Math.Log(2));
            potentialVolTextBox.Text = potentialVol.ToString();

            double level = potentialVol / vol;
            levelTextBox.Text = level.ToString();

            double diff = 1 / level;
            difTextBox.Text = diff.ToString();

            double waitingLevel = (2 / (double)uniqOperatorsCount) * (uniqOperandsCount / (double)operandsCount);
            waitingLevelTextBox.Text = waitingLevel.ToString();

            double intelegens = waitingLevel * vol;
            intelegensTextBox.Text = intelegens.ToString();

            double work = diff * vol;
            workTextBox.Text = work.ToString();

            double time = work / 10;
            timeTextBox.Text = time.ToString();

            double waitTime = uniqOperatorsCount * operandsCount * alphabet * (Math.Log(alphabet) / Math.Log(2)) / (2 * 10 * uniqOperandsCount);
            waitingTimeTextBox.Text = waitTime.ToString();

            double langLevel = potentialVol * potentialVol / vol;
            langLevelTextBox.Text = langLevel.ToString();

            double errorsCount = vol / 3000;
            ErrorsTextBox.Text = errorsCount.ToString();
        }

        public int[] analyz(string[] words,ref List<string> uniqOperators, ref List<string> uniqOperands)
        {
            int[] opers = new int[2];
            foreach (string word in words)
            {
                int k = Array.BinarySearch(operators, word);
                if (k < 0)
                {
                    k = Array.BinarySearch(operators, word + ";");
                }
                if (k < 0)
                {
                    k = word.IndexOf('(');
                }
                if (k < 0)
                {
                    k = word.IndexOf(')');
                }
                if (k < 0)
                {
                    k = word.IndexOf('.');
                }
                if (k < 0)
                {
                    k = word.IndexOf('[');
                }
                if (k < 0)
                {
                    k = word.IndexOf(']');
                }
                if (k < 0) {
                    opers[0]++;
                    if (!uniqOperators.Contains(word))
                    {
                        uniqOperators.Add(word);
                    }
                }
                else {
                    opers[1]++;
                    if (!uniqOperands.Contains(word))
                    {
                        uniqOperands.Add(word);
                    }
                }
            }
            return opers;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(timeFor3.Text);
            listBox2.Items.Add(errorsCount.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            float c = float.Parse(textBox1.Text);
            int n = listBox2.Items.Count;
            for (int i = 0; i < n; i++)
            {
                float er = float.Parse(listBox2.Items[i].ToString());
                float t = float.Parse(listBox1.Items[i].ToString());
                double p = Math.Exp(-c * (Math.Abs(er - n)) * t * t / 2);
                chart1.Series.ElementAt(0).Points.AddXY(t, p);
            }
        }
    }
}
