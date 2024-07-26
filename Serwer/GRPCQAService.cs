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
            //Console.WriteLine($"R: {question.Message.Length} B {question.Message.SubstringMax(40)}");
            string answer = onCommand(question.Message);
            //Console.WriteLine($"S: {answer.Length} B {answer.SubstringMax(40)}");
            return Task.FromResult(new Answer
            {
                Message = answer
            });
        }
    }
}
