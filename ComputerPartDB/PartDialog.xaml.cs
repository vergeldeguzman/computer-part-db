using System;
using System.Windows;

namespace ComputerPartsInventory
{
    /// <summary>
    /// Interaction logic for AddPartDialog.xaml
    /// </summary>
    public partial class PartDialog : Window
    {
        public PartDialog()
        {
            InitializeComponent();
        }

        public string Description
        {
            get { return txtDescription.Text; }
            set { txtDescription.Text = value; }
        }

        public string PartType
        {
            get { return cmbType.Text; }
            set
            {
                 cmbType.Text = value;
            }
        }

        public decimal Price
        {
            get {
                decimal priceInDecimal;
                if (!Decimal.TryParse(txtPrice.Text, out priceInDecimal))
                {
                    // Price is optional so set 0 for unparseable price
                    priceInDecimal = 0;
                }
                return priceInDecimal;
            }
            set { txtPrice.Text = value.ToString(); }
        }

        public string Location
        {
            get { return txtLocation.Text; }
            set { txtLocation.Text = value; }
        }

        public string Condition
        {
            get { return cmbCondition.Text; }
            set { cmbCondition.Text = value; }
        }

        public string Remarks
        {
            get { return txtRemarks.Text; }
            set { txtRemarks.Text = value; }
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Add validation on inputs
            this.DialogResult = true;
        }
    }
}
