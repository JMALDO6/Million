namespace Million.Domain.Entities
{
    /// <summary>
    /// Property entity representing a real estate property.
    /// </summary>
    /// <remarks>
    /// Constructor for Property entity.
    /// </remarks>
    /// <param name="name"></param>
    /// <param name="address"></param>
    /// <param name="price"></param>
    /// <param name="codeInternal"></param>
    /// <param name="year"></param>
    /// <param name="idOwner"></param>
    public class Property(string name, string address, decimal price, string codeInternal, int year, int idOwner)
    {
        /// <summary>
        /// Id of the property.
        /// </summary>
        public int IdProperty { get; set; }

        /// <summary>
        /// Name of the property.
        /// </summary>
        public string Name { get; set; } = name;

        /// <summary>
        /// Address of the property.
        /// </summary>
        public string Address { get; set; } = address;

        /// <summary>
        /// Price of the property.
        /// </summary>
        public decimal Price { get; set; } = price;

        /// <summary>
        /// Code internal of the property.
        /// </summary>
        public string CodeInternal { get; set; } = codeInternal;

        /// <summary>
        /// Year the property was built.
        /// </summary>
        public int Year { get; set; } = year;

        /// <summary>
        /// Id of the owner of the property.
        /// </summary>
        public int IdOwner { get; set; } = idOwner;

        /// <summary>
        /// Owner of the property.
        /// </summary>
        public Owner Owner { get; set; }

        /// <summary>
        /// Related images of the property.
        /// </summary>
        public ICollection<PropertyImage> PropertyImages { get; set; } = new List<PropertyImage>();

        /// <summary>
        /// Related traces of the property.
        /// </summary>
        public ICollection<PropertyTrace> PropertyTraces { get; set; } = new List<PropertyTrace>();
    }
}