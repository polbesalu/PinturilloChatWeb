using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinturillo_IanBelmonte_PolBesalú
{
    public class Chat
    {
        public List<string> missatges = new List<string>();
        public void clear()
        {
            missatges.Clear();
        }
        public void afegirMissatge(string missatge)
        {
            missatges.Add(missatge);
        }
        public void afegirMissatge(string missatge, string usuari)
        {
            missatges.Add(usuari + ": " + missatge);
        }
        public string enviarParaula(int num)
        {
            return Program.words[num];
        }
        public string obtenirMissatges()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var missatge in missatges)
            {
                sb.AppendLine(missatge);
            }
            return sb.ToString();
        }
    }
}
