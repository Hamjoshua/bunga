using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bunga.Models
{
    public class Bungalo
    {
        [Key]
        public int Id_бунгало { get; set; }
        public string Адрес { get; set; }
        public int Количество_челоек { get; set; }
        public bool Wi_fi { get; set; }       
        public virtual IdentityUser Сдающий { get; set; }
        public int Стоимость_сутки { get; set; }
    }
}
