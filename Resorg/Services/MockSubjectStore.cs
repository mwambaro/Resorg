using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

using Resorg.Entities;

namespace Resorg.Services
{
    public class MockSubjectStore : IDataStore<Subject>
    {
        List<Subject> subjects;
        DbDataContext db;
        public Command MockCommand { get; set; }

        public MockSubjectStore()
        {
            MockCommand = new Command(async () => await Mock());
        }

        async Task<bool> Mock()
        {
            bool val = true;

            try
            {
                subjects = new List<Subject>()
                {
                    new Subject{ Id = Guid.NewGuid().ToString(), Text = "Physics", Language = "English"},
                    new Subject{ Id = Guid.NewGuid().ToString(), Text = "Chemistry", Language = "English"},
                    new Subject{ Id = Guid.NewGuid().ToString(), Text = "Biology", Language = "English"},
                    new Subject{ Id = Guid.NewGuid().ToString(), Text = "Mathematics", Language = "English"},
                    new Subject{ Id = Guid.NewGuid().ToString(), Text = "Philosophy", Language = "English"},
                    new Subject{ Id = Guid.NewGuid().ToString(), Text = "Literature", Language = "English"},
                    new Subject{ Id = Guid.NewGuid().ToString(), Text = "Geography", Language = "English"}
                };

                db = new DbDataContext();
                db.Database.EnsureCreated();
                foreach (Subject s in subjects)
                {
                    await db.AddAsync(s);
                }
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                val = false;
            }

            return await Task.FromResult(val);
        }

        public async Task<bool> AddItemAsync(Subject category)
        {
            bool val = true;

            try
            {
                if (null == db) MockCommand.Execute(null);
                if (null == db) throw new Exception("AddItemAsync<Subject>: Database was not created");

                subjects.Add(category);
                await db.Subjects.AddAsync(category);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                val = false;
            }

            return await Task.FromResult(val);
        }

        public async Task<bool> UpdateItemAsync(Subject category)
        {
            bool val = true;

            try
            {
                if (null == db) MockCommand.Execute(null);
                if (null == db) throw new Exception("UpdateItemAsync<Subject>: Database was not created");

                var oldItem = db.Subjects.Find(category.Id);
                if (null != oldItem)
                {
                    subjects.Remove(oldItem);
                }
                subjects.Add(category);
                db.Subjects.Update(category);
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
                if (null == db) throw new Exception("DeleteItemAsync<Subject>: Database was not created");

                var oldItem = db.Subjects.Find(id);
                if (null != oldItem)
                {
                    subjects.Remove(oldItem);
                    db.Subjects.Remove(oldItem);
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

        public async Task<Subject> GetItemAsync(string id)
        {
            Subject item;
            
            try
            {
                if (null == db) MockCommand.Execute(null);
                if (null == db) throw new Exception("GetItemAsync<Subject>: Database was not created");

                item = await Task.FromResult(db.Subjects.Find(id));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                item = null;
            }

            return await Task.FromResult(item);
        }

        public async Task<IEnumerable<Subject>> GetItemsAsync(bool forceRefresh = false)
        {
            IEnumerable<Subject> _subjects = null;
            try
            {
                if (null == db) MockCommand.Execute(null);
                if (null == db) throw new Exception("GetItemsAsync<Subject>: Database was not created");

                _subjects = await Task.FromResult(db.Subjects.ToList());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return await Task.FromResult(_subjects);
        }
    }
}
