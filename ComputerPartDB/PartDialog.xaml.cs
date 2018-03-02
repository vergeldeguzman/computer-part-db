using System.Windows;
using System.Windows.Controls;

namespace ComputerPartDb
{
    /// <summary>
    /// Interaction logic for AddPartDialog.xaml
    /// </summary>
    public partial class PartDialog : Window
    {
        public PartDialog()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string Description { get; set; }

        public string PartType { get; set; } = "Other";

        // implicit validation occurs because of user text to decimal conversion
        public decimal Price { get; set; }

        public string Location { get; set; }

        public string Condition { get; set; } = "Unknown";

        public string Remarks { get; set; }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(txtDescription) || Validation.GetHasError(txtPrice))
            {
                DialogResult = null;
                return;
            }

            DialogResult = true;
        }
    }
    public class RequiredRule : ValidationRule
    {
        public string Name { get; set; }

        public int GridRow { get; set; }

        public int GridColumn { get; set; }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {            
            string str = value as string;

            if (!string.IsNullOrEmpty(str))
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                if (Name.Length == 0)
                {
                    Name = "Field";
                }

                return new ValidationResult(false, Name + " is required");
            }
        }
    }
}
