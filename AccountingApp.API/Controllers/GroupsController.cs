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
    public class GroupsController : CustomBaseController
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public GroupsController(IGroupService groupService, IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var groups= _groupService.GetAll();
            var dtos = _mapper.Map<List<GroupDto>>(groups).ToList();
            return CreateActionResult(CustomResponseDto<List<GroupDto>>.Success(200, dtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Group>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var group = await _groupService.GetByIdAsync(id);
            var dto = _mapper.Map<GroupDto>(group);
            return CreateActionResult(CustomResponseDto<GroupDto>.Success(200, dto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Group>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(Guid id)
        {
            Guid userId = Guid.NewGuid();
            var group=await _groupService.GetByIdAsync(userId);

            _groupService.ChangeStatus(group);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
        }

        [HttpPost]
        public async Task<IActionResult> Save(GroupDto groupDto)
        {
            Guid userId = Guid.NewGuid();
            var processedEntity = _mapper.Map<Group>(groupDto);

            processedEntity.CreatedBy=userId;
            processedEntity.UpdatedBy=userId;

            var group = _groupService.AddAsync(processedEntity);

            var groupResponseDto = _mapper.Map<GroupDto>(group);

            return CreateActionResult(CustomResponseDto<GroupDto>.Success(201, groupResponseDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Group>))]
        [HttpPut]
        public async Task<IActionResult> Update(GroupUpdateDto groupDto)
        {
            Guid userId = Guid.NewGuid();
            var currentGroup = await _groupService.GetByIdAsync(groupDto.Id);

            currentGroup.UpdatedBy=userId;
            currentGroup.Name=groupDto.Name;

            _groupService.Update(currentGroup);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }
    }
}
