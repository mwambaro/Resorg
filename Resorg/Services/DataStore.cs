using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

using Resorg.Entities;
using Resorg.Models;

namespace Resorg.Services
{
    public class DataStore<T> : IDataStore<T>
    {

        List<T> items;
        DbDataContext db;

        public DataStore()
        {
            items = new List<T>();
            db = new DbDataContext();
            db.Database.EnsureCreated();
        }

        public async void Mock(List<T> _items)
        {
            try
            {
                T item = default;
                if (null == db) throw new Exception($"Mock<{item.GetType().Name}>: Database was not created");

                await db.AddRangeAsync(_items);
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
                if (null == db) throw new Exception($"AddItemAsync<{item.GetType().Name}>: Database was not created");

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
                if (null == db) throw new Exception($"UpdateItemAsync<{item.GetType().Name}>: Database was not created");

                string id = (string)item.GetType().GetProperty("Id").GetValue(item);
                T oldItem = (T)db.Find(item.GetType(), id);
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
                T item = default;
                if (null == db) throw new Exception($"DeleteItemAsync<{item.GetType()}>: Database was not created");

                T oldItem = (T)db.Find(item.GetType(), id);
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
                if (null == db) throw new Exception($"GetItemAsync<{item.GetType()}>: Database was not created");

                T oldItem = (T)db.Find(item.GetType(), id);
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
            IEnumerable<T> _items = null;
            T item = default;

            try
            {
                if (null == db) throw new Exception($"GetItemsAsync<{item.GetType()}>: Database was not created");

                // TODO: Pluralize class name conveniently
                string _class = $"{item.GetType().Name}s"; 
                IEnumerable<T> _dbItems = ((IEnumerable<T>)db.GetType().GetProperty(_class).GetValue(db)).ToList();
                _items = await Task.FromResult(_dbItems);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return await Task.FromResult(_items);
        }
    }
}
