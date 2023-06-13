using DataLayer;
using EntityLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class BL_ShoppingCart
    {
        private DL_ShoppingCart oDataLayer = new DL_ShoppingCart();

        /// <summary>
        /// Method to know if there is at least one product in a Client's shopping cart
        /// </summary>
        /// <param name="clientId">Id of the Client</param>
        /// <param name="productId">Id of the Product</param>
        /// <returns></returns>
        public bool CartExists(int clientId, int productId)
        {
            return oDataLayer.CartExists(clientId, productId);
        }

        /// <summary>
        /// Method to add products to a Client's shopping cart
        /// </summary>
        /// <param name="clientId">Id of the Client</param>
        /// <param name="productId">Id of the Product</param>
        /// <param name="add">Is adding (true - false)</param>
        /// <param name="Message">Out param with message</param>
        /// <returns></returns>
        public bool ManageProductCartAmount(int clientId, int productId, bool add, out string Message)
        {
            return oDataLayer.ManageProductCartAmount(clientId, productId, add, out Message);
        }

        /// <summary>
        /// Method to get the amount of products of one client's propduct cart
        /// </summary>
        /// <param name="clientId">Id of the Client</param>
        /// <returns></returns>
        public int GetShoppingCartAmount(int clientId)
        {
            return oDataLayer.GetShoppingCartAmount(clientId);
        }

        /// <summary>
        /// Method to get all the products in the shopping cart of a CLient
        /// </summary>
        /// <param name="clientId">Id of the Client</param>
        /// <returns></returns>
        public List<Shopping_Cart> GetProductsInCart(int clientId)
        {
            return oDataLayer.GetProductsInCart(clientId);
        }

        /// <summary>
        /// Method to delete a product from client's shopping cart
        /// </summary>
        /// <param name="clientId">Id of the Client</param>
        /// <param name="productId">Id of the Product</param>
        /// <returns></returns>
        public bool DeleteCartProduct(int clientId, int productId)
        {
            return oDataLayer.DeleteCartProduct(clientId, productId);
        }
    }
}
