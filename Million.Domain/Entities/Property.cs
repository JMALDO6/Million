namespace Million.Domain.Entities
{
    /// <summary>
    /// Property entity representing a real estate property.
    /// </summary>
    public class Property
    {
        /// <summary>
        /// Id of the property.
        /// </summary>
        public int IdProperty { get; set; }

        /// <summary>
        /// Name of the property.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Address of the property.
        /// </summary>
        public required string Address { get; set; }

        /// <summary>
        /// Price of the property.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Code internal of the property.
        /// </summary>
        public required string CodeInternal { get; set; }

        /// <summary>
        /// Year the property was built.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Id of the owner of the property.
        /// </summary>
        public int IdOwner { get; set; }

        /// <summary>
        /// Owner of the property.
        /// </summary>
        public required Owner Owner { get; set; }

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