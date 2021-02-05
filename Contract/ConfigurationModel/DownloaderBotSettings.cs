using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.ConfigurationModel
{
    public class DownloaderBotSettings
    {
        public RMQConnectionSettings RMQConnectionSettings { get; set; }
        public RMQQueueSettings RMQQueueSettings { get; set; }        
    }

    public class RMQConnectionSettings
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
    }

    public class RMQQueueSettings
    {
        public string RoutingKey { get; set; }
    }
}
