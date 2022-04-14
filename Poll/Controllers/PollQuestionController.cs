using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using Poll.Interfaces;

namespace Poll.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollQuestionController : ControllerBase
    {
        private readonly ILogger<PollQuestionController> _logger;
        private Main _dbConn;
        private readonly IPollQuestionService _pollQuestionService;
        private readonly IUserService _userService;
        private readonly IPollService _pollService;

        public PollQuestionController(ILogger<PollQuestionController> logger, Main dbConn, IPollQuestionService pollQuestionService, IUserService userService, IPollService pollService)
        {
            _logger = logger;
            _dbConn = dbConn;
            _pollQuestionService = pollQuestionService;
            _userService = userService;
            _pollService = pollService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllFromPoll(int id)
        {
            var item = await _pollQuestionService.GetAllFromPollAsync(id);
            if (item == null)
                return BadRequest("Nada foi encontrado.");

            return Ok(item);
        }


        [HttpPost]
        public async Task<IActionResult> Save([FromBody] PollQuestionViewModel pollQuestion)
        {
            if (ModelState.IsValid)
            {
                var lstNotInserted = new List<Models.PollQuestion>();

                if (App.CheckPwd.CheckValidPwd(pollQuestion.user, _userService))
                {
                    pollQuestion.poll.UserId = pollQuestion.user.Id;
                    var resultPoll = await _pollService.AddOrUpdate(pollQuestion.poll);
                    if (resultPoll > 0)
                    {
                        foreach (var f in pollQuestion.questions)
                        {
                            var result = await _pollQuestionService.AddOrUpdate(f);
                            if (result == 0)
                            {
                                lstNotInserted.Add(f);
                            }
                        };

                        if (lstNotInserted.Any())
                            return BadRequest(lstNotInserted);

                        return Ok();
                    }
                }

                return Unauthorized();
            }

            return BadRequest();
        }
    }
}
