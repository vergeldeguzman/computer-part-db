using ComputerPartsInventory.Model;
using Services.Interfaces;
using Services.Providers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ComputerParstDb.ViewModel
{
    class MainViewModel
    {
        private IComputerPartsService computerPartsService;
        public ObservableCollection<ComputerPart> BoundedParts { get; private set; }
        public DelegateCommand<ICloseable> ExitAppCommand { get; private set; }

        public string PartType { get; set; } = "All";

        public string Condition { get; set; } = "Good";

        public MainViewModel()
        {
            this.computerPartsService = new ComputerPartsService();

            this.BoundedParts = new ObservableCollection<ComputerPart>();
            this.FilterBoundedParts();

            this.ExitAppCommand = new DelegateCommand<ICloseable>(this.CloseWindow, null);
        }

        private void CloseWindow(ICloseable window)
        {
            if (window != null)
            {
                window.Close();
            }
        }

        public void AddPart(ComputerPartDetail partDetail)
        {
            // update db
            int id = this.computerPartsService.Append(DTOConverter.FromComputerPartDetail(partDetail));

            // update list view
            ComputerPart part = new ComputerPart
            {
                Id = id,  // on selected item (ListView), the id is use for edit or delete db query
                Description = partDetail.Description,
                Condition = partDetail.Condition,
                PartType = partDetail.PartType
            };
            this.BoundedParts.Add(part);

            this.FilterBoundedParts();
        }

        public void EditPart(ComputerPart part, ComputerPartDetail partDetail)
        {
            // update db
            this.computerPartsService.Update(part.Id, DTOConverter.FromComputerPartDetail(partDetail));

            // update list view, boundedParts are updated automatically
            part.Description = partDetail.Description;
            part.Condition = partDetail.Condition;
            part.PartType = partDetail.PartType;

            this.FilterBoundedParts();
        }

        public void RemovePart(Object obj)
        {
            ComputerPart part = (ComputerPart)obj;

            // remove from list view
            this.BoundedParts.Remove(part);

            // remove from db
            this.computerPartsService.Delete(part.Id);
        }

        public void FilterBoundedParts()
        {
            this.BoundedParts.Clear();
            foreach (var dto in computerPartsService.GetComputerParts(
                PartType != "All" ? PartType : "",
                Condition != "All" ? Condition : ""))
            {
                this.BoundedParts.Add(DTOConverter.ToComputerPart(dto));
            }
        }
    }
}
