using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using ServerING.DTO;
using ServerING.Exceptions;
using ServerING.ModelConverters;
using ServerING.Models;
using ServerING.ModelsBL;
using ServerING.Services;
using ServerING.Enums;
using Microsoft.AspNetCore.Cors;

namespace ServerING.Controllers {
    [EnableCors("MyPolicy")]
    [ApiController]   
    [Route("/api/v1/users")]
    public class UserController : Controller {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly UserConverters userConverters;

        public UserController(IUserService userService,
            IMapper mapper, UserConverters userConverters) {
            this.userService = userService;
            this.mapper = mapper;
            this.userConverters = userConverters;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        public IActionResult GetAll() {
            return Ok(mapper.Map<IEnumerable<UserDto>>(userService.GetAllUsers()));
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserIdPasswordDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Add(UserPasswordDto user) {
            try {
                var addedUser = userService
                    .AddUser(mapper.Map<UserBL>(user));
                return Ok(mapper.Map<UserIdPasswordDto>(addedUser));
            }
            catch (UserAlreadyExistsException ex) {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id) {
            var user = mapper.Map<UserDto>(userService.GetUserByID(id));
            return user != null ? Ok(user) : NotFound();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Put(int id, UserPasswordDto user) {
            try {
                var updatedUser = userService
                    .UpdateUser(id, mapper.Map<UserBL>(user,
                        o => o.AfterMap((src, dest) => dest.Id = id)));

                return updatedUser != null ? Ok(mapper.Map<UserDto>(updatedUser)) : NotFound();
            }
            catch (UserAlreadyExistsException ex) {
                return Conflict(ex.Message);
            }
            catch (UserException ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Patch(int id, UserPasswordDto user) {
            try {
                var updatedUser = userService
                    .UpdateUser(id, userConverters.convertPatch(id, user));
                return updatedUser != null ? Ok(mapper.Map<UserDto>(updatedUser)) : NotFound();
            }
            catch (UserAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id) {
            var deletedUser = userService
                .DeleteUser(id);
            return deletedUser != null ? Ok(mapper.Map<UserDto>(deletedUser)) : NotFound();
        }

        [HttpGet("{userId}/favorites")]
        public IActionResult GetFavorites(
            int userId,
            [FromQuery] ServerFilterDto filter,
            [FromQuery] ServerSortState? sortState,
            [FromQuery] int? page,
            [FromQuery] int? pageSize
        )
        {
            return Ok(mapper.Map<IEnumerable<ServerDto>>(userService.GetUserFavoriteServers(userId, filter, sortState, page, pageSize)));
        }

        [HttpGet("{userId}/favorites/{serverId}")]
        [ProducesResponseType(typeof(FavoriteServerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult GetFavoriteByServerAndUserId(int userId, int serverId) {
            var favServer = mapper.Map<FavoriteServerDto>(userService.GetFavoriteByServerAndUserId(userId, serverId));
            return favServer != null ? Ok(favServer) : NotFound();
        }

        [HttpPost("{userId}/favorites/{serverId}")]
        [ProducesResponseType(typeof(FavoriteServerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult AddFavorite(int userId, int serverId)
        {
            try
            {
                return Ok(mapper.Map<FavoriteServerDto>(userService.AddFavoriteServer(userId, serverId)));
            }
            catch (UserFavoriteAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{userId}/favorites/{serverId}")]
        [ProducesResponseType(typeof(FavoriteServerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult DeleteFavorite(int userId, int serverId)
        {
            var deletedFavorite = userService.DeleteFavoriteServer(userId, serverId);
            return deletedFavorite != null ? Ok(mapper.Map<FavoriteServerDto>(deletedFavorite)) : NotFound();
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult Login(LoginDto loginDto) {
            var result = userService.Login(loginDto);
            return result != null ? Ok(mapper.Map<UserDto>(result)) : NotFound();
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(UserIdPasswordDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Register(LoginDto loginDto) {
            var userDto = new UserPasswordDto {
                Login = loginDto.Login,
                Password = loginDto.Password,
                Role = "user"
            };
            
            return Add(userDto);
        }
    }
}