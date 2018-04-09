using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ComputerParstDb
{
    public class ComputerPart : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                RaisePropertyChanged();
            }
        }

        private string condition;
        public string Condition
        {
            get { return condition; }
            set
            {
                condition = value;
                RaisePropertyChanged();
            }
        }

        private string partType;
        public string PartType
        {
            get { return partType; }
            set
            {
                partType = value;
                RaisePropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertName));
        }
    }
}
