using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace p1_encrypt_decrypt_app.ViewModels
{
    public class EncryptViewModel:BaseViewModel
    {
        #region fields
        private string _length_key_rsa;
        private string _key_aes;
        private string _Kpublic;
        private string _Kprivate;
        #endregion

        #region command
        public ICommand Encrypt_File { get; set; }
        public ICommand Load_File { get; set; }
        public ICommand Export_Key_File { get; set; }
        public ICommand Generate_Key_RSA { get; set; }
        public ICommand Generate_Key_AES { get; set; }
        #endregion

        #region constructor
        public EncryptViewModel()
        {

        }
        #endregion

        #region methods
        #endregion


    }
}
