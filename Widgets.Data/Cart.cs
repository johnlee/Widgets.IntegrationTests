using Highway.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Widgets.Data
{
    public class Cart : IIdentifiable<int>
    {
        private Cart()
        {
            CartItems = new List<Widget>();
        }

        int IIdentifiable<int>.Id
        {
            get { return CartId; }
            set { CartId = value; }
        }

        public int CartId
        {
            get;
            private set;
        }

        public User User
        {
            get;
            private set;
        }

        public int UserId
        {
            get;
            private set;
        }

        public ICollection<Widget> CartItems
        {
            get;
        }

        public DateTime CreatedAt
        {
            get;
            private set;
        }

        public void AddItem(Widget widget)
        {
            var cartItem = CartItems.Where(x => x.WidgetId == widget.WidgetId).FirstOrDefault();
            if (cartItem == null)
            {
                CartItems.Add(widget);
            }
        }

        public void RemoveItem(Widget widget)
        {
            var cartItem = CartItems.Where(x => x.WidgetId == widget.WidgetId).FirstOrDefault();
            if (cartItem != null)
            {
                CartItems.Remove(widget);
            }
        }

        public static Cart Create(int userId)
        {
            return new Cart
            {
                UserId = userId,
                CreatedAt = DateTime.Now
            };
        }
    }
}
