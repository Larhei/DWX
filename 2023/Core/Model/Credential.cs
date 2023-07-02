// (c) Licensed to HeiReS under one or more agreements.
// The HeiReS licenses this file to you under the MIT license.
using System.Security;

namespace Core.Model
{
    public readonly record struct Credential(SecureString password, long UserId)
    {
        public static Credential Empty { get; } = new Credential(new SecureString(), int.MinValue);
    }
}
