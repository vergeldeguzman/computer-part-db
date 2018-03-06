using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace ComputerPartDb.ViewModel
{
    class MainViewModel
    {
        public ObservableCollection<Part> BoundedParts { get; private set; }
        public DelegateCommand<ICloseable> ExitAppCommand { get; private set; }

        public MainViewModel()
        {
            this.BoundedParts = new ObservableCollection<Part>(DbUtils.SelectParts());

            this.ExitAppCommand = new DelegateCommand<ICloseable>(this.CloseWindow, null);
        }

        private void CloseWindow(ICloseable window)
        {
            if (window != null)
            {
                window.Close();
            }
        }

        public void AddPart(PartDetail partDetail)
        {
            // update db
            int id = DbUtils.InsertPartDetail(
                partDetail.Description,
                partDetail.Condition,
                partDetail.PartType,
                partDetail.Location,
                partDetail.Price,
                partDetail.Remarks);

            // update list view
            Part part = new Part
            {
                Id = id,  // on selected item (ListView), the id is use for edit or delete db query
                Description = partDetail.Description,
                Condition = partDetail.Condition
            };
            BoundedParts.Add(part);
        }

        public void EditPart(Part part, PartDetail partDetail)
        {
            // update db
            DbUtils.UpdatePart(
                part.Id,
                partDetail.Description,
                partDetail.Condition,
                partDetail.PartType,
                partDetail.Location,
                partDetail.Price,
                partDetail.Remarks);

            // update list view, boundedParts are updated automatically
            part.Description = partDetail.Description;
            part.Condition = partDetail.Condition;
        }

        public void RemovePart(Object obj)
        {
            Part part = (Part)obj;

            // remove from list view
            BoundedParts.Remove(part);

            // remove from db
            DbUtils.DeletePart(part.Id);
        }
    }
}
