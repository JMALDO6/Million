namespace Million.Domain.Entities
{
    /// <summary>
    /// PropertyTrace entity representing the trace of a property
    /// </summary>
    public class PropertyTrace
    {
        /// <summary>
        /// Id of the property trace
        /// </summary>
        public int IdPropertyTrace { get; set; }

        /// <summary>
        /// Date of the sale
        /// </summary>
        public DateTime DateSale { get; set; }

        /// <summary>
        /// Name of the property trace
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value of the property trace
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Tax of the property trace
        /// </summary>
        public double Tax { get; set; }

        /// <summary>
        /// Id of the related property
        /// </summary>
        public int IdProperty { get; set; }

        /// <summary>
        /// Property associated with this trace
        /// </summary>
        public Property Property { get; set; }
    }
}
