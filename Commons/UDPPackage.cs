using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Commons
{
    public static class UDPPackage
    {
        public static void SendPackages(this UdpClient udpClient, string data, IPEndPoint endPoint = null)
        {
            byte[] countBytes, numberBytes;
            int MaxSize = 65507;
            int ConfSize = 8;
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            var packageList = createPackages(dataBytes, MaxSize - ConfSize);
            for (int j = 0; j < packageList.Count; j++)
            {
                byte[] packageData = packageList[j];
                byte[] sendingPackage = new byte[packageData.Length + 8];

                countBytes = BitConverter.GetBytes(packageList.Count());
                countBytes.CopyTo(sendingPackage, 0);

                numberBytes = BitConverter.GetBytes(j);
                numberBytes.CopyTo(sendingPackage, 4);

                packageData.CopyTo(sendingPackage, 8);

                udpClient.Send(sendingPackage, sendingPackage.Length, endPoint);
                if (packageList.Count - 1 != j)
                    Thread.Sleep(1);
            }
        }

        public static string RecievePackages(this UdpClient udpClient, ref IPEndPoint remoteIpEndPoint)
        {
            string res;
            int count, number;
            int i = 0;
            byte[] data;
            LinkedList<(int, string)> receivedPackages = new();

            data = udpClient.Receive(ref remoteIpEndPoint);
            count = processPackage(data, receivedPackages);
            i++;

            while (count > i)
            {
                data = udpClient.Receive(ref remoteIpEndPoint);
                processPackage(data, receivedPackages);
                i++;
            }

            var sortedPackages = receivedPackages.OrderBy(x => x.Item1).Select(x => x.Item2);
            res = string.Concat(sortedPackages);

            return res;
        }

        private static int processPackage(byte[] data, LinkedList<(int, string)> receivedPackages)
        {
            byte[] countBytes = new byte[4];
            byte[] numberBytes = new byte[4];

            Array.Copy(data, 0, countBytes, 0, 4);
            int count = BitConverter.ToInt32(countBytes, 0);
            Array.Copy(data, 4, numberBytes, 0, 4);
            int number = BitConverter.ToInt32(numberBytes, 0);
            string message = Encoding.UTF8.GetString(data, 8, data.Length - 8);

            receivedPackages.AddLast((number, message));

            return count;
        }

        private static List<byte[]> createPackages(byte[] fullData, int packageSize)
        {
            int dataStart = 0;
            List<byte[]> packageList = new();
            while (dataStart < fullData.Length)
            {
                byte[] package = fullData.Skip(dataStart).Take(Math.Min(packageSize, fullData.Length - dataStart)).ToArray();
                packageList.Add(package);
                dataStart += packageSize;
            }
            return packageList;
        }
    }
}
