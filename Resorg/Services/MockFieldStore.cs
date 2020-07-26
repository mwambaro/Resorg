using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

using Resorg.Entities;

namespace Resorg.Services
{
    public class MockFieldStore : IDataStore<Field>
    {
        List<Field> fields;
        DbDataContext db;
        public Command MockCommand { get; set; }

        public MockFieldStore()
        {
            MockCommand = new Command(async () => await Mock());
        }

        async Task<bool> Mock()
        {
            try
            {
                fields = new List<Field>
                {
                    new Field { Id = Guid.NewGuid().ToString(), Text = "Electronics", Language = "English"},
                    new Field { Id = Guid.NewGuid().ToString(), Text = "Mechanics", Language = "English"},
                    new Field { Id = Guid.NewGuid().ToString(), Text = "Electromechanics", Language = "English"},
                    new Field { Id = Guid.NewGuid().ToString(), Text = "Computer Sciences", Language = "English"},
                    new Field { Id = Guid.NewGuid().ToString(), Text = "Electrotechnics", Language = "English"},
                    new Field { Id = Guid.NewGuid().ToString(), Text = "Petrochemistry", Language = "English"},
                    new Field { Id = Guid.NewGuid().ToString(), Text = "French Literature", Language = "English"}
                };

                db = new DbDataContext();
                db.Database.EnsureCreated();
                foreach (Field f in fields)
                {
                    await db.AddAsync(f);
                }
                await db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> AddItemAsync(Field field)
        {
            bool val = true;

            try
            {
                if (null == db) MockCommand.Execute(null);
                if (null == db) throw new Exception("AddItemAsync<Field>: Database was not created");

                fields.Add(field);
                db.Fields.Add(field);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                val = false;
            }

            return await Task.FromResult(val);
        }

        public async Task<bool> UpdateItemAsync(Field field)
        {
            bool val = true;

            try
            {
                if (null == db) MockCommand.Execute(null);
                if (null == db) throw new Exception("UpdateItemAsync<Field>: Database was not created");

                var oldItem = db.Fields.Find(field.Id);
                if (null != oldItem)
                {
                    fields.Remove(oldItem);
                }
                fields.Add(field);
                db.Fields.Update(field);
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
                if (null == db) throw new Exception("DeleteItemAsync<Field>: Database was not created");

                var oldItem = db.Fields.Find(id);
                if (null != oldItem)
                {
                    fields.Remove(oldItem);
                    db.Fields.Remove(oldItem);
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

        public async Task<Field> GetItemAsync(string id)
        {
            Field item = null;
            try
            {
                if (null == db) MockCommand.Execute(null);
                if (null == db) throw new Exception("GetItemAsync<Field>: Database was not created");

                item = await Task.FromResult(db.Fields.Find(id));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                item = null;
            }

            return await Task.FromResult(item);
        }

        public async Task<IEnumerable<Field>> GetItemsAsync(bool forceRefresh = false)
        {
            IEnumerable<Field> _fields = null;
            try
            {
                if (null == db) MockCommand.Execute(null);
                if (null == db) throw new Exception("GetItemsAsync<Field>: Database was not created");

                _fields = await Task.FromResult(db.Fields.ToList());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return await Task.FromResult(_fields);
        }
    }
}
