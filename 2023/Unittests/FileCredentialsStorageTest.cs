// (c) Licensed to HeiReS under one or more agreements.
// The HeiReS licenses this file to you under the MIT license.

using Core.Contracts;
using Core.Model;
using Moq;
//[assembly: DiscoverInternals]

namespace Unittests
{
    [TestClass]
    internal class FileCredentialsStorageTest : AbstractSecureCredentialsStorageTest<ISecureCredentialsStorage>
    {
        [TestMethod]
        public void TestAddCredentials()
        {
            var result = Storage.Object.AddCredential(Credential.Empty);
            Storage.Verify(s => s.AddCredential(It.IsAny<Credential>()), Times.Once());
            Assert.IsTrue(result, "Add did not return true.");
        }
    }
}
