using Commons;
using Klient.ClientCommunicators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klient.ClientServices
{
    internal class ChatClient : QAClient
    {
        public ChatClient(ClientCommunicator clientCommunicator) : base(clientCommunicator)
        {

        }

        internal string SendMessage(string senderName, string receiverName, string message)
        {
            string question = $"chat msg";
            string data = $"{receiverName} {senderName} {message}";
            string encodedData = CommonTools.EncodeToBase64(data);
            question += $" {encodedData}\n";

            string answer = clientCommunicator.QA(question);
            string answerData = CommonTools.FromSpecifiedDelimeterToEnd(answer, 2);
            string decodedAnswer = CommonTools.DecodeFromBase64(answerData);

            return decodedAnswer;
        }

        internal string GetMessages(string receiverName)
        {
            string question = $"chat get";
            string data = $"{receiverName}";
            string encodedData = CommonTools.EncodeToBase64(data);
            question += $" {encodedData}\n";

            string answer = clientCommunicator.QA(question);
            string answerData = CommonTools.FromSpecifiedDelimeterToEnd(answer, 2);
            string encodedAnswer = CommonTools.DecodeFromBase64(answerData);

            return encodedAnswer;
        }

        internal string Who()
        {
            string question = $"chat who\n";
            string answer = clientCommunicator.QA(question);
            string answerData = CommonTools.FromSpecifiedDelimeterToEnd(answer, 2);
            string encodedAnswer = CommonTools.DecodeFromBase64(answerData);
            return encodedAnswer;
        }
    }
}
