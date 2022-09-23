using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2Console.Models
{
    public partial class Utilisateur
    {
        public override string ToString()
        {
            return this.Id + " : " + this.Login;
        }
    }
}
