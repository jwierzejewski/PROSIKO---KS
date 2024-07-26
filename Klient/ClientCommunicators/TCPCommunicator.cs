using Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Klient.ClientCommunicators
{
    internal class TCPCommunicator : ClientCommunicator
    {
        private string hostname;
        private int port;
        TcpClient client;

        public TCPCommunicator(string hostname, int port)
        {
            this.hostname = hostname;
            this.port = port;
            client = new TcpClient(hostname, port);
        }

        public override string QA(string question)
        {
            byte[] data = Encoding.UTF8.GetBytes(question);
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);
            data = new byte[4096];
            string res = string.Empty;
            int bytes;
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                res += Encoding.UTF8.GetString(data, 0, bytes);


            } while (res.LastIndexOf('\n') == -1);
            return res;
        }
    }
}
