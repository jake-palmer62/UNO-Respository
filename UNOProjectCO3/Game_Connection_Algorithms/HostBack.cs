using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace UNOProjectCO3
{
    public abstract class HostBackEnd : IDisposable
    {
        public const int myPort = 55001; //Specifies the port used for UDP sockets.
        UdpClient UDP;
        ManualResetEvent resetEvent = new ManualResetEvent(true);
        bool disposed;
        Thread Listener; //Listener thread so that the host can connect to the game.
        public IPEndPoint Address { get { return UDP.Client.LocalEndPoint as IPEndPoint; } }
        protected abstract void DataRecieved(IPEndPoint ep, BinaryReader data);

        protected void Send(MemoryStream ms, IPEndPoint ep)
        {
            Send(ms.ToArray(), ep); //Sends data to and from sockets using the memory stream class
        }

        protected void Send(byte[] data, IPEndPoint ep)
        {
            if (!disposed)
            {
                UDP.Send(data, data.Length, ep);
            }

        }

        public HostBackEnd(int port = 0)
        {
            UDP = new UdpClient();
            UDP.ExclusiveAddressUse = false;
            UDP.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            if (port > 0)
            {
                UDP.Client.Bind(new IPEndPoint(IPAddress.Any, port));
            }
            InitListener();

        }

        public void ListenerTh() // Creates the listener thread for the host
        {
            while (!UDP.Client.IsBound)
            {
                Thread.Sleep(250);
            }

            while (UDP.Client != null && resetEvent.WaitOne(0))
            {
                byte[] data = null;
                IPEndPoint targetAddress = null;
                try
                {
                    data = UDP.Receive(ref targetAddress);
                }
                catch (SocketException theException)

                {
                    if (resetEvent.WaitOne(0))
                    {
                        throw theException;
                    }
                }
                if (data != null)
                {
                    using (var ms = new MemoryStream(data))
                    using (var br = new BinaryReader(ms))
                        DataRecieved(targetAddress, br);
                }
            }
        }

        public void InitListener() // Initialises listener thread.
        {
            if (Listener != null && Listener.IsAlive)
                return;
            Listener = new Thread(ListenerTh);
            Listener.IsBackground = true;
            Listener.Start();
        }

        public virtual void Dispose()
        {
            resetEvent.Reset();
            disposed = true;
            UDP.Close();
        }

        HostBackEnd()
        {
            Dispose();
        }
    }
}
