using Commons;
using GrpcServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Server.Communicators
{
    internal class UDPCommunicator : ICommunicator
    {
        private int portNo;
        private UdpClient udpClient;
        private CommandD onCommand;
        private CommunicatorD onDisconnect;
        private Thread _thread;
        private bool shouldTerminate = false;

        public UDPCommunicator(int portNo) 
        {
            this.portNo = portNo;
        }


        public void Start(CommandD onCommand, CommunicatorD onDisconnect)
        {
            this.onCommand = onCommand;
            this.onDisconnect = onDisconnect;
            udpClient = new UdpClient(this.portNo);
            _thread = new Thread(Communicate);
            _thread.Start();

            
        }

        private void Communicate()
        {
            while(!shouldTerminate)
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                string message = udpClient.RecievePackages(ref RemoteIpEndPoint);
                string answer = onCommand(message);
                udpClient.SendPackages(answer, RemoteIpEndPoint);
            }
            if (udpClient.Client.Connected)
            {
                udpClient.Close();
                onDisconnect(this);
            }
        }

        public void Stop()
        {
            shouldTerminate = true;
        }
    }
}
