using System;
using System.Windows;
using EmploiesSimpleWpfApp.Infrastructure.Commands.Base;

namespace EmploiesSimpleWpfApp.Infrastructure.Commands
{
    internal class CloseWindowCommand : Command
    {
        protected override bool CanExecute(object p) => p is Window || App.CurrentWindow != null;

        protected override void Execute(object p) => (p as Window ?? App.CurrentWindow)?.Close();
    }
}
