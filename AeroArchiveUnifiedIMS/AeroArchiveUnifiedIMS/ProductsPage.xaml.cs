using System;
using System.IO;
using System.Net.Http.Headers;
using Xamarin.Forms;

namespace AeroArchiveUnifiedIMS
{
    public partial class ProductsPage : ContentPage
    {
        private static Database database;
        private Product selectedProduct;

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
                await DisplayAlert("Error", "Please enter a valid Product.", "OK");
            }
        }

        private async void ClearButtonClicked(object sender, EventArgs e)
        {
            await ProductsPage.Database.ClearProductsDatabaseAsync();
            collectionView.ItemsSource = await ProductsPage.Database.GetProductsAsync();
            await DisplayAlert("Success", "Products Database Cleared", "OK");
        }

        private async void UpdateButtonClicked(object sender, SelectionChangedEventArgs e)
        {
            if (collectionView.SelectedItem is Product selectedProduct)
            {
                string userInput = await DisplayPromptAsync("Update Product ID", "Please enter the updated product ID:", initialValue: selectedProduct.productID);
                string userWarranty = await DisplayPromptAsync("Update Warranty Status", "Please enter the updated number of days left:", initialValue: selectedProduct.warrantyStatus.ToString());

                if (!string.IsNullOrEmpty(userInput))
                {
                    selectedProduct.productID = userInput;
                    selectedProduct.warrantyStatus = int.Parse(userWarranty);

                    await ProductsPage.Database.UpdateProductAsync(selectedProduct);

                    collectionView.ItemsSource = await ProductsPage.Database.GetProductsAsync();

                    await DisplayAlert("Success", "Product updated.", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Please enter a valid product ID.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "No product selected.", "OK");
            }
        }
    }
}


