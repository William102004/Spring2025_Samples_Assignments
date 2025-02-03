namespace Maui.ecommerce;

public partial class InventoryManagementView.xml : ContentPage
{
	public InventoryManagementView.xml()
	{
		InitializeComponent();
	}

	private void CancelClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//MainPage");
    }
}