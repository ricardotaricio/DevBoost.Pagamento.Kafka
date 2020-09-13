using System;
using System.Collections.Generic;
using System.Text;

namespace DevBoost.DroneDelivery.Infrastructure.Security
{
    public class Token
    {
        public string Secret { get; set; }
        public int ExpiracaoEmMinutos { get; set; }
        public string Emissor { get; set; }
        public string ValidoEm { get; set; }


        public byte[] ObterChave()
        {
           return Encoding.ASCII.GetBytes(Secret);
        }
    }
}
