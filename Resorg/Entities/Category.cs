using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace Resorg.Entities
{
    public class Category
    {
        public string Text { get; set; }
        public string Language { get; set; }
        [Key]
        public string Id { get; set; }
    }
}
