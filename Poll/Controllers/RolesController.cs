using Microsoft.AspNetCore.Mvc;
using Poll.Interfaces;

namespace Poll.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ILogger<RolesController> _logger;
        private Main _dbConn;
        private readonly IRoleService _roleService;

        public RolesController(ILogger<RolesController> logger, Main dbConn, IRoleService roleService)
        {
            _logger = logger;
            _dbConn = dbConn;
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lst = await _roleService.GetAllAsync();
            if (lst.Any())
                return Ok(lst);

            return BadRequest("Nada foi encontrado.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _roleService.GetAsync(id);
            if (item == null)
                return BadRequest("Nada foi encontrado.");

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Models.Role role)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleService.AddOrUpdate(new Models.Role { Name = role.Name });
                if (result > 0)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest();
        }
    }
}
