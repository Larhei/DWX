// (c) Licensed to HeiReS under one or more agreements.
// The HeiReS licenses this file to you under the MIT license.

using CommunityToolkit.Mvvm.ComponentModel;
using Full.Model;

namespace Full.Model
{
    public partial class CredentialItem : ObservableObject
    {
        private readonly Credential _credential = Credential.Empty;

        private long _userId;

        private string? _password;

        public CredentialItem()
        {
            UserId = int.MinValue;
            Password = string.Empty;
        }

        private CredentialItem(Credential credential)
            : this()
        {
            _credential = credential;
            UserId = _credential.UserId;
            Password = "xxxxx";
        }

        public long UserId
        {
            get { return _userId; }
            set { SetProperty(ref _userId, value); }
        }

        public string? Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public static CredentialItem Import(Credential credential)
        {
            return new CredentialItem(credential);
        }
    }
}
