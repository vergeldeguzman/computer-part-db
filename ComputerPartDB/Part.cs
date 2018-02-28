using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ComputerPartsInventory
{
    class Part : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int Id { get; set; }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        private string condition;
        public string Condition
        {
            get { return condition; }
            set
            {
                condition = value;
                OnPropertyChanged("Condition");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public void Remove()
        {
            DbUtils.DeletePart(Id);
        }
    }

    class PartSummary
    {
        public static List<Part> Read()
        {
            return DbUtils.SelectParts();
        }
    }
}
