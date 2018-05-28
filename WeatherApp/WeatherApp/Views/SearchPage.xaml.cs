using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using WeatherApp.Models;
using WeatherApp.ViewModels;

namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        private SearchViewModel Item;

        public SearchPage()
        {
            InitializeComponent();

            BindingContext = Item = new SearchViewModel();
        }

        private async void SearchWeatherBtn_Clicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(zipCodeEntry.Text))
            {
                await Item.SearchForLocation(zipCodeEntry.Text);
                SerchResultLabel.Text = "";
                getLocationBtn.Text = "Search Again";

                if (Item.LocationInfos.Count == 0)
                {
                    SerchResultLabel.Text = "There is no results for searched code";
                }
            }
        }

        private async void OnLocationSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is LocationInfo item))
                return;

            MessagingCenter.Send(this, "AddItem", item);

            await Navigation.PopModalAsync();

            SerchedItemsListView.SelectedItem = null;
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}