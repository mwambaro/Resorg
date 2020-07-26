using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Resorg.Entities;
using Resorg.Views;

namespace Resorg.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Resres> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel(): base()
        {
            Title = "Browse";
            Items = new ObservableCollection<Resres>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Resres>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Resres;
                Items.Add(newItem);
                await ResresStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await ResresStore.GetItemsAsync(true);
                if (null != items)
                {
                    foreach (var item in items)
                    {
                        Items.Add(item);
                    }
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