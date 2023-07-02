// (c) Licensed to HeiReS under one or more agreements.
// The HeiReS licenses this file to you under the MIT license.

using System;
using System.Threading;

namespace Full.Model
{
    public sealed record Worker : IDisposable
    {
        private bool _isDisposed;

        public Worker(CancellationTokenSource tokenSource, Action<Worker> cleanupHandler)
        {
            this.ThrowIfNull(nameof(tokenSource));
            this.ThrowIfNull(nameof(cleanupHandler));
            TokenSource = tokenSource;
            CleanupHandler = cleanupHandler;
        }

        ~Worker()
        {
            Dispose(false);
        }

        private CancellationTokenSource TokenSource { get; }
        private Action<Worker> CleanupHandler { get; }

        public CancellationToken Token
        {
            get { return TokenSource.Token; }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Cancel()
        {
            if (_isDisposed || TokenSource.IsCancellationRequested)
            {
                return;
            }

            TokenSource.Cancel();
        }

        private void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;

            if (disposing)
            {
                CleanupHandler.Invoke(this);

                // free managed resources
                TokenSource.Dispose();
            }
        }
    }
}
