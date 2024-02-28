namespace Dziennik;

public partial class studentListPage : ContentPage
{
    private Dictionary<string, CheckBox> checkBoxes = new Dictionary<string, CheckBox>();

    public studentListPage(string className, string[] students)
    {
        InitializeComponent();
        Headbar.Text = "Dziennik klasy: " + className; 

        int studentId = 1;
        foreach (var student in students)
        {
            var studentLayout = new StackLayout { Orientation = StackOrientation.Horizontal };

            var idLabel = new Label { Text = studentId.ToString() };
            idLabel.StyleClass ??= new List<string>();
            idLabel.StyleClass.Add("studenName");

            var nazwaLabel = new Label { Text = student };
            nazwaLabel.StyleClass ??= new List<string>();
            nazwaLabel.StyleClass.Add("studenName");

            var checkBox = new CheckBox();
            checkBoxes.Add(student, checkBox);

            studentLayout.Children.Add(idLabel);
            studentLayout.Children.Add(nazwaLabel);
            studentLayout.Children.Add(checkBox);

            studentsLayout.Children.Add(studentLayout);

            studentId++;
        }
    }

    private void clearAttendance(object sender, EventArgs e)
    {
        foreach (var checkBox in checkBoxes.Values)
        {
            checkBox.IsChecked = false;
        }
    }

    private void selectForAnswer(object sender, EventArgs e)
    {
        var presentStudents = checkBoxes.Where(kvp => kvp.Value.IsChecked).Select(kvp => kvp.Key).ToList();

        if (presentStudents.Count == 0)
        {
            DisplayAlert("Uwaga", "Nie ma uczniów mo¿liwych do wziêcia do odpowiedzi!", "OK");
            return;
        }

        Random rng = new Random();
        int index = rng.Next(presentStudents.Count);
        string chosenStudent = presentStudents[index];

        var Name = chosenStudent.Substring(2);
        DisplayAlert("Wylosowany uczeñ", "Uczeñ do odpowiedzi: "+Name, "OK");
    }
}