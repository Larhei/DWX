// (c) Licensed to HeiReS under one or more agreements.
// The HeiReS licenses this file to you under the MIT license.

using Core.Contracts;
using Core.Model;
using Moq;

namespace Unittests
{
    internal abstract class AbstractSecureCredentialsStorageTest<TU>
        where TU : ISecureCredentialsStorage
    {
        protected Mock<ISecureCredentialsStorage> Storage { get; private set; }

        [TestInitialize]
        public void Setup()
        {
            var mock = new Mock<ISecureCredentialsStorage>();
            mock.Setup(m => m.AddCredential(It.IsAny<Credential>())).Returns<Credential>(c => c == Credential.Empty);
            Storage = mock;
        }
    }
}
