using Klient.ClientCommunicators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klient.ClientServices
{
    internal class ConfClient : QAClient
    {
        public ConfClient(ClientCommunicator clientCommunicator) : base(clientCommunicator)
        {
        }

        internal string startedMediums()
        {
            string question = $"conf started-mediums-list\n";
            string answer = clientCommunicator.QA(question);
            return answer;
        }

        internal string stopMedium(string mediumName)
        {
            string question = $"conf stop-medium {mediumName}\n";
            string answer = clientCommunicator.QA(question);
            return answer;
        }

        internal string startMedium(string mediumName, object mediumAddress)
        {
            string question = $"conf start-medium {mediumName} {mediumAddress}\n";
            string answer = clientCommunicator.QA(question);
            return answer;
        }

        internal string stopService(string serviceName)
        {
            string question = $"conf stop-service {serviceName}\n";
            string answer = clientCommunicator.QA(question);
            return answer;
        }

        internal string startService(string serviceName)
        {
            string question = $"conf start-service {serviceName}\n";
            string answer = clientCommunicator.QA(question);
            return answer;
        }

        internal string startedServices()
        {
            string question = $"conf started-services-list\n";
            string answer = clientCommunicator.QA(question);
            return answer;
        }
    }
}
