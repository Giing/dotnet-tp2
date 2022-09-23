using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2Console.Models
{
    public partial class Avi
    {
        public override string ToString()
        {
            return "[" + this.Film.ToString() + "] [" + this.Utilisateur + "] : " + this.Avis; 
        }
    }
}
