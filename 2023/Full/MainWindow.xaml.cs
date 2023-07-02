using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using Full.ViewModel;

namespace Full
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // TODO: Show logging in SnoopUI
            // CompositionTarget.Rendering += HandleRednering;
        }

        private void HandleRednering(object sender, System.EventArgs e) =>
            Trace.WriteLine("LoadData");

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
