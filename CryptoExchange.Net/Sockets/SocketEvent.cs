﻿using CryptoExchange.Net.Objects;
using System.Threading;

namespace CryptoExchange.Net.Sockets
{
    public class SocketEvent
    {
        public string Name { get; set; }

        private CallResult<bool> result;
        private ManualResetEvent setEvnt;

        public SocketEvent(string name)
        {
            Name = name;
            setEvnt = new ManualResetEvent(false);
            result = new CallResult<bool>(false, new UnknownError("No response received"));
        }

        public void Set(bool result, Error error)
        {
            this.result = new CallResult<bool>(result, error);
            setEvnt.Set();
        }
        
        public CallResult<bool> Wait(int timeout = 5000)
        {
            setEvnt.WaitOne(timeout);
            return result;
        }

        public void Reset()
        {
            setEvnt.Reset();
            result = new CallResult<bool>(false, new UnknownError("No response received"));
        }
    }
}
