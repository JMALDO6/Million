namespace Million.Domain.Entities
{
    /// <summary>
    /// PropertyImage entity representing an image associated with a property.
    /// </summary>
    public class PropertyImage
    {
        /// <summary>
        /// Identifier for the PropertyImage.
        /// </summary>
        public int IdPropertyImage { get; set; }

        /// <summary>
        /// File data of the image.
        /// </summary>
        public byte[] File { get; set; }

        /// <summary>
        /// Status indicating if the image is enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Identifier for the associated Property.
        /// </summary>
        public int IdProperty { get; set; }

        /// <summary>
        /// Reference to the associated Property entity.
        /// </summary>
        public Property Property { get; set; }
    }
}
