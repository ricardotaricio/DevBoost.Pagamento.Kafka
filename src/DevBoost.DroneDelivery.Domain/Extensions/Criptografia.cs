using System.Security.Cryptography;
using System.Text;

namespace DevBoost.DroneDelivery.Domain.Extensions
{
    public static class Criptografia
    {
        public static string ObterHash(this string input)
        {
            string key = "ce0bcb5ae7127d04372fc11f3724a79b7de3ad59eccb0203ee4d7dc196f9e3be";

            input += key;

            var sBuilder = new StringBuilder();

            using (SHA256 sha256Hash = SHA256.Create())
            {

                byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
            }


            return sBuilder.ToString();
        }
    }
}
