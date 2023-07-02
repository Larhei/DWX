// (c) Licensed to HeiReS under one or more agreements.
// The HeiReS licenses this file to you under the MIT license.

using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Full.Contracts;
using Full.Model;

namespace Full.ViewModel
{
    public abstract partial class BaseViewModel : ObservableObject
    {
        protected BaseViewModel(IDispatcherService? dispatcherService)
        {
            if (IsInDesignTime)
            {
                return;
            }

            this.ThrowIfNull(dispatcherService);
            DispatcherService = dispatcherService;
        }

        public bool IsBusy
        {
            get
            {
                return Workers.Any();
            }
        }

        protected bool IsInDesignTime
        {
            get
            {
                return DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject());
            }
        }

        protected IDispatcherService DispatcherService { get; }

        private ConcurrentDictionary<string, Worker> Workers { get; } = new ConcurrentDictionary<string, Worker>();

        protected virtual void RefreshCommands()
        {
        }

        protected Worker ScheduleWork(CancellationToken? token = null, [CallerMemberName] string? workerName = null)
        {
            if (string.IsNullOrWhiteSpace(workerName))
            {
                workerName = GetType().Name;
            }

            Worker? scope = null;
            if (token == null)
            {
                scope = new Worker(new CancellationTokenSource(), UnscheduleWork);
            }
            else
            {
                scope = new Worker(CancellationTokenSource.CreateLinkedTokenSource(token.Value), UnscheduleWork);
            }

            if (!Workers.TryAdd(workerName, scope))
            {
                if (Workers.TryRemove(workerName, out Worker? existingScope))
                {
                    existingScope.Cancel();
                    existingScope.Dispose();
                }

                if (Workers.TryAdd(workerName, scope))
                {
                    throw new NotSupportedException("Unable to schedule worker.");
                }
            }

            DispatcherService?.InvokeOnMainThread(SignalWorkerChanged);
            return scope;
        }

        protected void UnscheduleWork(Worker worker)
        {
            var kv = Workers.FirstOrDefault(w => w.Value == worker);
            if (string.IsNullOrWhiteSpace(kv.Key))
            {
                return;
            }

            if (Workers.TryRemove(kv.Key, out Worker? currentWork))
            {
                worker?.Dispose();
            }

            DispatcherService?.InvokeOnMainThread(SignalWorkerChanged);
        }

        protected Task ExecuteOnUIThreadAsync(Action methodeToExecute, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            return DispatcherService?.InvokeOnMainThreadAsync(methodeToExecute) ?? Task.CompletedTask;
        }

        private void SignalWorkerChanged()
        {
            OnPropertyChanged(nameof(IsBusy));
            RefreshCommands();
        }
    }
}
