using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcServer;

namespace Server.Communicators
{
    internal class GRPCCommunicator : ICommunicator
    {
        int portNo;
        Grpc.Core.Server server;
        private CommandD onCommand;
        private CommunicatorD onDisconnect;

        public GRPCCommunicator(int portNo) 
        {
            this.portNo = portNo;
        }
        public void Start(CommandD onCommand, CommunicatorD onDisconnect)
        {
            this.onCommand = onCommand;
            this.onDisconnect = onDisconnect;
            var channelOptions = new List<ChannelOption>();
            channelOptions.Add(new ChannelOption("grpc.max_receive_message_length", -1));
            server = new Grpc.Core.Server(channelOptions)
            {
                Services = { QAService.BindService(new GRPCQAService(onCommand)) },
                Ports = { new ServerPort("localhost", portNo, ServerCredentials.Insecure) }
            };
            server.Start();
        }

        public void Stop()
        {
            server.ShutdownAsync().Wait();
            onDisconnect(this);
        }
    }
}
