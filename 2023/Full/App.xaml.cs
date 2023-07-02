using System;
using System.Security;
using System.Windows;
using Full.Services;
using Full.ViewModel;
using Microsoft.Extensions.Configuration;

namespace Full
{
    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

#if DEBUG
            // TODO: Make Binding Error
            WpfBindingErrors.BindingExceptionThrower.Attach();
#endif
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets(GetType().Assembly);
            var config = builder.Build();

            long.TryParse(config["UserId"], out long userId);
            var password = new SecureString();
            Array.ForEach(config["Password"]?.ToCharArray() ?? Array.Empty<char>(), password.AppendChar);
            var credentialsStorage = new CredentialsStorage(new Model.Credential(password, userId));
            var dispatcherService = new DispatcherService(Dispatcher);
            var viewModel = new MainWindowViewModel(credentialsStorage, dispatcherService);
            Application.Current.MainWindow = new MainWindow(viewModel);
            Application.Current.MainWindow.Show();
        }
    }
}
