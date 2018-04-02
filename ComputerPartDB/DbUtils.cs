using System;
using System.Collections.Generic;
using System.Linq;

namespace ComputerPartDb
{
    class DbUtils
    {
        public static List<Part> SelectParts()
        {
            using (var db = new ComputerPartContext())
            {
                var parts = db.ComputerParts
                    .Select(c => new Part
                    {
                        Id = c.Id,
                        Description = c.Description,
                        Condition = c.Condition
                    });
                return parts.ToList();
            }
        }

        public static PartDetail SelectPartDetail(int id)
        {
            using (var db = new ComputerPartContext())
            {
                var part = db.ComputerParts
                    .Where(c => c.Id == id)
                    .SingleOrDefault();
                if (part != null)
                {
                    return new PartDetail
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

        public static int InsertPartDetail(string description, string condition, string partType,
            string location, decimal? price, string remarks)
        {
            // checks on non-null db fields
            if (String.IsNullOrEmpty(description))
            {
                throw new ArgumentException("Parameter cannot be null or empty", "description");
            }
            if (String.IsNullOrEmpty(condition))
            {
                throw new ArgumentException("Parameter cannot be null or empty", "condition");
            }
            if (String.IsNullOrEmpty(partType))
            {
                throw new ArgumentException("Parameter cannot be null or empty", "partType");
            }

            int id = -1;
            using (var db = new ComputerPartContext())
            {
                ComputerPart computerPart = new ComputerPart
                {
                    Description = description,
                    Condition = condition,
                    PartType = partType,
                    Location = location,
                    Price = price,
                    Remarks = remarks
                };
                db.ComputerParts.Add(computerPart);
                db.SaveChanges();
                id = computerPart.Id;
            }
            return id;
        }

        public static void UpdatePart(int id, string description, string condition,
            string partType, string location, decimal? price, string remarks)
        {
            // checks on non-null db fields
            if (String.IsNullOrEmpty(description))
            {
                throw new ArgumentException("Parameter cannot be null or empty", "description");
            }
            if (String.IsNullOrEmpty(condition))
            {
                throw new ArgumentException("Parameter cannot be null or empty", "condition");
            }
            if (String.IsNullOrEmpty(partType))
            {
                throw new ArgumentException("Parameter cannot be null or empty", "partType");
            }

            using (var db = new ComputerPartContext())
            {
                var partToEdit = db.ComputerParts.Where(a => a.Id == id).SingleOrDefault();
                if (partToEdit != null)
                {
                    partToEdit.Description = description;
                    partToEdit.Condition = condition;
                    partToEdit.PartType = partType;
                    partToEdit.Location = location;
                    partToEdit.Price = price;
                    partToEdit.Remarks = remarks;
                    db.SaveChanges();
                }
            }
        }

        public static void DeletePart(int id)
        {
            using (var db = new ComputerPartContext())
            {
                var computerPart = new ComputerPart { Id = 1 };
                db.ComputerParts.Attach(computerPart);
                db.ComputerParts.Remove(computerPart);
                db.SaveChanges();
            }
        }
    }
}
