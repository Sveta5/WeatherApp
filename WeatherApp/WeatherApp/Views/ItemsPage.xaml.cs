using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApp.Models;
using WeatherApp.Views;
using WeatherApp.ViewModels;

namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }
        
        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new SearchPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ItemsListView.SelectedItem = null;

            if (viewModel.LocationInfos.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        private void LocationDelete_Clicked(object sender, EventArgs eventArgs)
        {
            if (ItemsListView.SelectedItem is LocationInfo selectedItem) viewModel.DeleteItemsCommand.Execute(selectedItem);
            ItemsListView.SelectedItem = null;
        }

        private void UpdateWeather_Clicked(object sender, EventArgs e)
        {
            ItemsListView.SelectedItem = null;
            viewModel.LoadItemsCommand.Execute(null);
        }
    }
}