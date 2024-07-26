using Server.Communicators;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Listeners
{
    internal class GRPCListener : IListener
    {
        int portNo;
        GRPCCommunicator grpcCommunicator;
        public GRPCListener(object portNo)
        {
            if (portNo == null)
                this.portNo = 12346;
            else
                this.portNo = int.Parse(portNo.ToString());
        }

        public string Name => $"GRPC:{portNo}";

        public void Start(CommunicatorD onConnect)
        {
            grpcCommunicator = new GRPCCommunicator(portNo);
            onConnect(grpcCommunicator);
        }

        public void Stop()
        {
            grpcCommunicator.Stop();
        }
    }
}
