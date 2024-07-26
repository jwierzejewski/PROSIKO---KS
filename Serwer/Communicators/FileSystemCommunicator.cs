using Commons;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Communicators
{
    internal class FileSystemCommunicator : ICommunicator
    {
        private string repoDirectoryPath;
        private CommandD onCommand;
        private CommunicatorD onDisconnect;
        private Thread _thread;
        FileSystemWatcher watcher;

        public FileSystemCommunicator(string repoDirectoryPath) 
        {
            this.repoDirectoryPath = repoDirectoryPath;
        }

        public void Start(CommandD onCommand, CommunicatorD onDisconnect)
        {
            this.onCommand = onCommand;
            this.onDisconnect = onDisconnect;
            _thread = new Thread(Communicate);
            _thread.Start();
        }

        private void Communicate(object? obj)
        {
            watcher = new FileSystemWatcher(repoDirectoryPath);
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            watcher.Filter = "*.in";
            watcher.Changed += onChanged;
            watcher.Deleted += onDeleted;

            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
        }

        private void onDeleted(object sender, FileSystemEventArgs e)
        {
            File.Delete(CreateReponseFilePath(e.FullPath));
        }

        private void onChanged(object sender, FileSystemEventArgs e)
        {
            string[] lines = File.ReadAllLines(e.FullPath);
            string[] answers = new string[lines.Length];
            for(int i=0;i<lines.Length;i++) {
                if (!string.IsNullOrWhiteSpace(lines[i]))
                {
                    string answer = this.onCommand(lines[i]);
                    answers[i] = answer;
                }
            }
            File.WriteAllText(CreateReponseFilePath(e.FullPath), string.Join("", answers));
        }

        private string CreateReponseFilePath(string fullPath)
        {
            return Path.Combine(Path.GetDirectoryName(fullPath), Path.GetFileNameWithoutExtension(fullPath)+".out");
        }

        public void Stop()
        {
            watcher.Dispose();
            onDisconnect(this);
        }
    }
}
