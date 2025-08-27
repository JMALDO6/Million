namespace Million.Domain.Entities
{
    public class Property
    {
        public int IdProperty { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public int Year { get; set; }
        public int IdOwner { get; set; }
        public Owner Owner { get; set; }
    }
}
