
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace web.api.Service
{

    public interface IMailService {
        void Send(string subject, string msg);
    }


    public class LocalMailService:IMailService
    {
        private readonly string _mailTo = Startup.configuration["mailSettings:mailToAddress"];
        private readonly string _mailFrom = Startup.configuration["mailSettings:mailFromAddress"];

        public void Send(string subject, string msg)
        {
            Debug.WriteLine($"从{_mailFrom}给{_mailTo}通过{nameof(LocalMailService)}发送了邮件");
        }
    }


    public class CloudMailService : IMailService
    {
        private readonly string _mailTo = "admin@qq.com";
        private readonly string _mailFrom = "noreply@alibaba.com";
        private readonly ILogger logger;

        public CloudMailService(ILogger<CloudMailService> logger)
        {
            this.logger = logger;
        }

        public void Send(string subject, string msg)
        {
            logger.LogInformation("这里是云邮件");
            Debug.WriteLine($"从{_mailFrom}给{_mailTo}通过{nameof(LocalMailService)}发送了邮件");
        }
    }
}
