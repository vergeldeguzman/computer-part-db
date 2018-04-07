using ComputerParstDb;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerPartsInventory.Model
{
    public static class DTOConverter
    {
        public static ComputerPart ToComputerPart(ComputerPartDTO dto)
        {
            return new ComputerPart
            {
                Id = dto.Id,
                Description = dto.Description,
                Condition = dto.Condition
            };
        }

        public static ComputerPartDetail ToCompuerPartDetail(ComputerPartDetailDTO dto)
        {
            return new ComputerPartDetail
            {
                Id = dto.Id,
                Description = dto.Description,
                Condition = dto.Condition,
                Location = dto.Location,
                PartType = dto.PartType,
                Price = dto.Price,
                Remarks = dto.Remarks
            };
        }

        public static ComputerPartDetailDTO FromComputerPartDetail(ComputerPartDetail detail)
        {
            return new ComputerPartDetailDTO
            {
                Id = detail.Id,
                Description = detail.Description,
                Condition = detail.Condition,
                Location = detail.Location,
                PartType = detail.PartType,
                Price = detail.Price,
                Remarks = detail.Remarks
            };
        }
    }
}
