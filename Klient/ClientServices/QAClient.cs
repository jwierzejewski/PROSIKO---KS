using Klient.ClientCommunicators;

namespace Klient.ClientServices
{
    internal class QAClient
    {
        protected ClientCommunicator clientCommunicator;

        public QAClient(ClientCommunicator clientCommunicator)
        {
            this.clientCommunicator = clientCommunicator;
        }
    }
}