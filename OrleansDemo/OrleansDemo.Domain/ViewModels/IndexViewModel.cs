using OrleansDemo.Domain.Services;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using System.ComponentModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace OrleansDemo.Domain.ViewModels
{
	public class IndexViewModel: ObservableObject
    {
        private string name = "";
        public string Name { get => name; set => SetProperty(ref name, value); }

        private bool isConnecting = false;
        public bool IsConnecting { get => isConnecting; set => SetProperty(ref isConnecting, value); }

        private bool isConnected = false;
        public bool IsConnected { get => isConnected; set => SetProperty(ref isConnected, value); }

        public ICommand ConnectToOrleans
        {
            get
            {
                return new AsyncRelayCommand(async () =>
                {
                    IsConnecting = true;

                    await Task.Delay(2000);

                    IsConnected = true;
                });
            }
        }

        public IndexViewModel(IUniqueNameService uniqueNameService)
        {
            Name = uniqueNameService.UniqueName;
        }
    }
}
