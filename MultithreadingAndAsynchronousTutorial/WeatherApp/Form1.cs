using System.Security.Policy;

namespace TutorialAsyncAwait
{
    public partial class Form1 : Form
    {
        private const string URL = "https://weatherslowapi.azurewebsites.net/weather/";
        private readonly HttpClient _httpClient = new HttpClient();
        public Form1()
        {
            InitializeComponent();
            _httpClient.BaseAddress = new Uri(URL);
        }

        private void dummyButton1_Click(object sender, EventArgs e)
        {
            dummyTextBox.Text = "Some dummy text...";
        }

        private void dummyButton2_Click(object sender, EventArgs e)
        {
            dummyTextBox.Text = "Even more dummy text...";
        }

        private async void searchButton_Click(object sender, EventArgs e)
        {
            resultTextBox.Clear();
            var cities = citiesTextBox.Text.Split(",");
            var tasks = new List<Task<string>>();
            foreach (var city in cities)
            {
                tasks.Add(_httpClient.GetStringAsync(city));
            }

            await Task.WhenAll(tasks);

            while (tasks.Count > 0)
            {
                var finished = await Task.WhenAny(tasks);
                tasks.Remove(finished);
                var result = await finished;
                resultTextBox.AppendText(result + "\r\n");
            }

            
        }
    }
}
