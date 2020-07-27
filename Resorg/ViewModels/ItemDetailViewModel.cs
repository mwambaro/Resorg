using System;

using Resorg.Models;
using Resorg.Entities;

namespace Resorg.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Resres Item { get; set; }
        public ItemDetailViewModel(Resres item = null)
        {
            Title = item?.Title;
            Item = item;
        }
    }
}
