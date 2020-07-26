using System;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Resorg.Entities
{
    public class Resres
    {
        public string Title { get; set; }
        public Subject Subject { get; set; }
        public Field Field { get; set; }
        public List<Category> Categories { get; set; }
        public List<Note> Notes { get; set; }
        public List<Tag> Tags { get; set; }
        public string Language { get; set; }
        [Url]
        public string Uri { get; set; }
        [Key]
        public string Id { get; set; }
    }
}
