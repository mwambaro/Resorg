using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Resorg.Models;

namespace Resorg.Entities
{
    public class Note
    {
        public string Text { get; set; }

        public Locator Location { get; set; }

        public CultureInfo Culture { get; set; }
    }
}
