using Grpc.Core;
using GrpcServer;
using Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class GRPCQAService : QAService.QAServiceBase
    {
        private CommandD onCommand;
        public GRPCQAService(CommandD onCommand) 
        {
            this.onCommand = onCommand;
        }
        public override Task<Answer> SendQuestion(Question question, ServerCallContext context)
        {
            string answer = onCommand(question.Message);
            return Task.FromResult(new Answer
            {
                Message = answer
            });
        }
    }
}
