namespace DapperApi.Services.Queries
{
    public class CommandText : ICommandText
    {
        public string GetProducts
        {
            get
            {
                return "Select * From Product";
            }
        }

        public string GetProductById => "Select * From Product Where Id= @Id";

        public string AddProduct => "Insert Into  Product (Name, Cost, CreatedDate) Values (@Name, @Cost, @CreatedDate)";

        public string UpdateProduct => "Update Product set Name = @Name, Cost = @Cost, CreatedDate = GETDATE() Where Id =@Id";

        public string RemoveProduct => "Delete From Product Where Id= @Id";
    }
}