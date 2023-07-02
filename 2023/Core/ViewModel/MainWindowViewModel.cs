// (c) Licensed to HeiReS under one or more agreements.
// The HeiReS licenses this file to you under the MIT license.
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Core.Contracts;
using Core.Model;

namespace Core.ViewModel
{
    public partial class MainWindowViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<CredentialItem> _allCredentials = new ObservableCollection<CredentialItem>();

        [ObservableProperty]
        private string _currentUserName;

        public MainWindowViewModel()
            : base(null)
        {
        }

        public MainWindowViewModel(ICredentialsStorage? credentialsStorage, IDispatcherService dispatcherService)
            : base(dispatcherService)
        {
            if (IsInDesignTime)
            {
                return;
            }

            ArgumentNullException.ThrowIfNull(credentialsStorage);
            CredentialsStorage = credentialsStorage;
        }

        private ICredentialsStorage? CredentialsStorage { get; }

        [CommunityToolkit.Mvvm.Input.RelayCommand(
            CanExecute = nameof(CanLoadData),
            AllowConcurrentExecutions = false,
            IncludeCancelCommand = false)]
        private async Task LoadData(CancellationToken token)
        {
            using (var worker = ScheduleWork(token))
            {
                try
                {
                    var credentials = await (CredentialsStorage?.GetCredentialsAsync(worker.Token) ??
                                            Task.FromResult<IEnumerable<Credential>>(Array.Empty<Credential>()))
                                            .ConfigureAwait(false);

                    var userName = Environment.UserName;

                    await ExecuteOnUIThreadAsync(
                        () => LoadDataOnUI(userName, credentials),
                        token).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    // TODO: Log
                }
                catch (ObjectDisposedException)
                {
                    // TODO: Log
                }
                catch (Exception)
                {
                    // TODO: Log
                }
                finally
                {
                    // UnscheduleWork(worker);
                }
            }
        }

        private bool CanLoadData()
        {
            return !IsBusy;
        }

        private void LoadDataOnUI(string userName, IEnumerable<Credential> credentials)
        {
            CurrentUserName = userName;
            AllCredentials = new ObservableCollection<CredentialItem>((credentials ?? Array.Empty<Credential>())
                .Select(x => CredentialItem.Import(x)));
        }
    }
}
