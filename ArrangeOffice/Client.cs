using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;




namespace ArrangeOffice
{
    class Client
    {
        private Socket clientSocket;
        private Thread clientThread;
        private TCPListener tcpListener;
        private bool error = false;
        public Client()
        {

            int port = Properties.Settings.Default.PORT;
            string ip = Properties.Settings.Default.IP;



            IPEndPoint connectIP =
                new IPEndPoint(IPAddress.Parse(ip), port);

            try
            {
                //socket
                clientSocket = new Socket(AddressFamily.InterNetwork
                              , SocketType.Stream, ProtocolType.Tcp);
                //send or receive before connect
                clientSocket.Connect(connectIP);

                tcpListener = new TCPListener();
                tcpListener.push(clientSocket);
                clientThread = new Thread(new ThreadStart(tcpListener.run));
                clientThread.Start();


            }
            catch (SocketException exception)
            {
                error = true;
                clientSocket.Close();
                Console.WriteLine("Socket error", exception);
                //      throw exception;
            }

        }
        //send message
        public void run(object str)
        {
            try
            {
                byte[] msgByte = Encoding.UTF8.GetBytes(str.ToString());
                clientSocket.Send(msgByte);
                
            }
            catch (ObjectDisposedException exception)
            {
                clientSocket.Close();
                Console.WriteLine("Socket send error", exception);
            }

        }

        public void stop()
        {
            if (clientSocket != null && clientSocket.Connected)
            {

                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
                clientThread.Abort();
                clientThread.Join();
            }


        }

        public void pushForm(ArrangeOffice form)
        {
            tcpListener.pushForm(form);
        }

        public bool isError()
        {
            if (error == true)
            {
                return true;

            }
            return false;
        }
    }
}
