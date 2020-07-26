
using System.ComponentModel.DataAnnotations;

namespace Resorg.Entities
{
    public class Field
    {
        public string Text { get; set; }
        public string Language { get; set; }
        [Key]
        public string Id { get; set; }
    }
}
