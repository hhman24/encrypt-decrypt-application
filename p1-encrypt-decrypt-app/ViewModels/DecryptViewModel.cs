using Microsoft.Win32;
using p1_encrypt_decrypt_app.Services;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace p1_encrypt_decrypt_app.ViewModels
{
    public class DecryptViewModel:BaseViewModel
    {
        #region fields
        private string _path_file;
        private string _Kprivate;
        private static UserControl cur;
        #endregion

        #region Propertise
        public string Path_File
        {
            get => _path_file;
            set
            {
                _path_file = value;
                OnPropertyChanged(nameof(Path_File));
            }
        }

        public string Kprivate
        {
            get => _Kprivate;
            set
            {
                _Kprivate = value;
                OnPropertyChanged(nameof(Kprivate));
            }
        }

        public static UserControl Cur { get => cur; set => cur = value; }
        #endregion


        #region command
        public ICommand decrypt_file { get; set; }
        public ICommand get_path_file { get; set; }
        public ICommand import_key_file { get; set; }
        #endregion

        #region constructor
        public DecryptViewModel()
        {
            get_path_file = new ViewModelCommand(ExecuteGetPathFileCommand);
            decrypt_file = new ViewModelCommand(ExecuteDecryptFileCommand);
            import_key_file = new ViewModelCommand(ExecuteImportKeyFileCommand);
        }

        private void ExecuteGetPathFileCommand(object obj)
        {
            OpenFileDialog file_dialog = new OpenFileDialog();
            bool? success = file_dialog.ShowDialog();

            if (success == true) Path_File = file_dialog.FileName;
            else
            {
                // didn't pick any thing
                Path_File = "";
            }
        }

        private void ExecuteImportKeyFileCommand(object obj)
        {
            OpenFileDialog file_dialog = new OpenFileDialog();
            bool? success = file_dialog.ShowDialog();

            if (success == true)
            {
                Kprivate = File.ReadAllText(file_dialog.FileName);
            }
        }

        private void ExecuteDecryptFileCommand(object obj)
        {
            if (string.IsNullOrEmpty(Path_File) || string.IsNullOrEmpty(Kprivate))
            {
                // FILE PATH not exsits
                MessageBox.Show("*Invalid Path File or Kprivate");
            }
            else
            {
                if(EncryptionAlgorithm.decrypt(Path_File, Kprivate)) MessageBox.Show("Success");
                else MessageBox.Show("Wrrong Private Key");
            }

        }

        #endregion
    }
}
