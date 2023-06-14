using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AeroArchiveUnifiedIMS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeesPage : ContentPage
    {
        private static Database database;
        private Employee selectedEmployee;
        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "employees.db3"));
                }

                return database;
            }
        }
        public EmployeesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await EmployeesPage.Database.GetEmployeeAsync();
        }

        private async void AddButtonClicked(object sender, EventArgs e)
        {
            string userName = await DisplayPromptAsync("Enter a new Employee Name", "Please enter the employee name:");
            string userRole = await DisplayPromptAsync("Enter the Employee Role", "Please enter the employee role:");
            string userID = await DisplayPromptAsync("Enter the Employee ID", "Please enter the employee ID:");
            string userAP = await DisplayPromptAsync("Enter the Employee Access Pass", "Please enter the employee acess pass:");

            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(userRole) && !string.IsNullOrEmpty(userID))
            {
                await EmployeesPage.Database.SaveEmployeeAsync(new Employee
                {
                    employeeName = userName,
                    role = userRole,
                    employeeID = int.Parse(userID),
                    accessPass = userAP
                });

                collectionView.ItemsSource = await EmployeesPage.Database.GetEmployeeAsync();

                // Show a success message or perform any other actions
                await DisplayAlert("Success", "Employee added to the database.", "OK");
            }
            else
            {
                // User input is empty, show a message or handle the empty input case
                await DisplayAlert("Error", "Please enter a valid Employee.", "OK");
            }
        }

        private async void ClearButtonClicked(object sender, EventArgs e)
        {
            await EmployeesPage.Database.ClearEmployeeDatabaseAsync();
            collectionView.ItemsSource = await EmployeesPage.Database.GetEmployeeAsync();
            await DisplayAlert("Success", "Employee Database Cleared", "OK");
        }

        private async void UpdateButtonClicked(object sender, SelectionChangedEventArgs e)
        {
            if (collectionView.SelectedItem is Employee selectedEmployee)
            {
                string userName = await DisplayPromptAsync("Update Employee Name", "Please enter the updated Employee Name:", initialValue: selectedEmployee.employeeName);
                string userRole = await DisplayPromptAsync("Update Employee Role", "Please enter the updated Employee role:", initialValue: selectedEmployee.role);
                string userID = await DisplayPromptAsync("Update Employee Name", "Please enter the updated Employee ID:", initialValue: selectedEmployee.employeeID.ToString());
                string userAP = await DisplayPromptAsync("Update Employee Access Pass", "Please enter the updated Employee access pass:", initialValue: selectedEmployee.accessPass);

                if (!string.IsNullOrEmpty(userName))
                {
                    selectedEmployee.employeeName = userName;
                    selectedEmployee.role = userRole;
                    selectedEmployee.employeeID = int.Parse(userID);
                    selectedEmployee.accessPass = userAP;

                    await EmployeesPage.Database.UpdateEmployeeAsync(selectedEmployee);

                    collectionView.ItemsSource = await EmployeesPage.Database.GetEmployeeAsync();

                    await DisplayAlert("Success", "Employee updated.", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Please enter valid Employee values.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "No Employee selected.", "OK");
            }
        }
    }
}