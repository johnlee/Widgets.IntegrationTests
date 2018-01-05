using Highway.Data;
using System.Linq;

namespace Widgets.Data.Queries
{
    public class GetUserByEmail : Scalar<User>
    {
        public GetUserByEmail(string email)
        {
            ContextQuery = context => context.AsQueryable<User>().FirstOrDefault(x => x.Email == email);
        }
    }
}