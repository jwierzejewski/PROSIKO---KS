using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Commons;

namespace Klient.ClientCommunicators
{
    internal class SerialPortCommunicator : ClientCommunicator
    {
        private SerialPort _serialPort;

        public SerialPortCommunicator(string PortName)
        {
            _serialPort = new SerialPort();

            _serialPort.PortName = PortName;
            _serialPort.ReadTimeout = 50000;
            _serialPort.WriteTimeout = 500;
        }

        public override string QA(string question)
        {
            string name;
            string message;

            _serialPort.Open();
            _serialPort.Write(question);
            string res = _serialPort.ReadLine();
            _serialPort.Close();

            return res;
        }
    }
}
