using ComputerParstDb.ViewModel;
using System;
using System.Windows;

namespace ComputerParstDb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ICloseable
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        // TODO: Make Click event handlers to Command event handlers,
        //       if WPF has 'convinient' MVVM implementation on dialogs
        //       and message box.
       
        private void EditPart_Click(object sender, RoutedEventArgs e)
        {
            ComputerPart part = (ComputerPart)lvComputerParts.SelectedItem;
            if (part == null)
            {
                // return if no selected item
                return;
            }

            try
            {
                PartDialog dialog = new PartDialog(part)
                {
                    Owner = this
                };
                dialog.ShowDialog();

                if (dialog.DialogResult == true)
                {
                    MainViewModel vm = (MainViewModel) DataContext;
                    PartViewModel dialogVm = (PartViewModel) dialog.DataContext;
                    vm.EditPart(part, dialogVm.GetPartDetail());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddPart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // initialize dialog with selected item
                PartDialog dialog = new PartDialog
                {
                    Owner = this
                };
                dialog.ShowDialog();

                if (dialog.DialogResult == true)
                {
                    MainViewModel vm = (MainViewModel)DataContext;
                    PartViewModel dialogVm = (PartViewModel)dialog.DataContext;
                    vm.AddPart(dialogVm.GetPartDetail());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void RemovePart_Click(object sender, RoutedEventArgs e)
        {
            var item = lvComputerParts.SelectedItem;
            if (item == null)
            {
                // return if no selected item
                return;
            }

            try
            {
                MessageBoxResult result = MessageBox.Show(
                    $"Are you sure you want to remove '{((ComputerPart)item).Description}'?",
                    "Remove Part", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    MainViewModel vm = (MainViewModel)DataContext;
                    vm.RemovePart(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
