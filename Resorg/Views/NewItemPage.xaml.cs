using System;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;

using Resorg.Models;
using Resorg.Entities;
using Resorg.Services;
using Resorg.ViewModels;

namespace Resorg.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }
        public Resres Resource { get; set; } = new Resres();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Subject> Subjects { get; set; } = new List<Subject>();
        public List<Field> Fields { get; set; } = new List<Field>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public List<Note> Notes { get; set; } = new List<Note>();

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            try
            {
                await UpdateResource(sender, e);
                MessagingCenter.Send<NewItemPage, Resres>(this, "AddItem", Resource);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            EntityStores();
        }

        async Task<bool> UpdateResource(object sender, EventArgs e)
        {
            bool val = true;

            try
            { 

            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return await Task.FromResult(val);
        }

        async void EntityStores()
        {
            try
            {
                var bs = new BaseViewModel();
                var categories = await bs.CategoryStore.GetItemsAsync();
                if(null != categories)
                {
                    Categories.Clear();
                    foreach(Category c in categories)
                    {
                        Categories.Add(c);
                    }
                }

                var subjects = await bs.SubjectStore.GetItemsAsync();
                if (null != subjects)
                {
                    Subjects.Clear();
                    foreach (Subject s in subjects)
                    {
                        Subjects.Add(s);
                    }
                }

                var fields = await bs.FieldStore.GetItemsAsync();
                if (null != fields)
                {
                    Fields.Clear();
                    foreach (Field f in fields)
                    {
                        Fields.Add(f);
                    }
                }

                var notes = await bs.NoteStore.GetItemsAsync();
                if (null != notes)
                {
                    Notes.Clear();
                    foreach (Note n in notes)
                    {
                        Notes.Add(n);
                    }
                }

                var tags = await bs.TagStore.GetItemsAsync();
                if (null != tags)
                {
                    Tags.Clear();
                    foreach (Tag t in tags)
                    {
                        Tags.Add(t);
                    }
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}