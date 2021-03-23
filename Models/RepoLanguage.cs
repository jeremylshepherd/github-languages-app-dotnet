using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubLanguagesApp.Models
{
    public class RepoLanguage
    {
        public int RepoLanguageId { get; set; }
        public int RepoId { get; set; }
        public string Language { get; set; }
        public long ByteSize { get; set; }
    }
}
