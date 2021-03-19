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

        public ReposController()
        {
            ApiHelper.InitializeClient();
        }

        // GET: api/<ReposController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{username}")]
        public async Task<List<Repo>> Get(string username)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"{username}/repos"))
            {
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.ToString());
                    List<Repo> repos = await JsonSerializer.DeserializeAsync<List<Repo>>(await response.Content.ReadAsStreamAsync());
                    return repos;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
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
