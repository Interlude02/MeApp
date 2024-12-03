namespace MeApp;

public partial class MainPage : ContentPage
{
    private readonly LocalDbService _dbService;
    private int _editNotecardId;

    public MainPage(LocalDbService dbService)
    {
        InitializeComponent();
        _dbService = dbService;
        Task.Run(async () => listView.ItemsSource = await _dbService.GetNotecard());
    }

    private async void saveButton_Clicked(object sender, EventArgs e)
    {
        if (_editNotecardId == 0)
        {
            // Add Customer

            await _dbService.Create(new Notecard
            {
                Question = questionEntryField.Text,
                Answer = answerEntryField.Text
            });
        }
        else
        {
            // Edit Customer

            await _dbService.Update(new Notecard
            {
                Id = _editNotecardId,
                Question = questionEntryField.Text,
                Answer = answerEntryField.Text
            });

            _editNotecardId = 0;
        }

        questionEntryField.Text = string.Empty;
        answerEntryField.Text = string.Empty;

        listView.ItemsSource = await _dbService.GetNotecard();
    }

    private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var notecard = (Notecard)e.Item;
        var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

        switch (action)
        {
            case "Edit":

                _editNotecardId = notecard.Id;
                questionEntryField.Text = notecard.Question;
                answerEntryField.Text = notecard.Answer;

                break;
            case "Delete":

                await _dbService.Delete(notecard);
                listView.ItemsSource = await _dbService.GetNotecard();

                break;
        }
    }

    private void accountInformationButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AccountInfo());
    }

    private void privatePolicyButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PrivatePolicy());
    }

    private void helpCenterButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new HelpCenter());
    }

    private void signOutButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SignOut());
    }
}
