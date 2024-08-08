using Commons;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klient.ClientCommunicators
{
    internal class FileSystemCommunicator : ClientCommunicator
    {
        private string repoDirectoryPath;
        private string fileName;
        private string answer = string.Empty;
        object locker;

        public FileSystemCommunicator(string repoDirectoryPath = "D:\\jacek\\jacek\\Prosiko\\FileSystemRepo")
        {
            locker = new object();
            this.repoDirectoryPath = repoDirectoryPath;
        }

        public override string QA(string question)
        {
            this.fileName = $"{DateTime.Now:yyyyMMddHHmmssfff}_{new Random().Next(0, 9999)}";
            answer = string.Empty;
            FileSystemWatcher watcher = new FileSystemWatcher(repoDirectoryPath);
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = $"{fileName}.out";
            watcher.Changed += onChanged;

            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;

            File.AppendAllText(Path.Combine(repoDirectoryPath, fileName + ".in"), question);

            lock (locker)
            {
                Monitor.Wait(locker);
            }
            File.Delete(Path.Combine(repoDirectoryPath, fileName + ".in"));

            return answer;
        }

        private void onChanged(object sender, FileSystemEventArgs e)
        {
            lock (locker)
            {
                try
                {
                    string line = File.ReadLines(e.FullPath).First();
                    answer = line;
                }
                catch (IOException ex)
                {
                    Console.WriteLine("File Loading failure: " + ex.Message);
                }
                Monitor.Pulse(locker);
            }

        }
    }
}
