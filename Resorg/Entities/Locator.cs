using System.ComponentModel.DataAnnotations;

namespace Resorg.Entities
{
    public class Locator
    {
        public int PageNumber { get; set; }
        public int ParagraphNumber { get; set; }
        public string Section { get; set; }
        [Key]
        public string Id { get; set; }
    }
}
