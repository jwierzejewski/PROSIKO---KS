using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commons;
using Klient.ClientCommunicators;

namespace Klient.ClientServices
{
    internal class PingClient : QAClient
    {
        public PingClient(ClientCommunicator clientCommunicator) : base(clientCommunicator)
        {

        }

        internal double Test(int amount, int outputLen, int inputLen)
        {
            string question = $"ping {inputLen} ";
            question += CommonTools.Trush(outputLen - question.Length - 1);
            question += "\n";

            var startTime = DateTime.Now;
            for (int i = 0; i < amount; i++)
            {
                string answer = clientCommunicator.QA(question);
            }
            var endTime = DateTime.Now;
            var duration = (endTime - startTime).TotalSeconds / amount;

            return duration;
        }
    }
}
