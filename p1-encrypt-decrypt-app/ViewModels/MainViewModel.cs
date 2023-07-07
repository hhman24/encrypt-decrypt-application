using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace p1_encrypt_decrypt_app.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        // Fields
        private BaseViewModel _current_child_view;

        public BaseViewModel Current_child_view
        { 
            get => _current_child_view;
            set 
            {
                _current_child_view = value;
                OnPropertyChanged(nameof(Current_child_view));
            } 
        }

        // Commands
        public ICommand ShowEncryptViewCommand { get; }
        public ICommand ShowDecryptViewCommand { get; }

        public MainViewModel()
        {
            //Initialize commands
            ShowEncryptViewCommand = new ViewModelCommand(ExecuteShowEncryptViewCommand);
            ShowDecryptViewCommand = new ViewModelCommand(ExecuteShowDecryptViewCommand);
            
            //Default view
            ExecuteShowEncryptViewCommand(null);


        }

        private void ExecuteShowEncryptViewCommand(object obj)
        {
            Current_child_view = new EncryptViewModel();
        }

        private void ExecuteShowDecryptViewCommand(object obj)
        {
            Current_child_view = new DecryptViewModel();
        }
    }
}
