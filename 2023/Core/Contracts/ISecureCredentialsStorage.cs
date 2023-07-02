// (c) Licensed to HeiReS under one or more agreements.
// The HeiReS licenses this file to you under the MIT license.

using Core.Model;

namespace Core.Contracts
{
    internal interface ISecureCredentialsStorage
    {
        bool AddCredential(Credential credential);
    }
}
