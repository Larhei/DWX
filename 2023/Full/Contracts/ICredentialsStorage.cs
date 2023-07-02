// (c) Licensed to HeiReS under one or more agreements.
// The HeiReS licenses this file to you under the MIT license.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Full.Model;

namespace Full.Contracts
{
    public interface ICredentialsStorage
    {
        Task<Credential?> GetCredentialsByUserIdAsync(long userId, CancellationToken token);

        Task<IEnumerable<Credential>> GetCredentialsAsync(CancellationToken token);
    }
}
