using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

using Resorg.Entities;

namespace Resorg.Services
{
    public class MockSubjectStore : DataStore<Subject>
    {
        public Command MockCommand { get; set; }

        public MockSubjectStore(): base(null, null)
        {
            MockCommand = new Command(async () => await MockSubjects());
        }

        async Task<bool> MockSubjects()
        {
            bool val = true;

            try
            {
                var subjects = new List<Subject>()
                {
                    new Subject{ Id = Guid.NewGuid().ToString(), Text = "Physics", Language = "English"},
                    new Subject{ Id = Guid.NewGuid().ToString(), Text = "Chemistry", Language = "English"},
                    new Subject{ Id = Guid.NewGuid().ToString(), Text = "Biology", Language = "English"},
                    new Subject{ Id = Guid.NewGuid().ToString(), Text = "Mathematics", Language = "English"},
                    new Subject{ Id = Guid.NewGuid().ToString(), Text = "Philosophy", Language = "English"},
                    new Subject{ Id = Guid.NewGuid().ToString(), Text = "Literature", Language = "English"},
                    new Subject{ Id = Guid.NewGuid().ToString(), Text = "Geography", Language = "English"}
                };

                Mock(subjects);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                val = false;
            }

            return await Task.FromResult(val);
        }
    }
}
