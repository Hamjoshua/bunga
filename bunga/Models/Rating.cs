using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bunga.Models
{
    public class Rating
    {
        [Key]
        public int Id_рейтинг { get; set; }
        public int Оценка { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "Id_бунгало")]
        public virtual Bungalo Бунгало {get; set;}

        [ScaffoldColumn(true)]
        [Display(Name = "Id_покупатель")]
        public virtual IdentityUser Покупатель { get; set; }
    }
}
