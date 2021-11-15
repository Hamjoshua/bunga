using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace bunga.Models
{
    public class Bungalo
    {
        [Key]
        public int Id_бунгало { get; set; }
        public string Название { get; set; }
        public string Адрес { get; set; }
        public string Описание { get; set; }
        public int Количество_человек { get; set; }
        public bool Wi_fi { get; set; }       
        public virtual IdentityUser Сдающий { get; set; }
        public int Стоимость_сутки { get; set; }
        public float Оценка { get; set; }
        public string[] GetImages()
        {
            string path = String.Format("wwwroot\\static\\bungaloes\\{0}", Id_бунгало);

            if (Directory.Exists(path))
            {
                string[] images_paths = Directory.GetFiles(path);
                if(images_paths.Length == 0)
                {
                    return new string[] { "/static/bungaloes/image-missing-icon.png" };
                }
                for(int i=0; i < images_paths.Length; i++)
                {
                    images_paths[i] = images_paths[i].Replace("wwwroot", "");
                }
                return images_paths;
            }

            return new string[] {"/static/bungaloes/image-missing-icon.png"};
        }
        public string GetImageFirst()
        {
            return GetImages()[0];
        }
    }
}
