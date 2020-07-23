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
        public string Subject { get; set; }
        public string Field { get; set; }
        public List<string> Categories { get; set; }
        public List<Note> Notes { get; set; }
        public List<string> Tags { get; set; }
        public CultureInfo Culture { get; set; }
        public string Uri { get; set; }
    }
}
