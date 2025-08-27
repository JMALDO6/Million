namespace Million.Domain.Entities
{
    /// <summary>
    /// Owner entity representing a property owner
    /// </summary>
    public class Owner
    {
        /// <summary>
        /// Id of the owner
        /// </summary>
        public int IdOwner { get; set; }

        /// <summary>
        /// Name of the owner
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Address of the owner
        /// </summary>
        public required string Address { get; set; }

        /// <summary>
        /// Photo of the owner
        /// </summary>
        public byte[]? Photo { get; set; }

        /// <summary>
        /// Birthday of the owner
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Relationship to the properties owned by this owner
        /// </summary>
        public required ICollection<Property> Properties { get; set; }
    }
}
