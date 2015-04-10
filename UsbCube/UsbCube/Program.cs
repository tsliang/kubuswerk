using System;
using System.IO.Ports;
using System.Text;

namespace Examples{
    internal class ReadWrite{

        public static void Main(string[] args){

            var com5 = new SerialPort("COM5", 57600);
            try{
                com5.DataBits = 8;
                com5.Encoding = Encoding.ASCII;
                com5.NewLine = new string(new []{Convert.ToChar(13), Convert.ToChar(10)});
                com5.Open();
                var b = Convert.ToByte("11000000", 2);
                com5.Write(new[] {b},0,1);
                Console.WriteLine("\r\nDone!\r\n");
            }
            catch (Exception ex){
                Console.WriteLine(ex.Message);
                Console.Read();
            }
            finally {
                if (com5.IsOpen){
                    com5.Close();
                }
            }
        }
    }
}