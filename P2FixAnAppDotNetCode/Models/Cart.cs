using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        /// <summary>
        /// Read-only property for dispaly only
        /// </summary>
        public IEnumerable<CartLine> Lines => GetCartLineList();

        //private Dictionary<int,CartLine> dictio_cartline = new Dictionary<int,CartLine>();
        private List<CartLine> list_cartline = new List<CartLine>();
        /// <summary>
        /// Return the actual cartline list
        /// </summary>
        /// <returns></returns>
        private List<CartLine> GetCartLineList()
        {
            return list_cartline;
        }

        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {
            bool already_exist = false;
            foreach (CartLine item in list_cartline)
            {
                if (item.Product.Name == product.Name)
                {
                    already_exist = true;
                    item.Quantity += 1;
                    break;
                }
            }
            if (already_exist == false)
            {
                list_cartline.Add(new CartLine(list_cartline.Count + 1, product, quantity));
            }
        }

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product) =>
                GetCartLineList().RemoveAll(l => l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            double total = 0;
            list_cartline.ForEach(x => total += x.Product.Price * x.Quantity);
            return total;
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            double sum = 0;
            int count = 0;
            list_cartline.ForEach(x => sum += x.Product.Price * x.Quantity);
            list_cartline.ForEach(x => count += x.Quantity);
            return sum / count;
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            foreach (CartLine element in Lines)
            {
                if (element.Product.Id == productId)
                {
                    return element.Product;
                }
            }

            //return new Product();
            return null;
        }

        /// <summary>
        /// Get a specifid cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            return Lines.ToArray()[index];
        }

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        //public void Clear()
        //{
        //    List<CartLine> cartLines = GetCartLineList();
        //    cartLines.Clear();
        //}

        public void Clear() => GetCartLineList().Clear();
        //list_cartline.Clear();
    }

    public class CartLine
    {
        public int OrderLineId = new int();
        public Product Product;
        public int Quantity = new int();

        public CartLine(int line_id, Product product_new, int quantity_new)
        {
            OrderLineId = line_id;
            Product = product_new;
            Quantity = quantity_new;
        }
    }
}
