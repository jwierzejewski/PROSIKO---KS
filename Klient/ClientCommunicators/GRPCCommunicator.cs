using Commons;
using Grpc.Net.Client;
using GrpcClient;
using Klient.ClientServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Klient.ClientCommunicators
{
    internal class GRPCCommunicator : ClientCommunicator
    {
        string hostname;
        int port;

        public GRPCCommunicator(string hostname, int port)
        {
            this.hostname = hostname;
            this.port = port;
        }

        public override string QA(string question)
        {
            using var channel = GrpcChannel.ForAddress($"{hostname}:{port}");
            var client = new QAService.QAServiceClient(channel);
            var reply = client.SendQuestion(new Question { Message = question });

            return reply.Message;
        }
    }
}
