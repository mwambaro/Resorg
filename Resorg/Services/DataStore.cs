using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Xamarin.Forms;

using Pluralize.NET;

using Resorg.Entities;
using Resorg.Models;

namespace Resorg.Services
{
    public class DataStore<T> : IDataStore<T>
    {

        List<T> items;
        DbDataContext db;

        public DataStore(string dbName=null, DbDataContext dbs=null)
        {
            items = new List<T>();
            if (null == dbs)
            {
                db = new DbDataContext(dbName);
            }
            else
            {
                db = dbs;
            }
        }

        public async void Mock(List<T> _items)
        {
            try
            {
                items = _items;
                T item = default;
                if (null == db) throw new Exception($"Mock<{item.GetType().Name}>: Database was not created");
                db.Database.EnsureCreated();

                foreach (T i in _items)
                {
                    db.Add(i);
                    await Task.Delay(200);
                }
                await db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public async Task<bool> AddItemAsync(T item)
        {
            bool val = true;

            try
            {
                if(null == item)
                {
                    throw new Exception($"AddItemAsync<{typeof(T).Name}>: null reference param");
                }
                if (null == db)
                {
                    throw new Exception($"AddItemAsync<{typeof(T).Name}>: Database was not created");
                }
                db.Database.EnsureCreated();

                items.Add(item);
                db.Add(item);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                val = false;
            }

            return await Task.FromResult(val);
        }

        public async Task<bool> UpdateItemAsync(T item)
        {
            bool val = true;

            try
            {
                if(null == item)
                {
                    throw new Exception($"UpdateItemAsync<{typeof(T).Name}>: Null reference param");
                }

                if (null == db)
                {
                    throw new Exception($"UpdateItemAsync<{typeof(T).Name}>: Database was not created");
                }
                db.Database.EnsureCreated();

                string id = (string)item.GetType().GetProperty("Id").GetValue(item);
                T oldItem = items.FirstOrDefault(s => (string)typeof(T).GetProperty("Id").GetValue(s) == id);

                if (null == oldItem)
                {
                    oldItem = (T)db.Find(typeof(T), id);
                }
                if (null != oldItem)
                {
                    items.Remove(oldItem);
                }

                items.Add(item);
                db.Update(item);
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
                if (null == db)
                {
                    throw new Exception($"DeleteItemAsync<{typeof(T).Name}>: Database was not created");
                }
                db.Database.EnsureCreated();

                T oldItem = items.FirstOrDefault(s => (string)typeof(T).GetProperty("Id").GetValue(s) == id);

                if (null == oldItem)
                {
                    oldItem = (T)db.Find(typeof(T), id);
                }
                if (null != oldItem)
                {
                    items.Remove(oldItem);
                    db.Remove(oldItem);
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

        public async Task<T> GetItemAsync(string id)
        {
            T item = default;
            
            try
            {
                if (null == db)
                {
                    throw new Exception($"GetItemAsync<{typeof(T).Name}>: Database was not created");
                }
                db.Database.EnsureCreated();

                T oldItem = items.FirstOrDefault(s => (string)typeof(T).GetProperty("Id").GetValue(s) == id);

                if(null == oldItem) oldItem = (T)db.Find(item.GetType(), id);
                item = await Task.FromResult(oldItem);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                item = default;
            }

            return await Task.FromResult(item);
        }

        public async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            IEnumerable<T> _items = items;

            try
            {
                if (null == db)
                {
                    throw new Exception($"GetItemsAsync<{typeof(T).Name}>: Database was not created");
                }
                db.Database.EnsureCreated();

                // TODO: Pluralize class name conveniently
                IPluralize plural = new Pluralizer();
                string _class = plural.Pluralize($"{typeof(T).Name}");
                var propertyInfo = db.GetType().GetProperty(_class);

                IEnumerable<T> _dbItems = null;
                if (null != propertyInfo)
                {
                    IEnumerable<T> dbObject = (IEnumerable<T>)propertyInfo.GetValue(db);
                    if (null != dbObject)
                    {
                        _dbItems = dbObject.ToList();
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"Db: {_class} object is null reference");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Db: {_class} not found as public member of database");
                }
                _items = null == _dbItems ? items : await Task.FromResult(_dbItems);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return await Task.FromResult(_items);
        }
    }
}
