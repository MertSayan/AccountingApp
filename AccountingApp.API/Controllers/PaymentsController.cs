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
    public class PaymentsController : CustomBaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        public PaymentsController(IPaymentService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var payments = _paymentService.GetAll();
            var dtos = _mapper.Map<List<PaymentDto>>(payments).ToList();
            return CreateActionResult(CustomResponseDto<List<PaymentDto>>.Success(200, dtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Payment>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var payment = await _paymentService.GetByIdAsync(id);
            var paymentDto = _mapper.Map<PaymentDto>(payment);
            return CreateActionResult(CustomResponseDto<PaymentDto>.Success(200, paymentDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Payment>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(Guid id)
        {
            Guid userId = Guid.NewGuid();
            var payment = await _paymentService.GetByIdAsync(id);
            payment.UpdatedBy = userId;

            _paymentService.ChangeStatus(payment);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(PaymentDto paymentDto)
        {
            Guid userId = Guid.NewGuid();

            var processedEntity = _mapper.Map<Payment>(paymentDto);

            processedEntity.CreatedBy = userId;
            processedEntity.UpdatedBy = userId;

            var payment = await _paymentService.AddAsync(processedEntity);

            var paymentResponseDto = _mapper.Map<PaymentDto>(payment);

            return CreateActionResult(CustomResponseDto<PaymentDto>.Success(201, paymentResponseDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Payment>))]
        [HttpPut]
        public async Task<IActionResult> Update(PaymentUpdateDto paymentDto)
        {
            Guid userId = Guid.NewGuid();

            var currentpayment = await _paymentService.GetByIdAsync(paymentDto.Id);

            currentpayment.UpdatedBy = userId;
            currentpayment.CustomerId = paymentDto.CustomerId;
            currentpayment.Amount = paymentDto.Amount;

            _paymentService.Update(currentpayment);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }


    }
}
