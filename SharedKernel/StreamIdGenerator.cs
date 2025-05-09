using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel
{
    public static class StreamIdGenerator
    {
        public static Guid GenerateStreamId(List<string> values)
        {
            // Combina os GUIDs em um único hash determinístico
            using var sha = SHA256.Create();
            var inputBytes = Encoding.UTF8.GetBytes($"{String.Join('-',values)}");
            var hash = sha.ComputeHash(inputBytes);
            return new Guid([.. hash.Take(16)]); // Pega os 16 primeiros bytes e forma um Guid
        }
    }
}
