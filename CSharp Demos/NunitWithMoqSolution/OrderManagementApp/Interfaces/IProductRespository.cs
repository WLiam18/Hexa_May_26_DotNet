using OrderManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementApp.Interfaces
{
    public interface IProductRespository
    {
        Product? GetProductById(int productId);
        void UpdateStock(int productId, int qunatity);
    }
}
