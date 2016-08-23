using System;
using System.Security.Cryptography;
using zavit.Domain.Clients.Tokens;

namespace zavit.Infrastructure.Clients.Tokens
{
    public class TokenSecurity : ITokenSecurity
    {
        public string HashTokenId(string tokenId)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            var byteValue = System.Text.Encoding.UTF8.GetBytes(tokenId);

            var byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }
    }
}