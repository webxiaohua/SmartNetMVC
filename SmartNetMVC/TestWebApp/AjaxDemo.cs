using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;

namespace TestWebApp
{
    public class AjaxDemo
    {
       
        public string GetMd5(string input)
        {
            if (input == null)
                input = string.Empty;

            byte[] bb = (new MD5CryptoServiceProvider()).ComputeHash(Encoding.Default.GetBytes(input));
            return BitConverter.ToString(bb).Replace("-", "").ToLower();
        }
    }
}