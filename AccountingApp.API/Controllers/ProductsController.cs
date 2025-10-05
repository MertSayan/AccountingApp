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
    public class ProductsController : CustomBaseController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = _productService.GetAll();
            var dtos = _mapper.Map<List<ProductDto>>(products).OrderBy(x => x.Name).ToList();
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, dtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(Guid id)
        {
            Guid userId = Guid.NewGuid();
            var product = await _productService.GetByIdAsync(id);
            product.UpdatedBy = userId;

            _productService.ChangeStatus(product);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            Guid userId = Guid.NewGuid();

            var processedEntity = _mapper.Map<Product>(productDto);

            processedEntity.CreatedBy = userId;
            processedEntity.UpdatedBy = userId;

            var product = await _productService.AddAsync(processedEntity);

            var productResponseDto = _mapper.Map<ProductDto>(product);

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productResponseDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
            Guid userId = Guid.NewGuid();

            var currentproduct = await _productService.GetByIdAsync(productDto.Id);

            currentproduct.UpdatedBy = userId;
            currentproduct.Name = productDto.Name;
            currentproduct.UnitPrice = productDto.UnitPrice;

            _productService.Update(currentproduct);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

    }
}
