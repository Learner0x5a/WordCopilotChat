using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCopilotChat.models
{
    [Table(Name = "request_template")]
    public class RequestTemplate
    {

        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }

        [Column(Name = "template_name")]
        public string TemplateName { get; set; }
    }
}
