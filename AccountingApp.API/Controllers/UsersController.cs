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
    public class UsersController : CustomBaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var users = _userService.GetAll();
            var dtos = _mapper.Map<List<UserDto>>(users).OrderBy(x => x.Name).ToList();
            return CreateActionResult(CustomResponseDto<List<UserDto>>.Success(200, dtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<User>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            var userDto = _mapper.Map<UserDto>(user);
            return CreateActionResult(CustomResponseDto<UserDto>.Success(200, userDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<User>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(Guid id)
        {
            Guid userId = Guid.NewGuid();
            var user = await _userService.GetByIdAsync(id);
            user.UpdatedBy = userId;

            _userService.ChangeStatus(user);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserDto userDto)
        {
            Guid userId = Guid.NewGuid();

            var processedEntity = _mapper.Map<User>(userDto);

            processedEntity.CreatedBy = userId;
            processedEntity.UpdatedBy = userId;

            var user = await _userService.AddAsync(processedEntity);

            var userResponseDto = _mapper.Map<UserDto>(user);

            return CreateActionResult(CustomResponseDto<UserDto>.Success(201, userResponseDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<User>))]
        [HttpPut]
        public async Task<IActionResult> Update(UserUpdateDto userDto)
        {
            Guid userId = Guid.NewGuid();

            var currentuser = await _userService.GetByIdAsync(userDto.Id);

            currentuser.UpdatedBy = userId;
            currentuser.Name = userDto.Name;
            currentuser.DepartmentId = userDto.DepartmentId;
            currentuser.GroupId = userDto.GroupId;

            _userService.Update(currentuser);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

    }
}
