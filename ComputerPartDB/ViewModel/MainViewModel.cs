using ComputerPartsInventory.Model;
using Services.Interfaces;
using System;
using System.Collections.ObjectModel;

namespace ComputerParstDb.ViewModel
{
    public class MainViewModel
    {
        private IComputerPartsService _computerPartsService;
        public ObservableCollection<ComputerPart> BoundedParts { get; private set; }
        public DelegateCommand<ICloseable> ExitAppCommand { get; private set; }

        public string PartType { get; set; } = "All";

        public string Condition { get; set; } = "Good";

        public MainViewModel(IComputerPartsService computerPartsService)
        {
            this._computerPartsService = computerPartsService;

            this.BoundedParts = new ObservableCollection<ComputerPart>();
            this.ReloadBoundedParts();

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
            int id = this._computerPartsService.Append(DTOConverter.FromComputerPartDetail(partDetail));

            // update list view if within Condition/Type filters
            if ((Condition == "All" || Condition == partDetail.Condition) &&
                (PartType == "All" || PartType == partDetail.PartType))
            {
                ComputerPart part = new ComputerPart
                {
                    Id = id,  // on selected item (ListView), the id is use for edit or delete db query
                    Description = partDetail.Description,
                    Condition = partDetail.Condition,
                    PartType = partDetail.PartType
                };
                this.BoundedParts.Add(part);
            }
        }

        public void EditPart(ComputerPart part, ComputerPartDetail partDetail)
        {
            // update db
            this._computerPartsService.Update(part.Id, DTOConverter.FromComputerPartDetail(partDetail));

            // update list view if within Condition/Type filters
            if ((Condition == "All" || Condition == partDetail.Condition) &&
                (PartType == "All" || PartType == partDetail.PartType))
            {
                // update list view, boundedParts are updated automatically
                part.Description = partDetail.Description;
                part.Condition = partDetail.Condition;
                part.PartType = partDetail.PartType;
            }
            else
            {
                // otherwise, remove from list view
                this.BoundedParts.Remove(part);
            }
        }

        public void RemovePart(Object obj)
        {
            ComputerPart part = (ComputerPart)obj;

            // remove from list view
            this.BoundedParts.Remove(part);

            // remove from db
            this._computerPartsService.Delete(part.Id);
        }

        public void ReloadBoundedParts()
        {
            this.BoundedParts.Clear();
            foreach (var dto in _computerPartsService.GetComputerParts(
                PartType != "All" ? PartType : "",
                Condition != "All" ? Condition : ""))
            {
                this.BoundedParts.Add(DTOConverter.ToComputerPart(dto));
            }
        }
    }
}
