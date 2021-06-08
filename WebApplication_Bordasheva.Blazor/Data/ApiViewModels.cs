using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WebApplication_Bordasheva.Blazor.Data
{
    public class ApiViewModels
    {
    }

    public class ListViewModel
    {
        [JsonPropertyName("phoneId")]
        public int PhoneId { get; set; } // id телефона 
        [JsonPropertyName("phoneName")]
        public string PhoneName { get; set; } // название телефона 

    }

    public class DetailsViewModel
    {
        [JsonPropertyName("phoneName")]
        public string PhoneName { get; set; } // название телефона
        [JsonPropertyName("description")]
        public string Description { get; set; } // описание телефона 
        [JsonPropertyName("price")]
        public int Price { get; set; } // цена
        [JsonPropertyName("image")]
        public string Image { get; set; } // имя файла изображения    
    }

}
