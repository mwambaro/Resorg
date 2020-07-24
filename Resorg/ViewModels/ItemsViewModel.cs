using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Resorg.Models;
using Resorg.Views;

namespace Resorg.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public List<string> Categories { get; set; } = new List<string>();
        public List<string> Subjects { get; set; } = new List<string>();
        public List<string> Fields { get; set; } = new List<string>();
        public string ResourceUri { get; set; }
        public string ResourceTitle { get; set; }
        public string ResourceTags { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            
            Categories.Add("Business and Management/Leadership");
            Categories.Add("Education");
            Categories.Add("Enterprise Management");
            Categories.Add("Humanities/Philosophy");

            Subjects.Add("Physics");
            Subjects.Add("Chemistry");
            Subjects.Add("Biology");
            Subjects.Add("Mathematics");

            Fields.Add("Electronics");
            Fields.Add("Mechanics");
            Fields.Add("Electrotechniques");
            Fields.Add("Computer Sciences");

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
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
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
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