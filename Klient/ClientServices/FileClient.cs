using Commons;
using Klient.ClientCommunicators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klient.ClientServices
{
    internal class FileClient : QAClient
    {
        public FileClient(ClientCommunicator clientCommunicator) : base(clientCommunicator)
        {

        }

        internal string putFile(string filePath)
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            string fileData = Convert.ToBase64String(bytes);
            string fileName = Path.GetFileName(filePath);

            string fileNameEncoded = CommonTools.EncodeToBase64(fileName);

            string question = $"file put {fileNameEncoded} {fileData}\n";

            string answer = clientCommunicator.QA(question);

            return answer;
        }

        internal string getFile(string fileName, string directoryPath = ".")
        {
            string fileNameEncoded = CommonTools.EncodeToBase64(fileName);

            string question = $"file get {fileNameEncoded}\n";

            string answer = clientCommunicator.QA(question);

            string _fileNameEncoded = CommonTools.GetParam(answer, 2);
            string _fileName = CommonTools.DecodeFromBase64(_fileNameEncoded);

            string fileData = CommonTools.GetParam(answer, 3);
            byte[] bytes = Convert.FromBase64String(fileData);

            File.WriteAllBytes(CommonTools.CreateFilePath(directoryPath, _fileName), bytes);

            return answer;
        }

        internal string dir()
        {
            string question = $"file dir\n";
            string answer = clientCommunicator.QA(question);
            string encodedAnswerData = CommonTools.GetParam(answer, 2);
            string answerData = CommonTools.DecodeFromBase64(encodedAnswerData);
            return $"file dir {answerData}\n";
        }

    }
}
