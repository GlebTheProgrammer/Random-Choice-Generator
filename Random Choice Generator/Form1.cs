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
using System.IO;

namespace Random_Choice_Generator
{

    public partial class Form1 : Form
    {
        private const string formName = "AnswerGenerator";
        private readonly string configPath = $"{Environment.CurrentDirectory}\\answersConfig.json";  // Определение пути к Config File

        public Form1()
        {
            InitializeComponent();
        }

        private void ProgressBarUpload(int i)
        {
            if (i == progressBar.Maximum)
            {
                progressBar.Maximum = i + 1;
                progressBar.Value = i + 1;
                progressBar.Maximum = i;
            }
            else
            {
                progressBar.Value = i + 1;
            }
            progressBar.Value = i;
        }

        private async void buttonGetAnswer_Click(object sender, EventArgs e)
        {
           buttonGetAnswer.Enabled = false;          // Отключение возможности запускать несколько потоков при многократном нажатии на кнопку
           await Task.Run(() =>                             // Организация многопоточности для работы progressBar и взаимодействия с формой (+ асинхронность)
            {
                for (int i = 1; i <= 100; i++)
                {
                    Invoke(new Action(() =>           // Решение проблемы обращения к progressBar из другого потока
                    {
                        ProgressBarUpload(i);
                        Text = $"{i}%";
                    }));
                    Thread.Sleep(20);
                }
            });

            MessageBox.Show("Answer");

            progressBar.Value = 0;
            Text = formName;
            buttonGetAnswer.Enabled = true;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = formName;

            try
            {
                var data = File.ReadAllText(configPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
    }
}
