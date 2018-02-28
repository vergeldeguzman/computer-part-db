using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComputerPartsInventory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Part> boundedParts;

        public MainWindow()
        {
            InitializeComponent();
            FillComputerPartsList();
        }

        private void FillComputerPartsList()
        {
            boundedParts = new ObservableCollection<Part>(PartSummary.Read());
            lvComputerParts.ItemsSource = boundedParts;
        }

        private void MenuExit_Click(object obj, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuPartsAdd_Click(object obj, RoutedEventArgs e)
        {
            try
            {
                PartDialog dialog = new PartDialog();
                dialog.Owner = this;
                dialog.ShowDialog();

                if (dialog.DialogResult == true)
                {
                    // update db
                    PartDetail partDetail = new PartDetail
                    {
                        Description = dialog.Description,
                        PartType = dialog.PartType,
                        Price = dialog.Price,
                        Location = dialog.Location,
                        Condition = dialog.Condition,
                        Remarks = dialog.Remarks
                    };
                    int id = partDetail.Add();

                    // update list view
                    Part part = new Part
                    {
                        Id = id,  // on selected item (ListView), the id is use for edit or delete db query
                        Description = partDetail.Description,
                        Condition = partDetail.Condition
                    };
                    boundedParts.Add(part);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuPartsEdit_Click(object obj, RoutedEventArgs e)
        {
            try
            {
                Part part = (Part)lvComputerParts.SelectedItem;
                if (part == null)
                {
                    // return if no selected item
                    return;
                }

                // read part detail from db
                PartDetail partDetail = new PartDetail();
                partDetail.Read(part.Id);

                // initialize dialog with read part detail
                PartDialog dialog = new PartDialog();
                dialog.Description = partDetail.Description;
                dialog.Condition = partDetail.Condition;
                dialog.PartType = partDetail.PartType;
                dialog.Price = partDetail.Price;
                dialog.Location = partDetail.Location;
                dialog.Remarks = partDetail.Remarks;

                // popup dialog
                dialog.Owner = this;
                dialog.ShowDialog();

                if (dialog.DialogResult == true)
                {
                    // update db
                    partDetail.Description = dialog.Description;
                    partDetail.Condition = dialog.Condition;
                    partDetail.PartType = dialog.PartType;
                    partDetail.Price = dialog.Price;
                    partDetail.Location = dialog.Location;
                    partDetail.Remarks = dialog.Remarks;
                    partDetail.Update();

                    // update list view
                    part.Description = partDetail.Description;
                    part.Condition = partDetail.Condition;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuPartsRemove_Click(object obj, RoutedEventArgs e)
        {
            try
            {
                Part part = (Part)lvComputerParts.SelectedItem;
                if (part == null)
                {
                    // return if no selected item
                    return;
                }

                MessageBoxResult result = MessageBox.Show(
                    $"Are you sure you want to remove '{part.Description}'?",
                    "Remove Part", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    // remove from list view
                    boundedParts.Remove(part);

                    // remove from db
                    part.Remove();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
