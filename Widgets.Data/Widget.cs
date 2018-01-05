using Highway.Data;

namespace Widgets.Data
{
    public class Widget : IIdentifiable<int>
    {
        private Widget() { }

        int IIdentifiable<int>.Id
        {
            get { return WidgetId; }
            set { WidgetId = value; }
        }

        public int WidgetId
        {
            get;
            private set;
        }

        public string Description
        {
            get;
            private set;
        }

        public decimal Price
        {
            get;
            private set;
        }

        public static Widget Create(string description, decimal price)
        {
            return new Widget
            {
                Description = description,
                Price = price
            };
        }
    }
}
