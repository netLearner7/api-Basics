﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web.api.Entites;

namespace web.api.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProduct(int productId, bool includeMaterials);
        IEnumerable<Material> GetMaterialsForProduct(int productId);
        Material GetMaterialForProduct(int productId, int materialId);
    }
}
