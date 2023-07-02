// (c) Licensed to HeiReS under one or more agreements.
// The HeiReS licenses this file to you under the MIT license.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Full.Contracts;
using Full.Model;

namespace Full.Services
{
    internal class CredentialsStorage : ICredentialsStorage
    {
        private readonly ConcurrentDictionary<long, Credential> _credentials = new ConcurrentDictionary<long, Credential>();

        public CredentialsStorage(Credential credentials)
        {
            _credentials = new ConcurrentDictionary<long, Credential>(
                new Dictionary<long, Credential>()
                {
                    { credentials.UserId, credentials },
                });
        }

        public async Task<IEnumerable<Credential>> GetCredentialsAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            await Task.Delay(2000, token).ConfigureAwait(false);
            return _credentials.Values.ToArray();
        }

        public async Task<Credential?> GetCredentialsByUserIdAsync(long userId, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            await Task.Delay(2000, token).ConfigureAwait(false);
            if (_credentials.TryGetValue(userId, out Credential credentials))
            {
                return credentials;
            }

            return null;
        }
    }
}
