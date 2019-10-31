using System;
using System.Net;
using System.Net.Sockets;

namespace TcpIp
{
    class ServerProgram
    {
        private TcpClient client;

        private readonly byte[] bytes = new byte[256];

        private readonly TcpListener server;

        NetworkStream stream;

        public ServerProgram()
        {
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            server = new TcpListener(localAddr, 13000);
            server.Start();
        }

        public static void Main()
        {
            ServerProgram serverProgram = new ServerProgram();

            while (true)
            {
                serverProgram.Open();

                string receivedData = serverProgram.Receive();
                serverProgram.Write("Received", receivedData);

                string responseData = serverProgram.Process(receivedData);

                serverProgram.Send(responseData);
                serverProgram.Write("Sent", responseData);

                serverProgram.Close();
            }
        }

        private string Process(string data)
        {
            return data.ToUpper();
        }

        private void Open()
        {
            Console.WriteLine("Waiting for a connection... ");

            client = server.AcceptTcpClient();
            Console.WriteLine("Connected!");
        }

        private void Close()
        {
            client.Close();
            Console.WriteLine("Disconnected!");
        }

        private void Send(string data)
        {
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

            stream.Write(msg, 0, msg.Length);
        }

        private string Receive()
        {
            stream = client.GetStream();

            int bytesRead;

            bytesRead = stream.Read(bytes, 0, bytes.Length);

            return System.Text.Encoding.ASCII.GetString(bytes, 0, bytesRead);

        }

        private void Write(string @event, string data)
        {
            Console.WriteLine($"{@event}: {data}");
        }
    }
}