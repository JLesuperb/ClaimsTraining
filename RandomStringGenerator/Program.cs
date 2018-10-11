using System;

namespace RandomStringGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            Console.WriteLine(GuidString);
            Console.ReadLine();
        }
    }
}
