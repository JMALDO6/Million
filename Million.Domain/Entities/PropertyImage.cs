namespace Million.Domain.Entities
{
    public class PropertyImage
    {
        public int IdPropertyImage { get; set; }
        public string File { get; set; }
        public string Enabled { get; set; }
        public int IdProperty { get; set; }
        public Property Property { get; set; }
    }
}
