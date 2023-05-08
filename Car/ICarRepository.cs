using Car.DTO;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepo
{
    public interface ICarRepository
    {
        Task<OperationResult<IEnumerable<GetCarDto>>> GetAll(FilterDto dto);
        Task<OperationResult<GetCarDto>> GetById(Guid id);
        Task<OperationResult<GetCarDto>> Add(AddCarDto dto);
        Task<OperationResult<GetCarDto>> Update(UpdateCarDto dto);
        Task<OperationResult<bool>> Delete(IEnumerable<Guid> ids);
    }
}
