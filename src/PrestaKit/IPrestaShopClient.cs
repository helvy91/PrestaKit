using PrestaKit.Clients.Media.Attachments;
using PrestaKit.Clients.Media.Images;
using PrestaKit.Clients.Resources;
using PrestaKit.Entities;
using PrestaKit.Entities.Common;

namespace PrestaKit
{
    /// <summary>
    /// The entry point for the PrestaShop webservice. Exposes typed clients for each
    /// resource, plus image and attachment file operations.
    /// </summary>
    public interface IPrestaShopClient
    {
        /// <summary>Returns a typed client for the given resource type.</summary>
        /// <typeparam name="T">The resource entity type.</typeparam>
        IResourceClient<T> Resource<T>() where T : PrestaShopEntity;

        /// <summary>Client for the <c>products</c> resource.</summary>
        IResourceClient<Product> Products { get; }

        /// <summary>Client for the <c>categories</c> resource.</summary>
        IResourceClient<Category> Categories { get; }

        /// <summary>Client for the <c>orders</c> resource.</summary>
        IResourceClient<Order> Orders { get; }

        /// <summary>Client for the <c>customers</c> resource.</summary>
        IResourceClient<Customer> Customers { get; }

        /// <summary>Client for the <c>stock_availables</c> resource.</summary>
        IResourceClient<StockAvailable> StockAvailables { get; }

        /// <summary>Client for the <c>combinations</c> resource.</summary>
        IResourceClient<Combination> Combinations { get; }

        /// <summary>Client for the <c>manufacturers</c> resource.</summary>
        IResourceClient<Manufacturer> Manufacturers { get; }

        /// <summary>Client for the <c>suppliers</c> resource.</summary>
        IResourceClient<Supplier> Suppliers { get; }

        /// <summary>Client for the <c>addresses</c> resource.</summary>
        IResourceClient<Address> Addresses { get; }

        /// <summary>Client for the <c>order_states</c> resource.</summary>
        IResourceClient<OrderState> OrderStates { get; }

        /// <summary>Client for uploading, downloading, and managing resource images.</summary>
        IImageClient Images { get; }

        /// <summary>Client for uploading, downloading, and managing attachment files.</summary>
        IAttachmentFileClient Attachments { get; }
    }
}
