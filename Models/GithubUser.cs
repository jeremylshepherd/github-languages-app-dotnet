using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GithubLanguagesApp.Models
{
    public class GithubUser
    {

        [JsonProperty("id")]
        [JsonPropertyName("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int GithubUserId { get; set; }
        [JsonProperty("login")]
        [JsonPropertyName("login")]
        public string GithubUsername { get; set; }
        [JsonProperty("avatar_url")]
        [JsonPropertyName("avatar_url")]
        public string GithubUserAvatar { get; set; }
        public List<Repo> Repos { get; set; }

    }
}
