﻿using System.Collections.ObjectModel;
using System.Windows.Input;

using EmploiesSimpleWpfApp.Data;
using EmploiesSimpleWpfApp.Infrastructure.Commands;
using EmploiesSimpleWpfApp.Infrastructure.Services.Interfaces;
using EmploiesSimpleWpfApp.Models;
using EmploiesSimpleWpfApp.ViewModels.Base;

namespace EmploiesSimpleWpfApp.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly IUserDialog _UserDialog;

        #region Title : string - Заголовок окна

        /// <summary>Заголовок окна</summary>
        private string _Title = "Заголовок окна";

        /// <summary>Заголовок окна</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion


        private ObservableCollection<Department> _Departments;

        public ObservableCollection<Department> Departments { get => _Departments; set => Set(ref _Departments, value); }

        #region SelectedDepartment : Department - Выбранный отдел

        /// <summary>Выбранный отдел</summary>
        private Department _SelectedDepartment;

        /// <summary>Выбранный отдел</summary>
        public Department SelectedDepartment { get => _SelectedDepartment; set => Set(ref _SelectedDepartment, value); }

        #endregion

        #region SelectedEmployee : Employee - Выбранный сотрудник

        /// <summary>Выбранный сотрудник</summary>
        private Employee _SelectedEmployee;

        /// <summary>Выбранный сотрудник</summary>
        public Employee SelectedEmployee { get => _SelectedEmployee; set => Set(ref _SelectedEmployee, value); }

        #endregion


        #region LoadDataCommand

        private ICommand _LoadDataCommand;

        public ICommand LoadDataCommand => _LoadDataCommand
            ??= new LambdaCommand(OnLoadDataCommandExecuted, CanLoadDataCommandExecute);

        private bool CanLoadDataCommandExecute(object p) => true;

        private void OnLoadDataCommandExecuted(object p)
        {
            Departments = new ObservableCollection<Department>(TestData.Departments);
        }

        #endregion

        #region Command CreateEmployeeCommand - Создание нового сотрудника

        /// <summary>Создание нового сотрудника</summary>
        private ICommand _CreateEmployeeCommand;

        /// <summary>Создание нового сотрудника</summary>
        public ICommand CreateEmployeeCommand => _CreateEmployeeCommand
            ??= new LambdaCommand(OnCreateEmployeeCommandExecuted, CanCreateEmployeeCommandExecute);

        /// <summary>Проверка возможности выполнения - Создание нового сотрудника</summary>
        private bool CanCreateEmployeeCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Создание нового сотрудника</summary>
        private void OnCreateEmployeeCommandExecuted(object p)
        {

        }

        #endregion

        #region Command EditEmployeeCommand - Редактирование сотрудника

        /// <summary>Редактирование сотрудника</summary>
        private ICommand _EditEmployeeCommand;

        /// <summary>Редактирование сотрудника</summary>
        public ICommand EditEmployeeCommand => _EditEmployeeCommand
            ??= new LambdaCommand(OnEditEmployeeCommandExecuted, CanEditEmployeeCommandExecute);

        /// <summary>Проверка возможности выполнения - Редактирование сотрудника</summary>
        private bool CanEditEmployeeCommandExecute(object p) => p is Employee;

        /// <summary>Логика выполнения - Редактирование сотрудника</summary>
        private void OnEditEmployeeCommandExecuted(object p)
        {
            var employee = (Employee)p;
            var old_dep = employee.Department;
            if(_UserDialog.Edit(employee))
            {
                // Сохранить employee в БД
                // Обновить состояние интерфейса
                SelectedDepartment = null;
                SelectedEmployee = null;

                SelectedDepartment = employee.Department;
                SelectedEmployee = employee;

                if(old_dep != employee.Department)
                {
                    old_dep.Employees.Remove(employee);
                    employee.Department.Employees.Add(employee);
                }
            }
            else
            {
                // Ничего не делаем
            }
        }

        #endregion

        #region Command RemoveEmployeeCommand - Удаление сотрудника

        /// <summary>Удаление сотрудника</summary>
        private ICommand _RemoveEmployeeCommand;

        /// <summary>Удаление сотрудника</summary>
        public ICommand RemoveEmployeeCommand => _RemoveEmployeeCommand
            ??= new LambdaCommand(OnRemoveEmployeeCommandExecuted, CanRemoveEmployeeCommandExecute);

        /// <summary>Проверка возможности выполнения - Удаление сотрудника</summary>
        private bool CanRemoveEmployeeCommandExecute(object p) =>
            p is Employee employee
            && SelectedDepartment != null
            && SelectedDepartment.Employees.Contains(employee);

        /// <summary>Логика выполнения - Удаление сотрудника</summary>
        private void OnRemoveEmployeeCommandExecuted(object p)
        {
            var dep = SelectedDepartment;
            dep.Employees.Remove((Employee)p);
            SelectedDepartment = null;
            SelectedDepartment = dep;
        }

        #endregion

        public MainWindowViewModel(IUserDialog UserDialog /*, Сервис управления сотрудниками */)
        {
            _UserDialog = UserDialog;
        }
    }
}
