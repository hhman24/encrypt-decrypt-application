using System.Windows.Controls;
using p1_encrypt_decrypt_app.ViewModels;

namespace p1_encrypt_decrypt_app.View.Pages
{

    /// <summary>
    /// Interaction logic for DecryptView.xaml
    /// </summary>
    public partial class DecryptView : UserControl
    {
        public DecryptViewModel view_model { get; set; }

        public DecryptView()
        {
            InitializeComponent();
            this.DataContext = view_model = new DecryptViewModel();
            DecryptViewModel.Cur = this;
        }
    }
}
