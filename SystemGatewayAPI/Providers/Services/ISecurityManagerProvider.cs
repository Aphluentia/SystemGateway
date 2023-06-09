﻿using SystemGateway.Dtos.SecurityManager;

namespace SystemGateway.Providers
{
    public interface ISecurityManagerProvider
    {
        public Task<string> GenerateSession(SecurityDataDto securityData);
        public Task<string> KeepAlive(string Token);
        public Task<string> ValidateSession(string Token);
        public Task<SecurityDataDto> GetTokenData(string Token);

    }
}
