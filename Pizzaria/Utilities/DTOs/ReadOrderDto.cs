namespace Pizzaria.Utilities.DTOs
{
    public class ReadOrderDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Table { get; set; }
        public int PizzaMenuNumber { get; set; }
    }
}
