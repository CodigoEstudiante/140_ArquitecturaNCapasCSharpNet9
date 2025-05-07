using _3.DataLayer.Entities;
using Microsoft.Data.SqlClient;

namespace _3.DataLayer;

public class ProductDL
{
    private readonly string _connectionString;

    public ProductDL(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Product> GetAll() {

        var products = new List<Product>();

        using (var cn = new SqlConnection(_connectionString))
        {
            cn.Open();
            var command = new SqlCommand("select IdProduct,Name,Price from Product", cn);
            command.CommandType = System.Data.CommandType.Text;

            using (var reader = command.ExecuteReader()) {
                while (reader.Read()) { 
                    products.Add(new Product()
                    {
                        IdProduct = int.Parse(reader["IdProduct"].ToString()!),
                        Name = reader["Name"].ToString()!,
                        Price = int.Parse(reader["Price"].ToString()!)
                    });
                }
            }
        }
        return products;
    }

    public bool Create(Product value)
    {
        var created = false;

        using (var cn = new SqlConnection(_connectionString))
        {
            cn.Open();
            var command = new SqlCommand("insert into Product(Name,Price) values(@Name,@Price)", cn);
            command.Parameters.AddWithValue("@Name", value.Name);
            command.Parameters.AddWithValue("@Price", value.Price);
            command.CommandType = System.Data.CommandType.Text;

            created = command.ExecuteNonQuery() != 0;
        }
        return created;
    }

    public bool Edit(Product value)
    {
        var edited = false;

        using (var cn = new SqlConnection(_connectionString))
        {
            cn.Open();
            var command = new SqlCommand("update Product set Name = @Name ,Price = @Price where IdProduct = @IdProduct", cn);
            command.Parameters.AddWithValue("@IdProduct", value.IdProduct);
            command.Parameters.AddWithValue("@Name", value.Name);
            command.Parameters.AddWithValue("@Price", value.Price);
            command.CommandType = System.Data.CommandType.Text;

            edited = command.ExecuteNonQuery() != 0;
        }
        return edited;
    }

    public bool Delete(int IdProduct) {
        var deleted = false;

        using (var cn = new SqlConnection(_connectionString))
        {
            cn.Open();
            var command = new SqlCommand("delete from Product where IdProduct = @IdProduct", cn);
            command.Parameters.AddWithValue("@IdProduct", IdProduct);
            command.CommandType = System.Data.CommandType.Text;

            deleted = command.ExecuteNonQuery() != 0;
        }
        return deleted;
    }
}
