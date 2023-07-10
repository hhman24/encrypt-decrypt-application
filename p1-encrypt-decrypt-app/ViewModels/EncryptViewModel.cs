using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using p1_encrypt_decrypt_app.Services;
using WinForms = System.Windows.Forms;
using System.IO;

namespace p1_encrypt_decrypt_app.ViewModels
{
    public class EncryptViewModel:BaseViewModel
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
        public ICommand encrypt_file { get; set; }
        public ICommand get_path_file { get; set; }
        public ICommand export_key_file { get; set; }
        public ICommand copy_key_clipboard { get; set; }
        #endregion

        #region constructor
        public EncryptViewModel()
        {
            get_path_file = new ViewModelCommand(ExecuteGetPathFileCommand);
            encrypt_file = new ViewModelCommand(ExecuteEncryptFileCommand);
            copy_key_clipboard = new ViewModelCommand(ExecuteCopyKeyClipBoardCommand);
            export_key_file = new ViewModelCommand(ExecuteExportKeyFileCommand);
        }
        #endregion

        private void ExecuteGetPathFileCommand(object obj)
        {
            OpenFileDialog file_dialog = new OpenFileDialog();
            bool? success = file_dialog.ShowDialog();
            
            if(success == true) Path_File = file_dialog.FileName;
            else
            {
                // didn't pick any thing
                Path_File = "";
            }
        }

        private void ExecuteEncryptFileCommand(object obj)
        {
            if (string.IsNullOrEmpty(Path_File))
            {
                // FILE PATH not exsits
                MessageBox.Show("*Invalid Path File");
            }
            else
            {
                // FILE PATH exsits
                //WinForms.FolderBrowserDialog dialog = new WinForms.FolderBrowserDialog();

                //WinForms.DialogResult result = dialog.ShowDialog();
                //if (result == WinForms.DialogResult.OK)
                //{
                //    string path_folder_des =dialog.SelectedPath;
                //    Kprivate = EncryptionAlgorithm.encypt(Path_File, path_folder_des, 2048);
                //    MessageBox.Show("Success");
                //}

                Kprivate = EncryptionAlgorithm.encypt(Path_File, 2048);

            }
            
        }

        private void ExecuteCopyKeyClipBoardCommand(object obj)
        {
            if(!string.IsNullOrEmpty(Kprivate)) Clipboard.SetText(Kprivate);
        }

        private void ExecuteExportKeyFileCommand(object obj)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if(dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, Kprivate.ToString());
            }
        }
    }
}
