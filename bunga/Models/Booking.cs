using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bunga.Models
{
    public class Booking
    {
        [Key]
        public int Id_бронь { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'.'MM'.'yyyy}")]
        [CheckDateRange()]
        public DateTime Дата_начала {get; set;}
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'.'MM'.'yyyy}")]
        [CheckDateRange()]
        public DateTime Дата_конца { get; set; }
        public virtual IdentityUser Арендатор { get; set; }
        public virtual Bungalo Бунгало { get; set; }
    }

    public class CheckDateRangeAttribute : ValidationAttribute
    {
        private DateTime _time;
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            _time = DateTime.UtcNow;
            DateTime dt = (DateTime)value;
            if (dt >= _time)
            {
                return ValidationResult.Success;
            }

            string err_msg = "Дата бронирования бунгало должна быть больше, чем сегодня";
            return new ValidationResult(ErrorMessage ?? err_msg);
        }

    }
}
