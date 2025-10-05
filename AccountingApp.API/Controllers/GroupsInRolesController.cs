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
    public class GroupsInRolesController : CustomBaseController
    {
        private readonly IGroupInRoleService _GroupInRoleService;
        private readonly IMapper _mapper;
        public GroupsInRolesController(IGroupInRoleService GroupInRoleService, IMapper mapper)
        {
            _GroupInRoleService = GroupInRoleService;
            _mapper = mapper;
        }

        [HttpGet] 
        public async Task<IActionResult> All()
        {
            var GroupInRoles =  _GroupInRoleService.GetAll();
            var dtos = _mapper.Map<List<GroupInRoleDto>>(GroupInRoles).ToList();
            return CreateActionResult(CustomResponseDto<List<GroupInRoleDto>>.Success(200, dtos));
        }   

        [ServiceFilter(typeof(NotFoundFilter<GroupInRole>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var GroupInRole = await _GroupInRoleService.GetByIdAsync(id);
            var GroupInRoleDto = _mapper.Map<GroupInRoleDto>(GroupInRole);
            return CreateActionResult(CustomResponseDto<GroupInRoleDto>.Success(200, GroupInRoleDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<GroupInRole>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(Guid id)
        {
            Guid userId= Guid.NewGuid();
            var GroupInRole = await _GroupInRoleService.GetByIdAsync(id);
            GroupInRole.UpdatedBy = userId;

            _GroupInRoleService.ChangeStatus(GroupInRole);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(GroupInRoleDto GroupInRoleDto)
        {
            Guid userId = Guid.NewGuid();

            var processedEntity= _mapper.Map<GroupInRole>(GroupInRoleDto);

            processedEntity.CreatedBy=userId;
            processedEntity.UpdatedBy=userId;

            var GroupInRole = await _GroupInRoleService.AddAsync(processedEntity);

            var GroupInRoleResponseDto = _mapper.Map<GroupInRoleDto>(GroupInRole);

            return CreateActionResult(CustomResponseDto<GroupInRoleDto>.Success(201, GroupInRoleResponseDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<GroupInRole>))]
        [HttpPut]
        public async Task<IActionResult> Update(GroupInRoleUpdateDto GroupInRoleDto)
        {
            Guid userId = Guid.NewGuid();

            var currentGroupInRole = await _GroupInRoleService.GetByIdAsync(GroupInRoleDto.Id);

            currentGroupInRole.UpdatedBy=userId;
            currentGroupInRole.GroupId = GroupInRoleDto.GroupId;
            currentGroupInRole.RoleId = GroupInRoleDto.RoleId;

            _GroupInRoleService.Update(currentGroupInRole);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

    }
}
