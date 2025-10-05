using AccountingApp.API.Filters;
using AccountingApp.Core.DTOs;
using AccountingApp.Core.DTOs.UpdateDtos;
using AccountingApp.Core.Models;
using AccountingApp.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : CustomBaseController
    {
        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;
        public SalesController(ISaleService saleService, IMapper mapper)
        {
            _saleService = saleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var sales = _saleService.GetAll();
            var dtos = _mapper.Map<List<SaleDto>>(sales).ToList();
            return CreateActionResult(CustomResponseDto<List<SaleDto>>.Success(200, dtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Sale>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var sale = await _saleService.GetByIdAsync(id);
            var saleDto = _mapper.Map<SaleDto>(sale);
            return CreateActionResult(CustomResponseDto<SaleDto>.Success(200, saleDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Sale>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(Guid id)
        {
            Guid userId = Guid.NewGuid();
            var sale = await _saleService.GetByIdAsync(id);
            sale.UpdatedBy = userId;

            _saleService.ChangeStatus(sale);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(SaleDto saleDto)
        {
            Guid userId = Guid.NewGuid();

            var processedEntity = _mapper.Map<Sale>(saleDto);

            processedEntity.CreatedBy = userId;
            processedEntity.UpdatedBy = userId;

            var sale = await _saleService.AddAsync(processedEntity);

            var saleResponseDto = _mapper.Map<SaleDto>(sale);

            return CreateActionResult(CustomResponseDto<SaleDto>.Success(201, saleResponseDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Sale>))]
        [HttpPut]
        public async Task<IActionResult> Update(SaleUpdateDto saleDto)
        {
            Guid userId = Guid.NewGuid();

            var currentsale = await _saleService.GetByIdAsync(saleDto.Id);

            currentsale.UpdatedBy = userId;
            currentsale.Quantity = saleDto.Quantity;
            currentsale.UnitPrice = saleDto.UnitPrice;
            currentsale.TotalPrice = saleDto.TotalPrice;
            currentsale.CustomerId = saleDto.CustomerId;
            currentsale.ProductId = saleDto.ProductId;

            _saleService.Update(currentsale);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

    }
}
