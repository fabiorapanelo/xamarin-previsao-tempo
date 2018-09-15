using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPrevisaoDoTempo
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VerDetalhes : ContentPage
	{
		public VerDetalhes ()
		{
			InitializeComponent ();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}