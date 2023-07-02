using System.Windows;
using Core.ViewModel;

namespace Core
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(MainWindowViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }

        public MainWindowViewModel? ViewModel
        {
            get
            {
                return DataContext as MainWindowViewModel;
            }
        }
    }
}
