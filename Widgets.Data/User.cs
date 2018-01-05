using Highway.Data;

namespace Widgets.Data
{
    public class User : IIdentifiable<int>
    {
        private User() { }

        int IIdentifiable<int>.Id
        {
            get { return UserId; }
            set { UserId = value; }
        }

        public int UserId
        {
            get;
            private set;
        }

        public string Email
        {
            get;
            private set;
        }

        public static User Create(string email)
        {
            return new User
            {
                Email = email
            };
        }
    }
}
