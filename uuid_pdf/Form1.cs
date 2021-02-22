using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

namespace uuid_pdf
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Title = "Seleccione un archivo PDF";
            openFileDialog1.Filter = "Archivos PDF (*.pdf)|*.pdf";
            openFileDialog1.Multiselect = true;
            string uuids = String.Empty;
            string temp = Path.GetTempPath();
            int o = 0;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter txt = new StreamWriter(textBox2.Text + "/uuid_pdf.txt", true);
                for(int f=0; f <openFileDialog1.SafeFileNames.Length; f++)
                {
                    o = f;
                    try
                    {
                        
                        string str_ruta = openFileDialog1.FileNames[f];
                        
                        var pdfDocument = new PdfDocument(new PdfReader(str_ruta));
                        var strategy = new LocationTextExtractionStrategy();
                        StreamWriter file = new StreamWriter(temp + "/all_pdf"+ o +".txt", true);
                        string text = String.Empty;
                        for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
                        {
                            var page = pdfDocument.GetPage(i);
                            text = PdfTextExtractor.GetTextFromPage(page);
                            file.Write(text);


                        }

                        file.Close();
                        file.Dispose();

                        string resultado = File.ReadAllLines(temp + "/all_pdf" + o + ".txt").Where(X => X.Contains("Folio Fiscal:")).First();
                        uuids+= resultado + "\r\n"  ;



                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                txt.Write(uuids);
                txt.Close();
                txt.Dispose();
                var message = "Su archivo txt se ha generado en la ruta:"+ textBox2.Text;
                MessageBox.Show(message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
