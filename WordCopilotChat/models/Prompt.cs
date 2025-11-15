using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCopilotChat.models
{
    [Table(Name = "prompts")]
    public class Prompt
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }

        [Column(Name = "prompt_type")]
        public string PromptType { get; set; }

        [Column(Name = "prompt_content", DbType = "TEXT")]
        public string PromptContent { get; set; }

        [Column(Name = "created_at")]
        public DateTime CreatedAt { get; set; }

        [Column(Name = "updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column(Name = "is_default")]
        public bool IsDefault { get; set; }

        [Column(Name = "description")]
        public string Description { get; set; }
    }
} 