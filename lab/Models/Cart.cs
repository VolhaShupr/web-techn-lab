using lab.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace lab.Models
{
    public class Cart
    {
        public Dictionary<int, CartItem> Items { get; set; }
        public Cart()
        {
            Items = new Dictionary<int, CartItem>();
        }

        ///<summary>
        ///Количество объектов в корзине
        ///</summary>
        public int Count
        {
            get { return Items.Sum(item => item.Value.Quantity); }
        }

        ///<summary>
        ///Количество калорий
        ///</summary>
        //public int Calories
        //{
        //    get { return Items.Sum(item => item.Value.Quantity * item.Value.Dish.Calories); }
        //}

        ///<summary>
        ///Добавление в корзину
        ///</summary>
        ///<param name="instrument">добавляемый объект</param> 
        public virtual void AddToCart(MusInstrument instrument)
        {
            //если объект есть в корзине то увеличить количество
            if (Items.ContainsKey(instrument.Id))
                Items[instrument.Id].Quantity++;

            //иначе - добавить объект в корзину 
            else
                Items.Add(instrument.Id, new CartItem { MusInstrument = instrument, Quantity = 1 });
        }

        ///<summary>
        ///Удалить объект из корзины
        ///</summary>
        ///<param name="id">id удаляемого объекта</param> 
        public virtual void RemoveFromCart(int id)
        {
            Items.Remove(id);
        }

        ///<summary>
        ///Очистить корзину
        ///</summary>
        public virtual void ClearAll()
        {
            Items.Clear();
        }
    }

    ///<summary>
    ///Клас описывает одну позицию в корзине
    ///</summary>
    public class CartItem
    {
        public MusInstrument MusInstrument { get; set; }
        public int Quantity { get; set; }
    }

}
