using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

using Resorg.Entities;

namespace Resorg.Services
{
    public class MockFieldStore : DataStore<Field>
    {
        public Command MockCommand { get; set; }

        public MockFieldStore(): base(null, null)
        {
            MockCommand = new Command(async () => await MockFields());
        }

        async Task<bool> MockFields()
        {
            try
            {
                var fields = new List<Field>
                {
                    new Field { Id = Guid.NewGuid().ToString(), Text = "Electronics", Language = "English"},
                    new Field { Id = Guid.NewGuid().ToString(), Text = "Mechanics", Language = "English"},
                    new Field { Id = Guid.NewGuid().ToString(), Text = "Electromechanics", Language = "English"},
                    new Field { Id = Guid.NewGuid().ToString(), Text = "Computer Sciences", Language = "English"},
                    new Field { Id = Guid.NewGuid().ToString(), Text = "Electrotechnics", Language = "English"},
                    new Field { Id = Guid.NewGuid().ToString(), Text = "Petrochemistry", Language = "English"},
                    new Field { Id = Guid.NewGuid().ToString(), Text = "French Literature", Language = "English"}
                };

                Mock(fields);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return await Task.FromResult(true);
        }
    }
}
