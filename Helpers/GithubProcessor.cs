using GithubLanguagesApp.Data;
using GithubLanguagesApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GithubLanguagesApp.Helpers
{
    public class GithubProcessor
    {
        private readonly ApplicationDbContext _context;
        public GithubProcessor(ApplicationDbContext context)
        {
            _context = context;
            ApiHelper.InitializeClient();
        }

        public async Task<GithubUser> GetUser(string username)
        {
            GithubUser user = _context.GithubUsers.Where(u => u.GithubUsername == username).FirstOrDefault();
            if(user == null)
            {
                user = await GetGithubUserFromApi(username);
            }
            List<Repo> repos = _context.Repos.Where(u => u.Owner == user).Include(r => r.RepoLanguages).ToList();
            user.Repos = repos;
           
            return user;
            
        }

        public async Task<List<Repo>> GetRepos(GithubUser user)
        {
            
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"{user.GithubUsername}/repos"))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<Repo> repos = await JsonSerializer.DeserializeAsync<List<Repo>>(await response.Content.ReadAsStreamAsync());
                    foreach (Repo repo in repos)
                    {
                        List<RepoLanguage> repoLanguages = await GetRepoLanguages(repo);
                        repo.RepoLanguages = repoLanguages;
                    }
                    return repos;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        } 
        
        public async Task<List<RepoLanguage>> GetRepoLanguages(Repo repo)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(repo.LanguagesUrl))
            {
                List<RepoLanguage> repoLanguages = new List<RepoLanguage>();
                if (response.IsSuccessStatusCode)
                {
                    Dictionary<string, long> dict = await JsonSerializer.DeserializeAsync<Dictionary<string, long>>(await response.Content.ReadAsStreamAsync());
                    foreach(KeyValuePair<string, long> entry in dict)
                    {
                        var repoLanguage = new RepoLanguage
                        {
                            Language = entry.Key,
                            ByteSize = entry.Value,
                            RepoId = repo.RepoId
                        };
                        repoLanguages.Add(repoLanguage);
                    }
                    return repoLanguages;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<GithubUser> GetGithubUserFromApi(string username)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(username))
            {
                if (response.IsSuccessStatusCode)
                {
                    GithubUser user = await JsonSerializer.DeserializeAsync<GithubUser>(await response.Content.ReadAsStreamAsync());
                    List<Repo> repositories = await GetRepos(user);
                    user.Repos = repositories;

                    _context.GithubUsers.Add(user);
                    _context.SaveChanges();
                    return user;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
               
    }
}
