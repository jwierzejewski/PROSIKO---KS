using Klient.ClientCommunicators;
using Klient.ClientServices;
using System.Text;

Thread.Sleep(1000);
TCPCommunicator tcpCommunicator = new TCPCommunicator("localhost",12345);
SerialPortCommunicator spCommunicator = new SerialPortCommunicator("COM2");
FileSystemCommunicator fsCommunicator = new();
UDPCommunicator udpCommunicator = new UDPCommunicator("localhost", 12346);
GRPCCommunicator grpcCommunicator = new GRPCCommunicator("http://localhost", 12347);



ConfClient cc = new ConfClient(fsCommunicator);
Console.WriteLine(cc.startService("chat"));
Console.WriteLine(cc.startService("file"));
Console.WriteLine(cc.startService("ping"));



int amount = 100;
int outputLen = 8192;
int inputLen = 8192;

PingClient pc1 = new PingClient(fsCommunicator);
var duration = pc1.Test(amount, outputLen, inputLen);
Console.WriteLine($"FileSystem Ping client average time: {duration}");

PingClient pc2 = new PingClient(tcpCommunicator);
duration = pc2.Test(amount, outputLen, inputLen);
Console.WriteLine($"TCP Ping client average time: {duration}");

PingClient pc3 = new PingClient(udpCommunicator);
duration = pc3.Test(amount, outputLen, inputLen);
Console.WriteLine($"UDP Ping client average time: {duration}");

PingClient pc4 = new PingClient(grpcCommunicator);
duration = pc4.Test(amount, outputLen, inputLen);
Console.WriteLine($"gRPC Ping client average time: {duration}");

PingClient pc5 = new PingClient(spCommunicator);
duration = pc5.Test(amount, outputLen, inputLen);
Console.WriteLine($"SerialPort Ping client average time: {duration}");



Console.WriteLine(cc.startMedium("udp", 12348));

Console.WriteLine(cc.startedMediums());
Console.WriteLine(cc.startedServices());
UDPCommunicator udpCommunicator2 = new UDPCommunicator("localhost", 12348);
Console.WriteLine(cc.stopMedium("UDP:12346"));
Console.WriteLine(cc.stopMedium("SerialPort:COM1"));
Console.WriteLine(cc.startedMediums());



ChatClient chc = new ChatClient(grpcCommunicator);
Console.WriteLine(chc.SendMessage("Jan", "Adam", "Cześć"));
Console.WriteLine(chc.GetMessages("Adam"));
Console.WriteLine(chc.Who());

FileClient fc = new FileClient(udpCommunicator2);
Console.Write(fc.putFile("D:\\jacek\\jacek\\systemy operacyjne\\so.pdf"));
Console.Write(fc.dir());
Console.Write(fc.getFile("raw-material-properties.csv"));


ConfClient cc2 = new ConfClient(grpcCommunicator);
Console.WriteLine(cc2.stopService("chat"));
