using AccountingApp.API.Filters;
using AccountingApp.Core.DTOs;
using AccountingApp.Core.DTOs.UpdateDtos;
using AccountingApp.Core.Models;
using AccountingApp.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.ComponentModel;

namespace AccountingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : CustomBaseController
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentsController(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var departments = _departmentService.GetAll();
            var dtos = _mapper.Map<List<DepartmentDto>>(departments);
            return CreateActionResult(CustomResponseDto<List<DepartmentDto>>.Success(200, dtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Department>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var department=await _departmentService.GetByIdAsync(id);
            var dto = _mapper.Map<DepartmentDto>(department);

            return CreateActionResult(CustomResponseDto<DepartmentDto>.Success(200, dto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Department>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(Guid id)
        {
            Guid userId = Guid.NewGuid();

            var department= await _departmentService.GetByIdAsync(id);

            department.UpdatedBy = userId;

            _departmentService.ChangeStatus(department);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(DepartmentDto departmentDto)
        {
            Guid userId = Guid.NewGuid();

            var processedEntity = _mapper.Map<Department>(departmentDto);
            processedEntity.UpdatedBy = userId;
            processedEntity.CreatedBy = userId;

            var department = await _departmentService.AddAsync(processedEntity);

            var departmentResponseDto = _mapper.Map<DepartmentDto>(department);

            return CreateActionResult(CustomResponseDto<DepartmentDto>.Success(201, departmentResponseDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Customer>))]
        [HttpPut]
        public async Task<IActionResult> Update(DepartmentUpdateDto departmentDto)
        {
            Guid userId = Guid.NewGuid();

            var currenDepartment = await _departmentService.GetByIdAsync(departmentDto.Id);

            currenDepartment.UpdatedBy = userId;
            currenDepartment.Name = departmentDto.Name;

            _departmentService.Update(currenDepartment);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

    }
}
