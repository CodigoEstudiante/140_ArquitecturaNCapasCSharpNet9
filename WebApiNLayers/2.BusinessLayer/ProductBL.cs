using _3.DataLayer;
using _3.DataLayer.Entities;

namespace _2.BusinessLayer;

public class ProductBL
{
    private readonly ProductDL _productDL;

    public ProductBL(ProductDL productDL)
    {
        _productDL = productDL;
    }

    public List<Product> GetAll()
    {
        return _productDL.GetAll();
    }

    public bool Create(Product value)
    {
        if (string.IsNullOrWhiteSpace(value.Name))
            throw new ArgumentException("El nombre del producto es requerido");

        if (value.Price <= 0)
            throw new ArgumentException("El precio debe ser mayor que cero");

        return _productDL.Create(value);
    }

    public bool Edit(Product value)
    {
        if (value.IdProduct == 0)
            throw new ArgumentException("El idProducto es requerido");

        if (string.IsNullOrWhiteSpace(value.Name))
            throw new ArgumentException("El nombre del producto es requerido");

        if (value.Price <= 0)
            throw new ArgumentException("El precio debe ser mayor que cero");

        return _productDL.Edit(value);
    }
    public bool Delete(int IdProduct)
    {
        if (IdProduct == 0)
            throw new ArgumentException("El idProducto es requerido");

        return _productDL.Delete(IdProduct);
    }
}
