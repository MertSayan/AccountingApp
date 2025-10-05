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
    public class RolesController : CustomBaseController
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        public RolesController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var roles = _roleService.GetAll();
            var dtos = _mapper.Map<List<RoleDto>>(roles).OrderBy(x => x.Name).ToList();
            return CreateActionResult(CustomResponseDto<List<RoleDto>>.Success(200, dtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Role>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var role = await _roleService.GetByIdAsync(id);
            var roleDto = _mapper.Map<RoleDto>(role);
            return CreateActionResult(CustomResponseDto<RoleDto>.Success(200, roleDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Role>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(Guid id)
        {
            Guid userId = Guid.NewGuid();
            var role = await _roleService.GetByIdAsync(id);
            role.UpdatedBy = userId;

            _roleService.ChangeStatus(role);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(RoleDto roleDto)
        {
            Guid userId = Guid.NewGuid();

            var processedEntity = _mapper.Map<Role>(roleDto);

            processedEntity.CreatedBy = userId;
            processedEntity.UpdatedBy = userId;

            var role = await _roleService.AddAsync(processedEntity);

            var roleResponseDto = _mapper.Map<RoleDto>(role);

            return CreateActionResult(CustomResponseDto<RoleDto>.Success(201, roleResponseDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Role>))]
        [HttpPut]
        public async Task<IActionResult> Update(RoleUpdateDto roleDto)
        {
            Guid userId = Guid.NewGuid();

            var currentrole = await _roleService.GetByIdAsync(roleDto.Id);

            currentrole.UpdatedBy = userId;
            currentrole.Name = roleDto.Name;

            _roleService.Update(currentrole);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

    }
}
