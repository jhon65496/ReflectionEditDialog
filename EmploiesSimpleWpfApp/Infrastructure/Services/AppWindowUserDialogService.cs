using System.Collections.Generic;
using System.Windows;
using EmploiesSimpleWpfApp.Data;
using EmploiesSimpleWpfApp.Infrastructure.Services.Interfaces;
using EmploiesSimpleWpfApp.Models;
using EmploiesSimpleWpfApp.ViewModels;
using EmploiesSimpleWpfApp.Views.Windows;

namespace EmploiesSimpleWpfApp.Infrastructure.Services
{
    internal class AppWindowUserDialogService : IUserDialog
    {
        private IEnumerable<Department> _Departments;

        public AppWindowUserDialogService(/* Репозиторий отделов */)
        {
            _Departments = TestData.Departments;
        }

        public bool Edit(Employee model)
        {
            var view_model = new EmployeeEditorViewModel(model, _Departments);
            var view = new EmployeeEditorWindow
            {
                DataContext = view_model,
                Owner = App.CurrentWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            view_model.Complete += (_, e) =>
            {
                view.DialogResult = e.Arg;
                view.Close();
            };

            return view.ShowDialog() ?? false;
        }
    }
}
