using Highway.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Widgets.Data.Queries;

namespace Widgets.Data
{
    public class CartService
    {
        private IRepository _repository;

        public CartService(IRepository repository)
        {
            _repository = repository;
        }

        public Cart GetCart(User user)
        {
            var cart = _repository.Find(new GetCartByUserId(user.UserId));

            if (cart == null)
            {
                cart = _repository.Context.Add(Cart.Create(user.UserId));
            }

            return cart;
        }

        public Cart AddToCart(User user, Widget widget)
        {
            var cart = _repository.Find(new GetCartByUserId(user.UserId));

            if (cart == null)
            {
                cart = _repository.Context.Add(Cart.Create(user.UserId));
            }

            cart.AddItem(widget);
            return cart;
        }

        public Cart RemoveFromCart(User user, Widget widget)
        {
            var cart = _repository.Find(new GetCartByUserId(user.UserId));

            if (cart == null)
            {
                return null;
            }

            cart.RemoveItem(widget);
            return cart;
        }
    }
}
