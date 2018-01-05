using Highway.Data;
using System.Linq;

namespace Widgets.Data.Queries
{
    public class GetCartByUserId : Scalar<Cart>
    {
        public GetCartByUserId(int userId)
        {
            ContextQuery = context => context.AsQueryable<Cart>().FirstOrDefault(x => x.UserId == userId);
        }
    }
}
