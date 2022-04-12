using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;
using System.Threading;

namespace PollInterface.Pages
{
    public class RolesModel : PageModel
    {
        public List<Poll.Models.Role> Roles { get; set; }
        public void OnGet()
        {
            Roles = GetAllRoles().Result;
        }

        public async Task<List<Poll.Models.Role>> GetAllRoles()
        {
            var client = new RestClient("http://localhost:5014/api/roles");
            var request = new RestRequest();
            var response = await client.GetAsync<List<Poll.Models.Role>>(request);

            return response;
        }
    }
}
