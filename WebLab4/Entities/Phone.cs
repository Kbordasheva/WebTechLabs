using System;
using System.Collections.Generic;
using System.Text;

namespace WebLab4.Entities
{
    public class Phone
    {
        public int PhoneId { get; set; } // id телефона
        public string PhoneName { get; set; } // название телефона
        public string Description { get; set; } // описание телефона
        public int Price { get; set; } // цена телефона
        public string Image { get; set; } // имя файла изображения

        // Навигационные свойства 
        /// <summary> 
        /// группа телефонов по производителям (Samsung, Apple, Xiaomi и т.д.) 
        /// </summary> 
        public int PhoneGroupId { get; set; }
        public PhoneGroup Group { get; set; }
    }
}
