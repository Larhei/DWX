// (c) Licensed to HeiReS under one or more agreements.
// The HeiReS licenses this file to you under the MIT license.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Contracts
{
    public interface IDispatcherService
    {
        void InvokeOnMainThread(Action signalWorkerChanged);

        Task InvokeOnMainThreadAsync(Action methodeToExecute);
    }
}
