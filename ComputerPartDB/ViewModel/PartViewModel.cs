using Services.DTO;
using Services.Interfaces;
using Services.Providers;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ComputerParstDb.ViewModel
{
    public class PartViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public string Error => throw new NotImplementedException();
        public string this[string columnName]
        {
            get
            {
                string result = "";
                switch (columnName)
                {
                    case "Description" :
                        if (String.IsNullOrEmpty(Description))
                        {
                            result = "Description is a mandatory field";
                        }
                        break;
                    case "PartType":
                        if (String.IsNullOrEmpty(PartType))
                        {
                            result = "Type is a mandatory field";
                        }
                        break;
                    case "Condition":
                        if (String.IsNullOrEmpty(Condition))
                        {
                            result = "Condition is a mandatory field";
                        }
                        break;
                    default:
                        break;
                }
                return result;
            }
        }

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

        private string partType = "Other";
        public string PartType
        {
            get { return partType; }
            set
            {
                partType = value;
                RaisePropertyChanged();
            }
        }

        // Text bound to decimal has implicit validation
        private decimal? price;
        public decimal? Price
        {
            get { return price; }
            set
            {
                price = value;
                RaisePropertyChanged();
            }
        }

        private string location;
        public string Location
        {
            get { return location; }
            set
            {
                location = value;
                RaisePropertyChanged();
            }
        }

        private string condition = "Unknown";
        public string Condition
        {
            get { return condition; }
            set
            {
                condition = value;
                RaisePropertyChanged();
            }
        }

        private string remarks;
        public string Remarks
        {
            get { return remarks; }
            set
            {
                remarks = value;
                RaisePropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertName));
        }

        public void InitializeFields(int id)
        {
            IComputerPartsService computerPartsService = new ComputerPartsService();

            // read part detail from db
            ComputerPartDetailDTO partDetail = computerPartsService.GetDetailsById(id);

            // update dialog
            Description = partDetail.Description;
            Condition = partDetail.Condition;
            PartType = partDetail.PartType;
            Price = partDetail.Price;
            Location = partDetail.Location;
            Remarks = partDetail.Remarks;
        }

        public ComputerPartDetail GetPartDetail()
        {
            return new ComputerPartDetail
            {
                Description = this.Description,
                PartType = this.PartType,
                Price = this.Price,
                Location = this.Location,
                Condition = this.Condition,
                Remarks = this.Remarks
            };
        }
    }
}
