// (c) Licensed to HeiReS under one or more agreements.
// The HeiReS licenses this file to you under the MIT license.

using System;
using System.Threading.Tasks;

namespace Full.Contracts
{
    public interface IDispatcherService
    {
        void InvokeOnMainThread(Action signalWorkerChanged);

        Task InvokeOnMainThreadAsync(Action methodeToExecute);
    }
}
