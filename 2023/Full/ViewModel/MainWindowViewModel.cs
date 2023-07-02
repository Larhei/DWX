// (c) Licensed to HeiReS under one or more agreements.
// The HeiReS licenses this file to you under the MIT license.
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Full.Contracts;
using Full.Model;

namespace Full.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        // Observable Property from CommunityToolkit does not work here.
        // See https://github.com/CommunityToolkit/dotnet/issues/158
        private ObservableCollection<CredentialItem> _allCredentials = new ObservableCollection<CredentialItem>();
        private string _currentUserName = string.Empty;
        private AsyncRelayCommand? _loadDataCommand;

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

            this.ThrowIfNull(credentialsStorage);
            CredentialsStorage = credentialsStorage;
        }

        public ObservableCollection<CredentialItem> AllCredentials
        {
            get { return _allCredentials; }
            set { SetProperty(ref _allCredentials, value); }
        }

        public string CurrentUserName
        {
            get { return _currentUserName; }
            set { SetProperty(ref _currentUserName, value); }
        }

        public IAsyncRelayCommand LoadDataCommand
        {
            get
            {
                return _loadDataCommand ??= new AsyncRelayCommand(LoadData, CanLoadData);
            }
        }

        private ICredentialsStorage? CredentialsStorage { get; }

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
