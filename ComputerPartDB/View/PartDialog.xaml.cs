using ComputerParstDb.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace ComputerParstDb
{
    /// <summary>
    /// Interaction logic for AddPartDialog.xaml
    /// </summary>
    public partial class PartDialog : Window
    {
        public PartDialog(ComputerPart part = null)
        {
            InitializeComponent();
            PartViewModel partViewModel = new PartViewModel();
            if (part != null)
            {
                partViewModel.InitializeFields(part.Id);
            }
            DataContext = partViewModel;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(txtDescription) || Validation.GetHasError(txtPrice))
            {
                DialogResult = null;
                return;
            }

            DialogResult = true;
        }
    }
}
