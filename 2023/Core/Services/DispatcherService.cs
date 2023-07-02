// (c) Licensed to HeiReS under one or more agreements.
// The HeiReS licenses this file to you under the MIT license.

using System;
using System.Threading.Tasks;
using System.Windows.Threading;
using Core.Contracts;

namespace Core.Services
{
    public class DispatcherService : IDispatcherService
    {
        public DispatcherService(Dispatcher dispatcher)
        {
            ArgumentNullException.ThrowIfNull(dispatcher);
            Dispatcher = dispatcher;
        }

        public Dispatcher Dispatcher { get; }

        public void InvokeOnMainThread(Action action)
        {
            if (Dispatcher.HasShutdownStarted)
            {
                return;
            }

            switch (Dispatcher.CheckAccess())
            {
                case true:
                    action?.Invoke();
                    break;
                default:
                    Dispatcher.Invoke(action);
                    break;
            }
        }

        public Task InvokeOnMainThreadAsync(Action action)
        {
            if (Dispatcher.HasShutdownStarted)
            {
                return Task.CompletedTask;
            }

            switch (Dispatcher.CheckAccess())
            {
                case true:
                    action?.Invoke();
                    return Task.CompletedTask;
                default:
                    return Dispatcher.InvokeAsync(action).Task;
            }
        }
    }
}
