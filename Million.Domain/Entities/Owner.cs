namespace Million.Domain.Entities
{
    public class Owner
    {
        public int IdOwner { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public string Birthday { get; set; }
        public ICollection<Property> Properties { get; set; }
    }
}
