using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLab4.Entities;

namespace WebApplication_Bordasheva.Models
{
    public class Cart
    {
        public Dictionary<int, CartItem> Items { get; set; }
        public Cart()
        {
            Items = new Dictionary<int, CartItem>();
        }
        /// <summary> 
        /// Количество объектов в корзине 
        /// </summary> 
        public int Count
        {
            get
            {
                return Items.Sum(item => item.Value.Quantity);
            }
        }
        /// <summary> 
        /// Сумма покупки
        /// </summary> 
        public int Price
        {
            get
            {
                return Items.Sum(item => item.Value.Quantity *
                    item.Value.Phone.Price);
            }
        }

        /// <summary> 
        /// Добавление в корзину 
        /// </summary> 
        /// <param name="phone">добавляемый объект</param> 
        public virtual void AddToCart(Phone phone)
        {
            // если объект есть в корзине 
            // то увеличить количество 
            if (Items.ContainsKey(phone.PhoneId))
                Items[phone.PhoneId].Quantity++;
            // иначе - добавить объект в корзину 
            else
                Items.Add(phone.PhoneId, new CartItem
                {
                    Phone = phone,
                    Quantity = 1
                });
        }

        /// <summary> 
        /// Удалить объект из корзины 
        /// </summary> 
        /// <param name="id">id удаляемого объекта</param> 
        public virtual void RemoveFromCart(int id)
        {
            Items.Remove(id);
        }

        /// <summary> 
        /// Очистить корзину 
        /// </summary> 
        public virtual void ClearAll()
        {
            Items.Clear();
        }
    }

    /// <summary> 
    /// Клас описывает одну позицию в корзине 
    /// </summary> 
    public class CartItem
    {
        public Phone Phone { get; set; }
        public int Quantity { get; set; }
    }
}
