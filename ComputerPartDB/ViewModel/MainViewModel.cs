using ComputerPartsInventory.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Services.DTO;
using Services.Interfaces;
using Services.Providers;

namespace ComputerParstDb.ViewModel
{
    class MainViewModel
    {
        private IComputerPartsService computerPartsService;
        public ObservableCollection<ComputerPart> BoundedParts { get; private set; }
        public DelegateCommand<ICloseable> ExitAppCommand { get; private set; }

        public MainViewModel()
        {
            this.computerPartsService = new ComputerPartsService();

            List<ComputerPart> parts = new List<ComputerPart>();
            foreach (var dto in computerPartsService.GetAll())
            {
                parts.Add(DTOConverter.ToComputerPart(dto));
            }
            this.BoundedParts = new ObservableCollection<ComputerPart>(parts);

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
                Condition = partDetail.Condition
            };
            BoundedParts.Add(part);
        }

        public void EditPart(ComputerPart part, ComputerPartDetail partDetail)
        {
            // update db
            this.computerPartsService.Update(part.Id, DTOConverter.FromComputerPartDetail(partDetail));

            // update list view, boundedParts are updated automatically
            part.Description = partDetail.Description;
            part.Condition = partDetail.Condition;
        }

        public void RemovePart(Object obj)
        {
            ComputerPart part = (ComputerPart)obj;

            // remove from list view
            BoundedParts.Remove(part);

            // remove from db
            this.computerPartsService.Delete(part.Id);
        }
    }
}
