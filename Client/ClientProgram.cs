using System;
using System.Net.Sockets;

namespace TcpIp
{
    class ClientProgram
    {
        private TcpClient client;
        private byte[] data;
        private NetworkStream stream;

        static void Main()
        {
            ClientProgram clientProgram = new ClientProgram();

            while (true)
            {
                string requestData = clientProgram.Read();

                clientProgram.Open();

                clientProgram.Send(requestData);
                string responseData = clientProgram.Receive();

                clientProgram.Close();

                clientProgram.Write(responseData);
            }
        }

        private void Open()
        {            
            int port = 13000;
            client = new TcpClient("127.0.0.1", port);
        }

        private void Close()
        {
            stream.Close();
            client.Close();
        }

        private string Read()
        {
            Console.Write("Send: ");

            return Console.ReadLine();
        }

        private void Send(string message)
        {
            data = System.Text.Encoding.ASCII.GetBytes(message);

            stream = client.GetStream();

            stream.Write(data, 0, data.Length);
        }

        private string Receive()
        {
            data = new byte[256];

            int bytes = stream.Read(data, 0, data.Length);
            
            return System.Text.Encoding.ASCII.GetString(data, 0, bytes);

        }

        private void Write(string message)
        {
            Console.WriteLine("Received: {0}", message);
        }
    }
}
