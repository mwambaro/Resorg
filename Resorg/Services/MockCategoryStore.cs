using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

using Resorg.Entities;

namespace Resorg.Services
{
    public class MockCategoryStore : IDataStore<Category>
    {
        List<Category> categories;
        DbDataContext db;
        public Command MockCommand { get; set; }
        public MockCategoryStore()
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
                    "Power Electronics",
                    "Statistics",
                    "Microbiology",
                    "Applied Chemistry",
                    "Oil Drilling",
                    "Machine Room Installment",
                    "Electrical Schemas Design"
                };

                categories = new List<Category>
                {
                    new Category { Id = Guid.NewGuid().ToString(), Text = texts[0], Language = "English"},
                    new Category { Id = Guid.NewGuid().ToString(), Text = texts[1], Language = "English"},
                    new Category { Id = Guid.NewGuid().ToString(), Text = texts[2], Language = "English"},
                    new Category { Id = Guid.NewGuid().ToString(), Text = texts[3], Language = "English"},
                    new Category { Id = Guid.NewGuid().ToString(), Text = texts[4], Language = "English"},
                    new Category { Id = Guid.NewGuid().ToString(), Text = texts[5], Language = "English"},
                    new Category { Id = Guid.NewGuid().ToString(), Text = texts[6], Language = "English"}
                };

                db = new DbDataContext();
                db.Database.EnsureCreated();
                foreach (Category c in categories)
                {
                    await db.AddAsync(c);
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

        public async Task<bool> AddItemAsync(Category category)
        {
            bool val = true;

            try
            {
                if (null == db) MockCommand.Execute(null);
                if (null == db) throw new Exception("AddItemAsync<Category>: Database was not created");

                categories.Add(category);
                db.Categories.Add(category);
                await db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                val = false;
            }

            return await Task.FromResult(val);
        }

        public async Task<bool> UpdateItemAsync(Category category)
        {
            bool val = true;

            try
            {
                if (null == db) MockCommand.Execute(null);
                if (null == db) throw new Exception("UpdateItemAsync<Category>: Database was not created");

                var oldItem = db.Categories.Find(category.Id);
                if (null != oldItem)
                {
                    categories.Remove(oldItem);
                }
                categories.Add(category);
                db.Categories.Update(category);
                await db.SaveChangesAsync();
            }
            catch(Exception ex)
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
                if (null == db) throw new Exception("DeleteItemAsync<Category>: Database was not created");

                var oldItem = db.Categories.Find(id);
                if (null != oldItem)
                {
                    categories.Remove(oldItem);
                    db.Categories.Remove(oldItem);
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

        public async Task<Category> GetItemAsync(string id)
        {
            Category item = null;
            try
            {
                if (null == db) MockCommand.Execute(null);
                if (null == db) throw new Exception("GetItemAsync<Category>: Database was not created");

                item = await Task.FromResult(db.Categories.Find(id));
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                item = null;
            }

            return await Task.FromResult(item);
        }

        public async Task<IEnumerable<Category>> GetItemsAsync(bool forceRefresh = false)
        {
            IEnumerable<Category> _categories = null;
            try
            {
                if (null == db) MockCommand.Execute(null);
                if (null == db) throw new Exception("GetItemsAsync<Category>: Database was not created");

                _categories = await Task.FromResult(db.Categories.ToList());
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return await Task.FromResult(_categories);
        }
    }
}
