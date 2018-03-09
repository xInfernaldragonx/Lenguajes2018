﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.IO;


namespace Pract1Lenguajes2018
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void nuevaPestañaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var lastIndex = this.tabControl1.TabCount - 1;
            TabPage tp = new TabPage();
            int tc = (tabControl1.TabCount );
            tp.Text = "Nueva Pestaña" + tc.ToString();
            tabControl1.TabPages.Insert(tc, tp);
            RichTextBox rt = new RichTextBox();
            rt.Size = new System.Drawing.Size(665, 192);
            rt.Location = new System.Drawing.Point(0, 0);
            tp.Controls.Add(rt);
            this.tabControl1.SelectedIndex = lastIndex;
            
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Creado por Manuel Rivera, 201212747");
        }

        private void análisisLéxicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scanner s = new scanner();
            s.Analizador(getRichTextBox().Text);

            
        }

        private void archivosDeSalidaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void buscarCoincidenciasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private RichTextBox getRichTextBox()
        {
            RichTextBox rt = null;
            TabPage tp = tabControl1.SelectedTab;
            if (tp != null)
            {
                rt = tp.Controls[0] as RichTextBox;

            }

            return rt;
        }

        private void cargarPestañaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;
            OpenFileDialog of = new OpenFileDialog();
            of.DefaultExt = "*.txt";
            of.Filter = "TXT Files|*.txt";
            if (of.ShowDialog() == System.Windows.Forms.DialogResult.OK && of.FileName.Length > 0)
            {
                if ((myStream = of.OpenFile())!= null)
                {
                    string flname = of.FileName;
                    string filetext = File.ReadAllText(flname);
                    getRichTextBox().Text = filetext;
                }
                    
               
            }
        }

        private void guardarArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
