using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Org.BouncyCastle.Security;

namespace Proyecto
{
    public partial class frmDashboard : Form
    {
        private string operation;
        public frmDashboard()
        {
            InitializeComponent();
            lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }

        private void btndescargar_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("{0}.pdf", DateTime.Now.ToString("ddMMyyyyHHmmss"));

            //string PaginaHTML_Texto = "<table border=\"1\"><tr><td>HOLA MUNDO</td></tr></table>";
            string PaginaHTML_Texto = Properties.Resources.Plantilla.ToString();
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@VARIABLE0", txtComercial.Text);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@VARIABLE1", txtSoldBy.Text);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@VARIABLE2", txtCash.Text);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@VARIABLE3", txtSoldBy.Text);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@VARIABLE4", txtCDD.Text);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@VARIABLE5", DateTime.Now.ToString("dd/MM/yyyy"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@VARIABLE6", txtAmount.Text);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@VARIABLE7", txtCustomer.Text);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@VARIABLE8", txtName.Text);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@VARIABLE9", txtAddress.Text);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@VARIABLE6", txtCity.Text);

            string filas = string.Empty;
            decimal total = 0;
            foreach (DataGridViewRow row in dgvproductos.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Quantity"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Description"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Unit Price"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Amount"].Value.ToString() + "</td>";
                filas += "</tr>";
                total += decimal.Parse(row.Cells["Amount"].Value.ToString());
            }
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS", filas);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@TOTAL", total.ToString());
           


            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                {
                    //Creamos un nuevo documento y lo definimos como PDF
                    Document pdfDoc = new Document(PageSize.A4,25,25,25,25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();
                    pdfDoc.Add(new Phrase(""));

                    //Agregamos la imagen del banner al documento
                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.shop, System.Drawing.Imaging.ImageFormat.Png);
                    img.ScaleToFit(700, 600);
                    img.Alignment = iTextSharp.text.Image.UNDERLYING;

                    img.SetAbsolutePosition(-40, pdfDoc.Top - 817);
                    pdfDoc.Add(img);


                    //pdfDoc.Add(new Phrase("Hola Mundo"));
                    using (StringReader sr = new StringReader(PaginaHTML_Texto))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();
                    stream.Close();
                }

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {/*
            dgvproductos.Columns.Add("Quantity", "Quantity");
            dgvproductos.Columns.Add("Description", "Description");
            dgvproductos.Columns.Add("Unit Price", "Unit Price");
            dgvproductos.Columns.Add("Amount", "Amount");*/
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            int indice_fila = dgvproductos.Rows.Add();
            DataGridViewRow row = dgvproductos.Rows[indice_fila];

            row.Cells["Quantity"].Value = txtQuant.Text;
            row.Cells["Description"].Value = txtDes.Text;
            row.Cells["UnitPrice"].Value = txtPrice.Text;
            row.Cells["Amount"].Value = decimal.Parse(txtQuant.Text) * decimal.Parse(txtPrice.Text);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddClie_Click(object sender, EventArgs e)
        {
            using (frmClient frm = new frmClient())
            {
                frm.ShowDialog();
            }
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
             using (frmClient frm = new frmClient())
            {
                frm.ShowDialog();
            }
        }
    }
}
