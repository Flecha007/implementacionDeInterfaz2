using aplicacion1.Models.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace aplicacion1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //lbRespuesta.Text = "";
            string url = "http://192.168.0.8/API/post.php";
            VentasRequest objProducto = new VentasRequest();
            objProducto.nombre_cliente = txt1.Text;
            objProducto.doc_cliente = Int32.Parse( txt2.Text);
            objProducto.nombre_paquete = txt3.Text;
            objProducto.cantidad_dias = Int32.Parse(text4.Text);
            objProducto.total = Int32.Parse(text5.Text);
            objProducto.nombre_usuario = text6.Text;
            objProducto.fecha = text7.Text;

            string resultado = Send<VentasRequest>(url, objProducto, "POST");
            //lbRespuesta.Text = resultado;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        private async void button2_Click(object sender, EventArgs e)
        {
            string respuesta = await GetDatos();
            List<VentasRequest> lst = JsonConvert.DeserializeObject<List<VentasRequest>>(respuesta);
            dataGridView1.DataSource = lst;
        }

        public async Task<string> GetDatos()
        {
            WebRequest objRequest = WebRequest.Create("http://192.168.0.8/API/post.php");
            WebResponse objResponse = objRequest.GetResponse();
            StreamReader cadena = new StreamReader(objResponse.GetResponseStream());
            return await cadena.ReadToEndAsync();

        }


        public string Send<T>(string url, T objectRequest, string method = "POST")
        {
            string result = "";

            try
            {

                JavaScriptSerializer js = new JavaScriptSerializer();

                //serializamos el objeto
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(objectRequest);

                //peticion
                WebRequest request = WebRequest.Create(url);
                //headers
                request.Method = method;
                request.PreAuthenticate = true;
                request.ContentType = "application/json;charset=utf-8'";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

            }
            catch (Exception e)
            {
                result = e.Message;

            }

            return result;
        }


    }
}
