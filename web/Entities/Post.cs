using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web.Entities
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Html { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}
