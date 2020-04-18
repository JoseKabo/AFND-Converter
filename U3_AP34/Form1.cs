﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace U3_AP34
{
    public partial class Form1 : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.FromArgb(100, 52, 22, 19);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.FromArgb(100, 231, 76, 60);
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.FromArgb(100, 156, 66, 57);
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.FromArgb(100, 231, 76, 60);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Convirtiendo...");
            recogerEntrada();
            procesarEntrada();
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.FromArgb(100, 156, 66, 57);
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.FromArgb(100, 231, 76, 60);
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        /*ZONA DE PROCESAMIENTO*/

        List<datosQs> entrada;
        String qs = ""; String[] q0 = null; String[] q1 = null;

        List<String> Qs = new List<string>();

        List<datosQs> auxEntrada;
        private void recogerEntrada()
        {
            entrada = new List<datosQs>();
            for (int i = 0; i < dgvEntrada.Rows.Count - 1; i++)
            {
                qs = dgvEntrada.Rows[i].Cells[0].Value.ToString();
                q0 = dgvEntrada.Rows[i].Cells[1].Value.ToString().Split(',');
                q1 = dgvEntrada.Rows[i].Cells[2].Value.ToString().Split(',');
                entrada.Add(new datosQs(qs, q0, q1));
            }
        }

        private void procesarEntrada()
        {
            String resultado = "";
            dgvSalida.Rows.Add("q" + dgvSalida.Rows.Count, dgvEntrada.Rows[0].Cells[0].Value.ToString(), dgvEntrada.Rows[0].Cells[1].Value.ToString(), dgvEntrada.Rows[0].Cells[2].Value.ToString());
            Qs.Add(dgvSalida.Rows[0].Cells[2].Value.ToString()); // aqui agrego el primer destino0 correspondiente al primer registro
            Qs.Add(dgvSalida.Rows[0].Cells[3].Value.ToString());// aqui agrego el segundo destino0 correspondiente al primer registro
            
            foreach (String e in Qs) // se recorren los dos 
            {
                String[] auxE = e.Split(','); // se parte para revisar en la tabla de entrada
                for (int i = 0; i < auxE.Length; i++) 
                {
                    for (int j = 0; j < dgvEntrada.Rows.Count - 1; j++) 
                    {
                        if (auxE[i].Equals(dgvEntrada.Rows[j].Cells[0].Value.ToString())) // si coincide al agregado recientemente con el encontrado en la tabla
                            resultado = resultado +"," + dgvEntrada.Rows[j].Cells[1].Value.ToString();
                    }
                }
            }
            resultado.TrimStart(',');
            resultado.TrimEnd(',');
            dgvSalida.Rows.Add("q" + dgvSalida.Rows.Count, Qs[0], resultado);
        }
    }
}

public class datosQs
{
    public String q  { get; set; }
    public String[] c0 { get; set; }
    public String[] c1 { get; set; }
    public datosQs(String q, String[] c0, String[] c1)
    {
        this.q = q;
        this.c0 = c0;
        this.c1 = c1;
    }
}
