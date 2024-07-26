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
            UdpClient udpClient = new UdpClient();
            udpClient.Connect(hostname, portNo);
            byte[] numberBytes, countBytes;
            udpClient.SendPackages(question);
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            string res = udpClient.RecievePackages(ref RemoteIpEndPoint);
            return res;
        }

        public List<byte[]> createPackages(byte[] fullData)
        {
            int packageSize = 65536;
            int dataStart = 0;
            List<byte[]> packageList = new();
            while (dataStart<fullData.Length)
            {
                byte[] package = fullData.Skip(dataStart).Take(Math.Min(packageSize,fullData.Length-dataStart)).ToArray();
                packageList.Add(package);
                dataStart += packageSize;
            }
            return packageList;
        }
    }
}
