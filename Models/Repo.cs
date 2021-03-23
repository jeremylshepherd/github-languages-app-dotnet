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
    public class Repo
    {
        [JsonProperty("id")]
        [JsonPropertyName("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int RepoId { get; set; }
        [JsonProperty("name")]
        [JsonPropertyName("name")]
        public string RepoName { get; set; }
        [JsonProperty("owner")]
        [JsonPropertyName("owner")]
        public GithubUser Owner { get; set; }
        [JsonProperty("created_at")]
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("languages_url")]
        [JsonPropertyName("languages_url")]
        public Uri  LanguagesUrl { get; set; }
        public List<RepoLanguage> RepoLanguages { get; set; }
    }
}
