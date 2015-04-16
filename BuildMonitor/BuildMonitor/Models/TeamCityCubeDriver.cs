using System;
using System.IO.Ports;
using System.Text;
using System.Threading;
using BuildMonitor.Models;

namespace BuildMonitor {
    public class TeamCityCubeDriver {

        public TeamCityCubeDriver(string comPort){
            ComPort = comPort;
        }

        public string ComPort { get; set; }
        private CubeConfiguration lastConfigurationSent;

        public void SendCommand(CubeConfiguration configuration, bool clearFirst) {
            if (lastConfigurationSent == configuration){
                return;
            }
            var serialPort = new SerialPort(ComPort, 57600);
            try {
                serialPort.DataBits = 8;
                serialPort.Encoding = Encoding.ASCII;
                serialPort.NewLine = new string(new[] { Convert.ToChar(13), Convert.ToChar(10) });
                serialPort.Open();

                if (clearFirst) {
                    WriteToArduino(serialPort, CubeConfiguration.AllOff);
                }
                WriteToArduino(serialPort, configuration);
                lastConfigurationSent = configuration;
            }
            finally {
                if (serialPort.IsOpen) {
                    serialPort.Close();
                }
            }
        }

        private void WriteToArduino(SerialPort serialPort, CubeConfiguration configuration) {
            serialPort.Write(new[] { configuration.ToOpCode() }, 0, 1);
            Thread.Sleep(100);
        }
    }
}
