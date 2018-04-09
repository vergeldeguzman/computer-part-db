using Services.DTO;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Providers
{
    public class ComputerPartsService : IComputerPartsService
    {
        public int Append(ComputerPartDetailDTO computerPart)
        {
            int id = -1;
            using (var db = new ComputerPartsContext())
            {
                ComputerParts computerParts = new ComputerParts
                {
                    Description = computerPart.Description,
                    Condition = computerPart.Condition,
                    PartType = computerPart.PartType,
                    Location = computerPart.Location,
                    Price = computerPart.Price,
                    Remarks = computerPart.Remarks
                };
                db.ComputerParts.Add(computerParts);
                db.SaveChanges();
                id = computerParts.Id;
            }
            return id;
        }

        public void Delete(int id)
        {
            using (var db = new ComputerPartsContext())
            {
                var computerParts = new ComputerParts { Id = id };
                db.ComputerParts.Attach(computerParts);
                db.ComputerParts.Remove(computerParts);
                db.SaveChanges();
            }
        }

        public List<ComputerPartDTO> GetComputerParts(string partType = null, string condition = null)
        {
            using (var db = new ComputerPartsContext())
            {
                var parts = db.ComputerParts
                    .Where(c =>
                        (String.IsNullOrEmpty(partType) || c.PartType == partType) &&
                        (String.IsNullOrEmpty(condition) || c.Condition == condition))
                    .Select(c => new ComputerPartDTO
                    {
                        Id = c.Id,
                        Description = c.Description,
                        Condition = c.Condition,
                        PartType = c.PartType
                    });
                return parts.ToList();
            }
        }

        public ComputerPartDetailDTO GetDetailsById(int id)
        {
            using (var db = new ComputerPartsContext())
            {
                var part = db.ComputerParts
                    .Where(c => c.Id == id)
                    .SingleOrDefault();
                if (part != null)
                {
                    return new ComputerPartDetailDTO
                    {
                        Description = part.Description,
                        Condition = part.Condition,
                        PartType = part.PartType,
                        Location = part.Location,
                        Price = part.Price,
                        Remarks = part.Remarks
                    };
                }
            }

            return null;
        }

        public void Update(int id, ComputerPartDetailDTO detail)
        {
            using (var db = new ComputerPartsContext())
            {
                var partToEdit = db.ComputerParts.Where(a => a.Id == id).SingleOrDefault();
                if (partToEdit != null)
                {
                    partToEdit.Description = detail.Description;
                    partToEdit.Condition = detail.Condition;
                    partToEdit.PartType = detail.PartType;
                    partToEdit.Location = detail.Location;
                    partToEdit.Price = detail.Price;
                    partToEdit.Remarks = detail.Remarks;
                    db.SaveChanges();
                }
            }
        }
    }
}
