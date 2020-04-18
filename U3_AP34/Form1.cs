using System;
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
        List<String> Qs = new List<string>();

        private String buscar0s(String f)
        {
            String resultado = "";
            String[] auxE = f.Split(','); // se parte para revisar en la tabla de entrada
            for (int i = 0; i < auxE.Length; i++)
                {
                for (int j = 0; j < dgvEntrada.Rows.Count - 1; j++)
                {
                    if (auxE[i].Equals(dgvEntrada.Rows[j].Cells[0].Value.ToString())) // si coincide al agregado recientemente con el encontrado en la tabla
                        resultado = resultado + "," + dgvEntrada.Rows[j].Cells[1].Value.ToString();
                }
            }
            return resultado.TrimStart(',');
        }

        private String buscar1s(String f)
        {
            String resultado = "";
            String[] auxE = f.Split(','); // se parte para revisar en la tabla de entrada
            for (int i = 0; i < auxE.Length; i++)
            {
                for (int j = 0; j < dgvEntrada.Rows.Count - 1; j++)
                {
                    if (auxE[i].Equals(dgvEntrada.Rows[j].Cells[0].Value.ToString())) // si coincide al agregado recientemente con el encontrado en la tabla
                        resultado = resultado + "," + dgvEntrada.Rows[j].Cells[2].Value.ToString();
                }
            }
            return resultado.TrimStart(',');
        }

        private void recogerSalida()
        {
            for (int i = 0; i < dgvSalida.Rows.Count; i++)
            {
                Qs.Add(dgvSalida.Rows[i].Cells[2].Value.ToString());
                Qs.Add(dgvSalida.Rows[i].Cells[3].Value.ToString());
            }
        }



        private void procesarEntrada()
        {

            int contador = 5;
            dgvSalida.Rows.Add("q" + dgvSalida.Rows.Count, dgvEntrada.Rows[0].Cells[0].Value.ToString(), dgvEntrada.Rows[0].Cells[1].Value.ToString(), dgvEntrada.Rows[0].Cells[2].Value.ToString());
            //Qs.Add(dgvSalida.Rows[0].Cells[2].Value.ToString()); // aqui agrego el primer destino0 correspondiente al primer registro
            //Qs.Add(dgvSalida.Rows[0].Cells[3].Value.ToString());// aqui agrego el segundo destino0 correspondiente al primer registro
            Qs = new List<string>();
            recogerSalida();

            foreach (String e in Qs)
            {
                dgvSalida.Rows.Add("q" + dgvSalida.Rows.Count, e, buscar0s(e), buscar1s(e)); 
            }

            /*foreach (String e in Qs) // se recorren los dos 
            {
                String[] auxE = e.Split(','); // se parte para revisar en la tabla de entrada
                for (int i = 0; i < auxE.Length; i++) /
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
            dgvSalida.Rows.Add("q" + dgvSalida.Rows.Count, Qs[0], resultado, resultado2);*/
        }
    }
}

