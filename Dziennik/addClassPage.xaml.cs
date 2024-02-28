namespace Dziennik;

public partial class addClassPage : ContentPage
{
	public addClassPage()
	{
		InitializeComponent();
	}

    private void addClass(object sender, EventArgs e)
    {
        string className = classNameEntry.Text;

        string folderPath = Path.Combine(AppContext.BaseDirectory, "classes");

        if (Directory.Exists(folderPath) == false)
        {
            Directory.CreateDirectory(folderPath);
        }

        string filePath = Path.Combine(folderPath, className + ".txt");

        using (StreamWriter writer = File.CreateText(filePath))

        DisplayAlert("Sukces", "Klasa zosta³a dodana pomyœlnie.", "OK");
        ClearForm();

        Navigation.PopAsync();
    }

    private void ClearForm()
    {
        classNameEntry.Text = string.Empty;
    }
}