using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Resorg.Entities;

namespace Resorg.Services
{
    public class MockResresStore : DataStore<Resres>
    {
        List<Subject> subjects = new List<Subject>();
        List<Field> fields = new List<Field>();
        List<Tag> tags = new List<Tag>();
        List<Note> notes = new List<Note>();
        List<Category> categories = new List<Category>();
        List<string> titles;
        Dependency stores;
        public Command MockCommand { get; set; }

        public MockResresStore()
        {
            stores = new Dependency();
            MockCommand = new Command(async () => await MockResreses());
        }

        async Task<bool> MockResreses()
        {
            try
            {
                await LoadResresItems();

                string resresUri = "https://laaszen.org/research-resources";
                titles = new List<string>
                {
                    "Power Electronics In Industry.pdf",
                    "Organic Chemistry.epub",
                    "Optical illustion explained.mobi",
                    "Fourier transforms.pdf",
                    "Sanction of the victim and its effects.mobi",
                    "English culture during colonisation.pdf",
                    "Imaginary global division lines.epub"
                };

                int count = 0;
                var resreses = new List<Resres>();
                foreach (string t in titles)
                {
                    resreses.Add(
                        new Resres
                        {
                            Id = Guid.NewGuid().ToString(),
                            Title = t,
                            Language = "English",
                            Uri = $"{resresUri}/{t}",
                            Notes = notes.GetRange(count, 1),
                            Tags = tags.GetRange(count, 1),
                            Categories = categories.GetRange(count, 1),
                            Subject = subjects[count],
                            Field = fields[count]
                        }
                    );
                    count += 1;
                }

                //System.Diagnostics.Debug.WriteLine($"Research Resources in MEM: {resreses.Count()} [Note: {resreses[1].Notes[0].Text}]");

                Mock(resreses);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return await Task.FromResult(true);
        }

        async Task LoadResresItems()
        {

            try
            {
                if(null != subjects) subjects.Clear();
                var subjectItems = await stores.SubjectStore.GetItemsAsync(true);
                foreach (var subjectItem in subjectItems)
                {
                    subjects.Add(subjectItem);
                }

                if (null != fields) fields.Clear();
                var fieldItems = await stores.FieldStore.GetItemsAsync(true);
                foreach (var fieldItem in fieldItems)
                {
                    fields.Add(fieldItem);
                }

                if (null != notes) notes.Clear();
                var noteItems = await stores.NoteStore.GetItemsAsync(true);
                foreach (var noteItem in noteItems)
                {
                    notes.Add(noteItem);
                }

                if (null != tags) tags.Clear();
                var tagItems = await stores.TagStore.GetItemsAsync(true);
                foreach (var tagItem in tagItems)
                {
                    tags.Add(tagItem);
                }

                if (null != categories) categories.Clear();
                var categoryItems = await stores.CategoryStore.GetItemsAsync(true);
                foreach (var categoryItem in categoryItems)
                {
                    categories.Add(categoryItem);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
