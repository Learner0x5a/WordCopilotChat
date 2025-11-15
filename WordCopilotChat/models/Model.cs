using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCopilotChat.models
{
    [Table(Name = "models")]
    public class Model
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }

        [Column(Name = "nick_name")]
        public string NickName { get; set; }

        [Column(Name = "template_id")]
        public int TemplateId { get; set; }
        public RequestTemplate Template { get; set; }

        [Column(Name = "api_key")]
        public string ApiKey { get; set; }

        [Column(Name = "base_url")]
        public string BaseUrl { get; set; }

        [Column(Name = "parameters")]
        public string Parameters { get; set; }
        [Column(Name = "enable_multi")] // 0:禁用,1:多模态启用
        public int EnableMulti { get; set; }
        [Column(Name = "enable_tools")] // 0:禁用,1:启用
        public int EnableTools { get; set; }
        [Column(Name = "enable_think")] // 0:禁用,1:启用
        public int EnableThink { get; set; }
        [Column(Name = "model_type")] // 1:chat,2:embedding
        public int modelType { get; set; }

    }
}
