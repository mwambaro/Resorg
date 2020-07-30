using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

using Resorg.Entities;

namespace Resorg.Services
{
    public class MockCategoryStore : DataStore<Category>
    {
        public Command MockCommand { get; set; }
        public MockCategoryStore()
        {
            MockCommand = new Command(async () => await MockCategories());
        }

        async Task<bool> MockCategories()
        {
            bool val = true;

            try
            {
                var texts = new List<string>
                {
                    "Power Electronics",
                    "Statistics",
                    "Microbiology",
                    "Applied Chemistry",
                    "Oil Drilling",
                    "Machine Room Installment",
                    "Electrical Schemas Design"
                };

                var categories = new List<Category>
                {
                    new Category { Id = Guid.NewGuid().ToString(), Text = texts[0], Language = "English"},
                    new Category { Id = Guid.NewGuid().ToString(), Text = texts[1], Language = "English"},
                    new Category { Id = Guid.NewGuid().ToString(), Text = texts[2], Language = "English"},
                    new Category { Id = Guid.NewGuid().ToString(), Text = texts[3], Language = "English"},
                    new Category { Id = Guid.NewGuid().ToString(), Text = texts[4], Language = "English"},
                    new Category { Id = Guid.NewGuid().ToString(), Text = texts[5], Language = "English"},
                    new Category { Id = Guid.NewGuid().ToString(), Text = texts[6], Language = "English"}
                };

                Mock(categories);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                val = false;
            }

            return await Task.FromResult(val);
        }
    }
}
