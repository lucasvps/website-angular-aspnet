using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetWebApi.Models
{
    public abstract class BaseModelo
    {
        [Key]
        public long Id { get; set; }
    }
}