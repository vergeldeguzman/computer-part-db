using Services.DTO;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface IComputerPartsService
    {
        List<ComputerPartDTO> GetComputerParts(string partType, string condition);
        ComputerPartDetailDTO GetDetailsById(int id);
        int Append(ComputerPartDetailDTO computerPart);
        void Update(int id, ComputerPartDetailDTO computerPartDetail);
        void Delete(int id);
    }
}
