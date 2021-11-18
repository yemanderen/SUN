using Grpc.Core;
using GrpcService2.Protos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService2.Services
{
    // 这里继承proto文件生成的服务抽象类，需要自己实现
    public class FirstService : ProFirst.ProFirstBase
    {
        private readonly ILogger<FirstService> _logger;
        public FirstService(ILogger<FirstService> logger)
        {
            _logger = logger;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override Task<LxpReply> GetUserInfo(LxpRequest request, ServerCallContext context)
        {
            return Task.FromResult(new LxpReply { Message = "LXP  我爱你" });

        }

        public override Task<LxpReply> SayHello(LxpRequest request, ServerCallContext context)
        {
            return Task.FromResult(new LxpReply { Message = "LXP 你好" });
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
