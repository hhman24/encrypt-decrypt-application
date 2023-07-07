using p1_encrypt_decrypt_app.ViewModels;
using System.Windows.Controls;

namespace p1_encrypt_decrypt_app.View.Pages
{
    /// <summary>
    /// Interaction logic for EncryptView.xaml
    /// </summary>
    public partial class EncryptView : UserControl
    {
        public EncryptViewModel view_model { get; set; }

        public EncryptView()
        {
            InitializeComponent();
            this.DataContext= view_model = new EncryptViewModel();
        }
    }
}
