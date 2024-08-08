using Commons;
using Klient.ClientServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Klient.ClientCommunicators
{
    internal class UDPCommunicator : ClientCommunicator
    {
        private string hostname;
        private int portNo;

        public UDPCommunicator(string hostanme, int portNo) 
        {
            this.hostname = hostanme;
            this.portNo = portNo;
        }
        public override string QA(string question)
        {
            UdpClient udpClient = new UdpClient(hostname, portNo);
            udpClient.SendPackages(question);
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            string res = udpClient.RecievePackages(ref RemoteIpEndPoint);
            return res;
        }
    }
}
