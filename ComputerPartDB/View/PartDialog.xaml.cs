using ComputerPartDb.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace ComputerPartDb
{
    /// <summary>
    /// Interaction logic for AddPartDialog.xaml
    /// </summary>
    public partial class PartDialog : Window
    {
        public PartDialog(Part part = null)
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
