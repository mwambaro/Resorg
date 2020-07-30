using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

using Resorg.Entities;

namespace Resorg.Services
{
    public class MockTagStore : DataStore<Tag>
    {
        
        public Command MockCommand { get; set; }

        public MockTagStore(): base(null, null)
        {
            MockCommand = new Command(async () => await MockTags());
        }

        async Task<bool> MockTags()
        {
            bool val = true;

            try
            {
                List<Tag> tags = new List<Tag>
                {
                    new Tag { Id = Guid.NewGuid().ToString(), Text = "Zener Diodes", Language = "English"},
                    new Tag { Id = Guid.NewGuid().ToString(), Text = "Dichotomy method", Language = "English"},
                    new Tag { Id = Guid.NewGuid().ToString(), Text = "Compiler theories", Language = "English"},
                    new Tag { Id = Guid.NewGuid().ToString(), Text = "Schroedinger equations solutions", Language = "English"},
                    new Tag { Id = Guid.NewGuid().ToString(), Text = "Turning electromagnetic field", Language = "English"},
                    new Tag { Id = Guid.NewGuid().ToString(), Text = "Batteries chemistry", Language = "English"},
                    new Tag { Id = Guid.NewGuid().ToString(), Text = "Proverbials and idioms", Language = "English"}
                };

                Mock(tags);
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
