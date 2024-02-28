using Dziennik.Models;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Collections.ObjectModel;
namespace Dziennik;

public partial class editListPage : ContentPage
{
    private ObservableCollection<Student> Students { get; set; }
    private string filePath { get; set; }

    public editListPage(string fPath)
    {
        InitializeComponent();
        filePath = fPath;
        Students = new ObservableCollection<Student>();

        string[] students = File.ReadAllLines(filePath);

        foreach (var student in students)
        {
            Students.Add(new Student { Name = student});
        }

        studentsListView.ItemsSource = Students;

        updateStudentId();
    }


    private async void addNewStudent(object sender, EventArgs e)
    {
        string newStudentName = newStudentEntry.Text;
        if (string.IsNullOrWhiteSpace(newStudentName) == false)
        {
            Students.Add(new Student{ Name = ". " + newStudentName });
            newStudentEntry.Text = string.Empty;

            updateStudentId();
        }
        else
        {
            await DisplayAlert("B≥πd", "Wprowadü imiÍ nowego ucznia", "OK");
        }
    }

    private async void editStudent(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var student = (Student)button.BindingContext;

        var Name = student.Name.Substring(2);

        var newName = await DisplayPromptAsync("Edytuj ucznia", "Wprowadü nowe imie:", initialValue: Name);
        if (string.IsNullOrWhiteSpace(newName) == false)
        {
            student.Name = ". " + newName;
        }
    }

    private void deleteStudent(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var student = (Student)button.BindingContext;
        Students.Remove(student);

        updateStudentId();
    }

    private void updateStudentId()
    {
        for (int i = 0; i < Students.Count; i++)
        {
            Students[i].Id = i + 1;
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        File.WriteAllLines(filePath, Students.Select(student => student.Name));
    }
}