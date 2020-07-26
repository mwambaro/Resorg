using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

using Resorg.Entities;

namespace Resorg.Services
{
    public class MockNoteStore : IDataStore<Note>
    {
        List<Note> notes;
        DbDataContext db;
        public Command MockCommand { get; set; }

        public MockNoteStore ()
        {
            MockCommand = new Command(async () => await Mock());
        }

        async Task<bool> Mock()
        {
            bool val = true;

            try
            {
                var texts = new List<string>
                {
                    "Management of global resources is the key to achieving world peace.",
                    "How can we convince tyranic governments to restructure and unite?",
                    "Finances related issues can then be solved once and for all.",
                    "They all are patterned after Structured sSystems Analysis and Design Method (SSADM).",
                    "Accepting to share proprietary knowledge related to resources exploitation is a huge challenge.",
                    "The resources are not evenly and uniformly distributed around the globe.",
                    "On this battlefield we live or die until our goals are achieved!"
                };

                notes = new List<Note>
                {
                    new Note { Id = Guid.NewGuid().ToString(), Text = texts[0], Language = "English", Location = new Locator { Id = Guid.NewGuid().ToString(), PageNumber = 23, ParagraphNumber = 5, Section = "Introduction"} },
                    new Note { Id = Guid.NewGuid().ToString(), Text = texts[1], Language = "English", Location = new Locator { Id = Guid.NewGuid().ToString(), PageNumber = 133, ParagraphNumber = 2, Section = "Frequently Asked Questions"} },
                    new Note { Id = Guid.NewGuid().ToString(), Text = texts[2], Language = "English", Location = new Locator { Id = Guid.NewGuid().ToString(), PageNumber = 250, ParagraphNumber = 1, Section = "Applications"} },
                    new Note { Id = Guid.NewGuid().ToString(), Text = texts[3], Language = "English", Location = new Locator { Id = Guid.NewGuid().ToString(), PageNumber = 80, ParagraphNumber = 6, Section = "Practical Diagrams"} },
                    new Note { Id = Guid.NewGuid().ToString(), Text = texts[4], Language = "English", Location = new Locator { Id = Guid.NewGuid().ToString(), PageNumber = 300, ParagraphNumber = 3, Section = "Managing Resources"} },
                    new Note { Id = Guid.NewGuid().ToString(), Text = texts[5], Language = "English", Location = new Locator { Id = Guid.NewGuid().ToString(), PageNumber = 150, ParagraphNumber = 4, Section = "ArcGIS Map"} },
                    new Note { Id = Guid.NewGuid().ToString(), Text = texts[6], Language = "English", Location = new Locator { Id = Guid.NewGuid().ToString(), PageNumber = 240, ParagraphNumber = 7, Section = "The Resolution"} }
                };

                db = new DbDataContext();
                db.Database.EnsureCreated();
                foreach (Note t in notes)
                {
                    await db.AddAsync(t);
                }
                await db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                val = false;
            }

            return await Task.FromResult(val);
        }

        public async Task<bool> AddItemAsync(Note category)
        {
            bool val = true;

            try
            {
                if (null == db) MockCommand.Execute(null);
                if (null == db) throw new Exception("AddItemAsync<Note>: Database was not created");

                notes.Add(category);
                db.Notes.Add(category);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                val = false;
            }

            return await Task.FromResult(val);
        }

        public async Task<bool> UpdateItemAsync(Note category)
        {
            bool val = true;

            try
            {
                if (null == db) MockCommand.Execute(null);
                if (null == db) throw new Exception("UpdateItemAsync<Note>: Database was not created");

                var oldItem = db.Notes.Find(category.Id);
                if (null != oldItem)
                {
                    notes.Remove(oldItem);
                }
                notes.Add(category);
                db.Notes.Update(category);
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
                if (null == db) throw new Exception("DeleteItemAsync<Note>: Database was not created");

                var oldItem = db.Notes.Find(id);
                if (null != oldItem)
                {
                    notes.Remove(oldItem);
                    db.Notes.Remove(oldItem);
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

        public async Task<Note> GetItemAsync(string id)
        {
            Note item = null;
            try
            {
                if (null == db) MockCommand.Execute(null);
                if (null == db) throw new Exception("GetItemAsync<Note>: Database was not created");

                item = await Task.FromResult(db.Notes.Find(id));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                item = null;
            }

            return await Task.FromResult(item);
        }

        public async Task<IEnumerable<Note>> GetItemsAsync(bool forceRefresh = false)
        {
            IEnumerable<Note> _notes = null;
            try
            {
                if (null == db) MockCommand.Execute(null);
                if (null == db) throw new Exception("GetItemsAsync<Note>: Database was not created");

                _notes = await Task.FromResult(db.Notes.ToList());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return await Task.FromResult(_notes);
        }
    }
}
