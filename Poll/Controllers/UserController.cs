using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poll.Interfaces;

namespace Poll.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private Main _dbConn;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserController(ILogger<UserController> logger, Main dbConn, IUserService userService, IRoleService roleService)
        {
            _logger = logger;
            _dbConn = dbConn;
            _userService = userService;
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lst = await _userService.GetAllAsync();
            if (lst.Any())
                return Ok(lst);

            return BadRequest("Nada foi encontrado.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _userService.GetAsync(id);
            if (item == null)
                return BadRequest("Nada foi encontrado.");

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] ViewModels.UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                if (user.role is null)
                {
                    return BadRequest();
                }

                var addRole = await _roleService.GetAsync(user.role.Id);
                if (addRole is null)
                {
                    var newRole = await _roleService.AddOrUpdate(user.role);
                    if (newRole == 0)
                        return BadRequest("Falha ao incluir Role");

                    user.user.RoleId = newRole;
                }

                var result = await _userService.AddOrUpdate(user.user);
                if (result > 0)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest();
        }
    }
}
