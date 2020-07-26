using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

using Resorg.Entities;

namespace Resorg.Services
{
    public class MockTagStore : IDataStore<Tag>
    {
        List<Tag> tags;
        DbDataContext db;
        public Command MockCommand { get; set; }

        public MockTagStore()
        {
            MockCommand = new Command(async () => await Mock());
        }

        async Task<bool> Mock()
        {
            bool val = true;

            try
            {
                tags = new List<Tag>
                {
                    new Tag { Id = Guid.NewGuid().ToString(), Text = "Zener Diodes", Language = "English"},
                    new Tag { Id = Guid.NewGuid().ToString(), Text = "Dichotomy method", Language = "English"},
                    new Tag { Id = Guid.NewGuid().ToString(), Text = "Compiler theories", Language = "English"},
                    new Tag { Id = Guid.NewGuid().ToString(), Text = "Schroedinger equations solutions", Language = "English"},
                    new Tag { Id = Guid.NewGuid().ToString(), Text = "Turning electromagnetic field", Language = "English"},
                    new Tag { Id = Guid.NewGuid().ToString(), Text = "Batteries chemistry", Language = "English"},
                    new Tag { Id = Guid.NewGuid().ToString(), Text = "Proverbials and idioms", Language = "English"}
                };

                db = new DbDataContext();
                db.Database.EnsureCreated();
                foreach (Tag t in tags)
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

        public async Task<bool> AddItemAsync(Tag category)
        {
            bool val = true;

            try
            {
                if (null == db) MockCommand.Execute(null);
                if (null == db) throw new Exception("AddItemAsync<Tag>: Database was not created");

                tags.Add(category);
                db.Tags.Add(category);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                val = false;
            }

            return await Task.FromResult(val);
        }

        public async Task<bool> UpdateItemAsync(Tag category)
        {
            bool val = true;

            try
            {
                if (null == db) MockCommand.Execute(null);
                if (null == db) throw new Exception("UpdateItemAsync<Tag>: Database was not created");

                var oldItem = db.Tags.Find(category.Id);
                if (null != oldItem)
                {
                    tags.Remove(oldItem);
                }
                tags.Add(category);
                db.Tags.Update(category);
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
                if (null == db) throw new Exception("DeleteItemAsync<Tag>: Database was not created");

                var oldItem = db.Tags.Find(id);
                if (null != oldItem)
                {
                    tags.Remove(oldItem);
                    db.Tags.Remove(oldItem);
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

        public async Task<Tag> GetItemAsync(string id)
        {
            Tag item = null;
            try
            {
                if (null == db) MockCommand.Execute(null);
                if (null == db) throw new Exception("GetItemAsync<Tag>: Database was not created");

                item = await Task.FromResult(db.Tags.Find(id));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                item = null;
            }

            return await Task.FromResult(item);
        }

        public async Task<IEnumerable<Tag>> GetItemsAsync(bool forceRefresh = false)
        {
            IEnumerable<Tag> _tags = null;
            try
            {
                if (null == db) MockCommand.Execute(null);
                if (null == db) throw new Exception("GetItemsAsync<Tag>: Database was not created");

                _tags = await Task.FromResult(db.Tags.ToList());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return await Task.FromResult(_tags);
        }
    }
}
