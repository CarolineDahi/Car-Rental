using Car.DTO;
using CarRepo;
using Microsoft.AspNetCore.Mvc;
using ProStep.SharedKernel.OperationResult;
using System.ComponentModel.DataAnnotations;

namespace Car_Rental.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CarController : Controller
    {
        private readonly ICarRepository carRepository;

        public CarController(ICarRepository carRepository)
        {
            this.carRepository = carRepository;
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<GetCarDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(FilterDto dto)
            => await carRepository.GetAll(dto).ToJsonResultAsync();

        [HttpGet]
        [ProducesResponseType(typeof(GetCarDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([Required] Guid id)
            => await carRepository.GetById(id).ToJsonResultAsync();

        [HttpPost]
        [ProducesResponseType(typeof(GetCarDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(AddCarDto dto)
           => await carRepository.Add(dto).ToJsonResultAsync();

        [HttpPut]
        [ProducesResponseType(typeof(GetCarDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(UpdateCarDto dto)
           => await carRepository.Update(dto).ToJsonResultAsync();

        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([Required] Guid id)
            => await carRepository.Delete(new List<Guid> { id }).ToJsonResultAsync();

        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteRange([Required] IEnumerable<Guid> ids)
            => await carRepository.Delete(ids).ToJsonResultAsync();
    }
}
