namespace Dziennik
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            refreashClassList();
        }
        private async void onAddClassClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new addClassPage());
        }

        private void onRefreashClassListClicked(object sender, EventArgs e)
        {
            refreashClassList();
        }

        private void refreashClassList()
        {
            classStackLayout.Children.Clear(); 

            string folderPath = Path.Combine(AppContext.BaseDirectory, "classes");

            if (Directory.Exists(folderPath))
            {
                var classFiles = Directory.GetFiles(folderPath, "*.txt");

                foreach (var cFile in classFiles)
                {
                    var className = Path.GetFileNameWithoutExtension(cFile);

                    var contentView = new ContentView();
                    var label = new Label { Text = className };
                    label.StyleClass ??= new List<string>();
                    label.StyleClass.Add("classLabel");

                    var deleteButton = new Button { Text = "Usuń" };
                    deleteButton.StyleClass ??= new List<string>();
                    deleteButton.StyleClass.Add("delButton");

                    contentView.GestureRecognizers.Add(new TapGestureRecognizer
                    {
                        Command = new Command(() => PokazUczniow(cFile))
                    });

                    deleteButton.Clicked += async (sender, e) =>
                    {
                        var result = await DisplayAlert("Potwierdzenie", $"Czy na pewno chcesz usunąć klasę {className}?", "Tak", "Anuluj");
                        if (result)
                        {
                            File.Delete(cFile);
                            refreashClassList(); 
                        }
                    };

                    var stackLayout = new StackLayout { Orientation = StackOrientation.Horizontal };
                    stackLayout.Children.Add(label);
                    stackLayout.Children.Add(deleteButton);

                    contentView.Content = stackLayout;

                    classStackLayout.Children.Add(contentView);
                }
            }
        }

        private async void PokazUczniow(string filePath)
        {
            var className = Path.GetFileNameWithoutExtension(filePath);

            var options = await DisplayActionSheet($"Opcje dla klasy {className}", "Anuluj", null, "Wyświetl listę uczniów", "Edytuj listę uczniów");

            switch (options)
            {
                case "Wyświetl listę uczniów":
                    string[] students = File.ReadAllLines(filePath);

                    var sListPage = new studentListPage(className, students);

                    await Navigation.PushAsync(sListPage);
                    break;

                case "Edytuj listę uczniów":
                    var eListPage = new editListPage(filePath);
                    await Navigation.PushAsync(eListPage);
                    break;

                default:
                    break;
            }
        }

    }
}