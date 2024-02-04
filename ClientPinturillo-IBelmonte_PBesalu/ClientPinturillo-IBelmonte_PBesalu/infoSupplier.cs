using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClientPinturillo_IBelmonte_PBesalu
{
    internal class infoSupplier
    {
        private static infoSupplier instance = null;
        private static HttpClient httpclient;
        HttpListener listener = new HttpListener();
        private static string http = "http://";
        private string server = "";
        private string usuari = "";
        
        public async Task<string> listen()
        {
            listener.Prefixes.Add("http://*:8081/");
            listener.Start();
            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                return request.ToString();
            }
        }

        public static infoSupplier Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new infoSupplier();
                    httpclient = new HttpClient();
                }
                return instance;
            }
        }

        public void IniciaConexio(MainWindow parent, string server)
        {
            this.server = server;
            var timer = new System.Timers.Timer(TimeSpan.FromMilliseconds(100).TotalMilliseconds);
            timer.Elapsed += async (sender, e) =>
            {
                try
                {
                    var resposta = await httpclient.GetAsync($"{http}{this.server}");
                }
                catch
                {
                    throw new Exception("No s'ha pogut establir connexió amb el servidor");
                }
            };

            timer.Start();
        }
        public void EnviaMissatge(string missatge)
        {
            var msg = $"{this.usuari}: {missatge}";
            httpclient.GetAsync($"{msg}");
        }
        public string CreaUsuari(string usuari)
        {
            if (usuari == "")
            {
                this.usuari = $"usuari{new Random().Next(1000)}";
            }
            return this.usuari;
        }
        public void EnviaDibuix(string dibuix)
        {
            httpclient.GetAsync($"{this.usuari}: {dibuix}");
        }
    }
}
