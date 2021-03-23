using GithubLanguagesApp.Data;
using GithubLanguagesApp.Helpers;
using GithubLanguagesApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GithubLanguagesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReposController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly GithubProcessor githubProcessor;
        
        public ReposController(ApplicationDbContext context)
        {
            _context = context;
            githubProcessor = new GithubProcessor(context);
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{username}")]
        public async Task<List<Repo>> Get(string username)
        {
            GithubUser user = await githubProcessor.GetUser(username);
            List<Repo> repos = await githubProcessor.GetRepos(user);
            return repos;
        }

        // POST api/<ReposController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ReposController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReposController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
