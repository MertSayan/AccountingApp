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
    public class CustomersController : CustomBaseController
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        public CustomersController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet] 
        public async Task<IActionResult> All()
        {
            var customers =  _customerService.GetAll();
            var dtos = _mapper.Map<List<CustomerDto>>(customers).OrderBy(x=>x.Name).ToList();
            return CreateActionResult(CustomResponseDto<List<CustomerDto>>.Success(200, dtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Customer>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            var customerDto = _mapper.Map<CustomerDto>(customer);
            return CreateActionResult(CustomResponseDto<CustomerDto>.Success(200, customerDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Customer>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(Guid id)
        {
            Guid userId= Guid.NewGuid();
            var customer = await _customerService.GetByIdAsync(id);
            customer.UpdatedBy = userId;

            _customerService.ChangeStatus(customer);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CustomerDto customerDto)
        {
            Guid userId = Guid.NewGuid();

            var processedEntity= _mapper.Map<Customer>(customerDto);

            processedEntity.CreatedBy=userId;
            processedEntity.UpdatedBy=userId;

            var customer = await _customerService.AddAsync(processedEntity);

            var customerResponseDto = _mapper.Map<CustomerDto>(customer);

            return CreateActionResult(CustomResponseDto<CustomerDto>.Success(201, customerDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Customer>))]
        [HttpPut]
        public async Task<IActionResult> Update(CustomerUpdateDto customerDto)
        {
            Guid userId = Guid.NewGuid();

            var currentCustomer = await _customerService.GetByIdAsync(customerDto.Id);

            currentCustomer.UpdatedBy=userId;
            currentCustomer.Name = customerDto.Name;

            _customerService.Update(currentCustomer);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
