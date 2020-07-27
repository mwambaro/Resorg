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
    public class MockResresStore : Resorg.Services.Dependency, IDataStore<Resres>
    {
        List<Resres> resreses;
        DbDataContext db;
        List<Subject> subjects = new List<Subject>();
        List<Field> fields = new List<Field>();
        List<Tag> tags = new List<Tag>();
        List<Note> notes = new List<Note>();
        List<Category> categories = new List<Category>();
        List<string> titles;

        public Command LoadResresItemsCommand { get; set; }
        public Command MockCommand { get; set; }

        public MockResresStore(): base(false)
        {
            MockCommand = new Command(async () => await Mock());
        }

        async Task<bool> Mock()
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
                resreses = new List<Resres>();
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

                db = new DbDataContext();
                db.Database.EnsureCreated();
                db.Resreses.AddRange(resreses);

                System.Diagnostics.Debug.WriteLine($"Research Resources in MEM: {resreses.Count()} [Note: {resreses[1].Notes[0].Text}]");

                db.SaveChanges();

                System.Diagnostics.Debug.WriteLine($"Research Resources in DB: {db.Resreses.Count()}");
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> AddItemAsync(Resres resres)
        {
            bool val = true;

            try
            {
                if (null == db) MockCommand.Execute(null);
                if (null == db) throw new Exception("AddItemAsync<Resres>: Database was not created");

                resreses.Add(resres);
                await db.Resreses.AddAsync(resres);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                val = false;
            }

            return await Task.FromResult(val);
        }

        public async Task<bool> UpdateItemAsync(Resres resres)
        {
            bool val = true;

            try
            {
                if(null == resres)
                {
                    val = false;
                    return await Task.FromResult(val);
                }

                if (null == db) MockCommand.Execute(null);
                if (null == db) throw new Exception("UpdateItemAsync<Resres>: Database was not created");

                var oldItem = db.Resreses.Find(resres.Id);
                if (null == oldItem)
                {
                    oldItem = resreses.Where((Resres arg) => arg.Id == resres.Id).FirstOrDefault();
                }

                if (null != oldItem)
                {
                    resreses.Remove(oldItem);
                }
                resreses.Add(resres);
                db.Resreses.Update(resres);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                val = false;
            }

            return await Task.FromResult(val);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            bool val = true;

            try
            {
                if (null == db) MockCommand.Execute(null);
                if (null == db) throw new Exception("DeleteItemAsync<Resres>: Database was not created");

                var oldItem = db.Resreses.Find(id);
                if (null == oldItem)
                {
                    oldItem = resreses.Where((Resres arg) => arg.Id == id).FirstOrDefault();
                }

                if (null != oldItem)
                {
                    resreses.Remove(oldItem);
                    db.Resreses.Remove(oldItem);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                val = false;
            }

            return await Task.FromResult(val);
        }

        public async Task<Resres> GetItemAsync(string id)
        {
            Resres item = null;
            try
            {
                if (null == db) MockCommand.Execute(null);
                if (null == db) throw new Exception("GetItemAsync<Resres>: Database was not created");

                item = await Task.FromResult(db.Resreses.Find(id));
                if(null == item)
                {
                    item = await Task.FromResult(resreses.FirstOrDefault(s => s.Id == id));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                item = null;
            }

            return await Task.FromResult(item);
        }

        public async Task<IEnumerable<Resres>> GetItemsAsync(bool forceRefresh = false)
        {
            IEnumerable<Resres> _resreses = null;
            try
            {
                if (null == db) MockCommand.Execute(null);
                if (null == db) throw new Exception("GetItemsAsync<Resres>: Database was not created");

                _resreses = await Task.FromResult(db.Resreses.ToList());
                if (null == _resreses || _resreses?.Count() == 0)
                {
                    _resreses = await Task.FromResult(resreses);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return await Task.FromResult(_resreses);
        }

        async Task LoadResresItems()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                if(null != subjects) subjects.Clear();
                var subjectItems = await SubjectStore.GetItemsAsync(true);
                foreach (var subjectItem in subjectItems)
                {
                    subjects.Add(subjectItem);
                }

                if (null != fields) fields.Clear();
                var fieldItems = await FieldStore.GetItemsAsync(true);
                foreach (var fieldItem in fieldItems)
                {
                    fields.Add(fieldItem);
                }

                if (null != notes) notes.Clear();
                var noteItems = await NoteStore.GetItemsAsync(true);
                foreach (var noteItem in noteItems)
                {
                    notes.Add(noteItem);
                }

                if (null != tags) tags.Clear();
                var tagItems = await TagStore.GetItemsAsync(true);
                foreach (var tagItem in tagItems)
                {
                    tags.Add(tagItem);
                }

                if (null != categories) categories.Clear();
                var categoryItems = await CategoryStore.GetItemsAsync(true);
                foreach (var categoryItem in categoryItems)
                {
                    categories.Add(categoryItem);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
