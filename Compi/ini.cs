﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Compi
{
    public partial class ini : Form
    {
        MainWinForm mainWin = null;
        int total = 0;



        public ini()
        {
            InitializeComponent();
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void ini_DoubleClick(object sender, EventArgs e)
        {
            //editorGramatica id = new editorGramatica(this);
            //id.Show();
            //this.Hide();
            MainWinForm mainWinForm = new MainWinForm(this);
            mainWinForm.Show();
            this.Hide();
        }

        private void label2_DoubleClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ini_Load(object sender, EventArgs e)
        {
            //progressBar1.Maximum = 100;
            //minimum indica el valor mínimo de la barra
            //progressBar1.Minimum = 0;
            //value indica desde donde se va a comenzar a llenar la barra, la nuestra iniciara desde cero
            //progressBar1.Value = 0;

            //Por ejemplo podemos hacer que la barra inicie desde la mitad
            //la siguiente instrucción indica que inicie cargando desde la mitad del tamaño de la barra
            //progressBar1.Value = progressBar1.Maximum / 2;

            //step indica el paso de la barra, entre más pequeño sea la barra tardará más en cargar
            //progressBar1.Step = 5;
            //// maximum indica el valor máximo de la barra
            //progressBar1.Maximum = 100;

            ////minimum indica el valor mínimo de la barra
            //progressBar1.Minimum = 0;

            ////value indica desde donde se va a comenzar a llenar la barra, la nuestra iniciara desde cero
            //progressBar1.Value = 0;

            ////Por ejemplo podemos hacer que la barra inicie desde la mitad
            ////la siguiente instrucción indica que inicie cargando desde la mitad del tamaño de la barra
            ////progressBar1.Value = progressBar1.Maximum / 2;

            ////step indica el paso de la barra, entre más pequeño sea la barra tardará más en cargar
            //progressBar1.Step = 5;

            //el ciclo for cargará la barra
            //for (int i = progressBar1.Minimum; i < progressBar1.Maximum; i = i + progressBar1.Step)
            //{
            //    //esta instrucción avanza la posición actual de la barra
            //    progressBar1.PerformStep();
            //    label1.Text = "cargando complementos..";
            //}
            //label1.Text = "sistema cargado.";

            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 10;

            if (!this.backgroundWorker1.IsBusy)
                this.backgroundWorker1.RunWorkerAsync();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int i = progressBar1.Minimum;

            total = total + 1;
            if (total == 2 && i < progressBar1.Maximum)
            {
                progressBar1.PerformStep();
                progressBar1.PerformStep();
                progressBar1.PerformStep();
                progressBar1.PerformStep();
                label1.Text = "cargando complementos.";
            }

            if (total == 3 && i < progressBar1.Maximum)
            {
                progressBar1.PerformStep();
                progressBar1.PerformStep();
                progressBar1.PerformStep();
                progressBar1.PerformStep();
                label1.Text = "cargando complementos..";
            }

            if (total == 4 && i < progressBar1.Maximum)
            {
                progressBar1.PerformStep();
                progressBar1.PerformStep();
                progressBar1.PerformStep();
                progressBar1.PerformStep();
                label1.Text = "cargando complementos...";
            }

            if (total == 5 && i < progressBar1.Maximum)
            {
                label1.Text = "sistema cargado";

                for (int isf = i; isf < progressBar1.Maximum; isf = isf + progressBar1.Step)
                {
                    //esta instrucción avanza la posición actual de la barra
                    progressBar1.PerformStep();
                    label1.Text = "sistema cargado";
                }
            }

            if (total == 6)
            {
                MainWinForm mainWinForm = new MainWinForm(this);
                mainWinForm.Show();
                this.Hide();
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ini_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.backgroundWorker1.WorkerSupportsCancellation)
                this.backgroundWorker1.CancelAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (mainWin == null)
            {
                mainWin = new MainWinForm(this);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mainWin.Show();
            this.Hide();
        }
    }
}