﻿namespace Tsi.Erp.TestTracker.Core
{
    public class MailServiceOption
    {
        public string Host {  get; set; }
        public int Port { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
    }
}