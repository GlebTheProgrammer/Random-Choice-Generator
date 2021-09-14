using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Random_Choice_Generator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonGetAnswer_Click(object sender, EventArgs e)
        {
            Task.Run(() =>                             // Организация многопоточности для работы progressBar и взаимодействия с формой
            {
                for (int i = 1; i < 100; i++)
                {
                    Invoke(new Action(() =>           // Решение проблемы обращения к progressBar из другого потока
                    {
                        progressBar.Value = i;
                    }));
                    Thread.Sleep(200);
                }
            });

            
        }
    }
}
