using System;
using System.IO;
using System.Net.Http.Headers;
using Xamarin.Forms;

namespace AeroArchiveUnifiedIMS
{
    public partial class ProductsPage : ContentPage
    {
        private static Database database;
        public static Database Database
        {
            get
            {
                if(database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "products.db3"));
                }

                return database;
            }
        }
        public ProductsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await ProductsPage.Database.GetProductsAsync();
        }

        private async void AddButtonClicked(object sender, EventArgs e)
        {
            string userInput = await DisplayPromptAsync("Enter a new Product ID", "Please enter a new product ID:");
            string userWarranty = await DisplayPromptAsync("Enter the Warranty Status", "Please enter the number of days left:");

            if (!string.IsNullOrEmpty(userInput))
            {
                await ProductsPage.Database.SaveProductAsync(new Product
                {
                    productID = userInput,
                    warrantyStatus = int.Parse(userWarranty)
                });

                collectionView.ItemsSource = await ProductsPage.Database.GetProductsAsync();

                // Show a success message or perform any other actions
                await DisplayAlert("Success", "Product added to the database.", "OK");
            }
            else
            {
                // User input is empty, show a message or handle the empty input case
                await DisplayAlert("Error", "Please enter a valid string.", "OK");
            }
        }

        private async void ClearButtonClicked(object sender, EventArgs e)
        {
            await ProductsPage.Database.ClearDatabaseAsync();
            collectionView.ItemsSource = await ProductsPage.Database.GetProductsAsync();
            await DisplayAlert("Success", "Database Cleared", "OK");
        }
    }
}


