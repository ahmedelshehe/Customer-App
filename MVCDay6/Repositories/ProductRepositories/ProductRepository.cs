using MVCDay6.Models;

namespace MVCDay6.Repositories.ProductRepositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly Context context;

        public ProductRepository(Context context) : base(context)
        {
            this.context = context;
        }
    }
}
