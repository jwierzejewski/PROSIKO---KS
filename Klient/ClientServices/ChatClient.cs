using Commons;
using Klient.ClientCommunicators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klient.ClientServices
{
    //Base64
    internal class ChatClient : QAClient
    {
        public ChatClient(ClientCommunicator clientCommunicator) : base(clientCommunicator)
        {

        }

        internal string SendMessage(string senderName, string recieverName, string message)
        {
            string question = $"chat msg";
            string data = $"{recieverName} {senderName} {message}";
            string encodedData = CommonTools.EncodeToBase64(data);
            question += $" {encodedData}\n";

            string answer = clientCommunicator.QA(question);
            string answerData = CommonTools.FromSpecifiedDelimeterToEnd(answer, 2);
            string decodedAnswer = CommonTools.DecodeFromBase64(answerData);

            return decodedAnswer;
        }

        internal string GetMessages(string recieverName)
        {
            string question = $"chat get";
            string data = $"{recieverName}";
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
