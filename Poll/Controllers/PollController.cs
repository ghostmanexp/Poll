using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Poll.Interfaces;

namespace Poll.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollController : ControllerBase
    {
        private readonly ILogger<PollController> _logger;
        private Main _dbConn;
        private readonly IPollService _pollService;
        private readonly IUserService _userService;

        public PollController(ILogger<PollController> logger, Main dbConn, IPollService pollService, IUserService userService)
        {
            _logger = logger;
            _dbConn = dbConn;
            _pollService = pollService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lst = await _pollService.GetAllAsync();
            if (lst.Any())
                return Ok(lst);

            return BadRequest("Nada foi encontrado.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _pollService.GetAsync(id);
            if (item == null)
                return BadRequest("Nada foi encontrado.");

            return Ok(item);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllFromUser(int id)
        {
            var item = await _pollService.GetAllFromUserAsync(id);
            if (item == null)
                return BadRequest("Nada foi encontrado.");

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] ViewModels.PollViewModel poll)
        {
            if (ModelState.IsValid)
            {
                if (CheckValidPwd(poll.user))
                {
                    poll.poll.UserId = poll.user.Id;
                    var result = await _pollService.AddOrUpdate(poll.poll);
                    if (result > 0)
                        return Ok(result);

                    return BadRequest(result);
                }

                return Unauthorized();
            }

            return BadRequest();
        }

        private bool CheckValidPwd(Models.User user)
        {
            var hashPwd = _userService.GetAsync(user.Id).Result;
            var passwordVerificationResult = new PasswordHasher<object?>().VerifyHashedPassword(null, hashPwd.Password, user.Password);
            if (passwordVerificationResult == PasswordVerificationResult.Success)
                return true;

            return false;
        }
    }
}
