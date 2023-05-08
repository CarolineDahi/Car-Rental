using Car.DTO;
using _Car = Car.Model.Car;
using Car.SQL.Context;
using Microsoft.EntityFrameworkCore;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace CarRepo
{
    public class CarRepository : ICarRepository
    {
        private readonly CarDBContext context;
        private readonly IMemoryCache cache;

        public CarRepository(CarDBContext context, IMemoryCache cache)
        {
            this.context = context;
            this.cache = cache;
        }

        public async Task<OperationResult<IEnumerable<GetCarDto>>> GetAll(FilterDto dto)
        {
            if (!cache.TryGetValue("cars", out IEnumerable<_Car> carsQuery))
            {
                // If cars not found in cache, get them from database
                carsQuery = await context.Cars.ToListAsync();

                // Cache the cars for 5 minutes
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                };
                cache.Set("cars", carsQuery, cacheOptions);
            }

            carsQuery = carsQuery.Where(c => dto.SearchString != null ?
                                    c.CarNumber.Contains(dto.SearchString)
                                    || c.Color.Contains(dto.SearchString)
                                    || c.Type.ToString().Contains(dto.SearchString)
                                    : true);

            if (dto.SortBy == "type")
            {
                carsQuery = carsQuery.OrderBy(c => c.Type.ToString());
            }
            else if (dto.SortBy == "color")
            {
                carsQuery = carsQuery.OrderBy(c => c.Color);
            }
            else if (dto.SortBy == "dailyfare")
            {
                carsQuery = carsQuery.OrderBy(c => c.DailyFare);
            }
            else if (dto.SortBy == "type_desc")
            {
                carsQuery = carsQuery.OrderByDescending(c => c.Type.ToString());
            }
            else if (dto.SortBy == "color_desc")
            {
                carsQuery = carsQuery.OrderByDescending(c => c.Color);
            }
            else if (dto.SortBy == "dailyfare_desc")
            {
                carsQuery = carsQuery.OrderByDescending(c => c.DailyFare);
            }
            else
            {
                carsQuery = carsQuery.OrderBy(c => c.Id);
            }

            var cars = carsQuery.Skip((dto.PageIndex - 1) * dto.PageSize)
                                .Take(dto.PageSize)
                                .Select(c => new GetCarDto
                                {
                                    Id = c.Id,
                                    CarNumber = c.CarNumber,
                                    Color = c.Color,
                                    DailyFare = c.DailyFare,
                                    EngineCapacity = c.EngineCapacity,
                                    HasDriver = c.HasDriver,
                                    Type = c.Type
                                }).ToList();

            return OperationR.SetSuccess(cars.AsEnumerable());
        }

        public async Task<OperationResult<GetCarDto>> GetById(Guid id)
        {
            var car = await context.Cars
                                   .Where(c => c.Id.Equals(id))
                                   .Select(c => new GetCarDto
                                   {
                                       Id = c.Id,
                                       CarNumber = c.CarNumber,
                                       Color = c.Color,
                                       DailyFare = c.DailyFare,
                                       EngineCapacity = c.EngineCapacity,
                                       HasDriver = c.HasDriver,
                                       Type = c.Type
                                   }).SingleOrDefaultAsync();
            return OperationR.SetSuccess(car);
        }

        public async Task<OperationResult<GetCarDto>> Add(AddCarDto dto)
        {
            var car = new _Car
            {
                CarNumber = dto.CarNumber,
                Color = dto.Color,
                EngineCapacity = dto.EngineCapacity,
                HasDriver = dto.HasDriver,
                Type = dto.Type,
                DailyFare = dto.DailyFare
            };
            context.Add(car);
            await context.SaveChangesAsync();

            return (GetById(car.Id).Result);
        }

        public async Task<OperationResult<GetCarDto>> Update(UpdateCarDto dto)
        {
            var car = await context.Cars.Where(c => c.Id.Equals(dto.Id)).SingleOrDefaultAsync();

            var uniqueNum = await context.Cars
                                         .Where(c => c.CarNumber.Equals(dto.CarNumber) && !c.Id.Equals(dto.Id))
                                         .SingleOrDefaultAsync();

            if(uniqueNum != null)
            {
                return OperationR.SetFailed<GetCarDto>("Can use this Car Number .... already exist");
            }

            car.CarNumber = dto.CarNumber;
            car.Color = dto.Color;
            car.DailyFare = dto.DailyFare;
            car.EngineCapacity = dto.EngineCapacity;
            car.HasDriver = dto.HasDriver;
            car.Type = dto.Type;

            context.Update(car);
            await context.SaveChangesAsync();

            return (GetById(car.Id).Result);
        }

        public async Task<OperationResult<bool>> Delete(IEnumerable<Guid> ids)
        {
            var cars = await context.Cars
                                    .Where(c => ids.Contains(c.Id))
                                    .Include(c => c.RentedCars)
                                    .Include(c => c.DriverCars)
                                    .ToListAsync();
            cars.ForEach(car =>
            {
                context.RemoveRange(car.RentedCars);
                context.RemoveRange(car.DriverCars);
            });
            context.RemoveRange(cars);
            await context.SaveChangesAsync();

            return OperationR.SetSuccess(true);
        }
    }
}
