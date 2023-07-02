// (c) Licensed to HeiReS under one or more agreements.
// The HeiReS licenses this file to you under the MIT license.

using CommunityToolkit.Mvvm.ComponentModel;

namespace Core.Model
{
    public partial class CredentialItem : ObservableObject
    {
        private readonly Credential _credential = Credential.Empty;

        [ObservableProperty]
        private long _userId;

        [ObservableProperty]
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

        public static CredentialItem Import(Credential credential)
        {
            return new CredentialItem(credential);
        }
    }
}
