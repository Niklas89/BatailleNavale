using System;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace ClientSocketApp
{
    class Program
    {

        static void Main(string[] args)
        {
        connection:
            try
            {
                // On créé un objet de type TcpClient défini par son host et son port
                TcpClient client = new TcpClient("127.0.0.1", 1302);
                //int nb = 2;
                string text = "test";
                string messageToSend = text; // Message à envoyer
                int byteCount = Encoding.ASCII.GetByteCount(messageToSend + 1);
                byte[] sendData = Encoding.ASCII.GetBytes(messageToSend);

                //Envoi de message
                NetworkStream stream = client.GetStream();
                stream.Write(sendData, 0, sendData.Length);
                Console.WriteLine("sending data to server...");

                // Lecture de messages
                StreamReader sr = new StreamReader(stream);
                string response = sr.ReadLine();
                // Console.WriteLine(response);
                AfficheMessage(response);

                stream.Close();
                client.Close();
                Console.ReadKey();
            }
            catch (Exception)
            {
                Console.WriteLine("failed to connect...");
                goto connection;
            }
        }

        public static void AfficheMessage(string reponse)
        {
            foreach (char i in reponse)
                Console.WriteLine(i);
        }
    }
}