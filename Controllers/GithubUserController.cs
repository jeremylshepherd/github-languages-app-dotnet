using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using GithubLanguagesApp.Models;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using GithubLanguagesApp.Helpers;
using GithubLanguagesApp.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GithubLanguagesApp.Controllers
{    

    [Route("api/[controller]")]
    [ApiController]
    public class GithubUserController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly GithubProcessor githubProcessor;

        public GithubUserController(ApplicationDbContext context)
        {
            _context = context;
            githubProcessor = new GithubProcessor(context);
    }


        // GET: api/<GithubUserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<GithubUserController>/jeremylshepherd
        [HttpGet("{username}")]
        public async Task<GithubUser> Get(string username)
        {
            GithubUser user = await githubProcessor.GetUser(username);
            return user;
        }

        // POST api/<GithubUserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GithubUserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GithubUserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
