using System.Reflection;
using System.IO;
using System.Text;

namespace WinFormsApp4
{
    public partial class InfoForm : Form
    {
        private WebBrowser webBrowser;

        public InfoForm(string title, string resourceName)
        {
            InitializeComponent();
            this.Text = title;
            this.Size = new System.Drawing.Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            webBrowser = new WebBrowser { Dock = DockStyle.Fill };
            this.Controls.Add(webBrowser);

            LoadHtmlFromResource(resourceName);
        }

        private void LoadHtmlFromResource(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourcePath = assembly.GetManifestResourceNames().FirstOrDefault(r => r.EndsWith(fileName, StringComparison.OrdinalIgnoreCase));

            if (resourcePath == null)
            {
                string allRes = string.Join(", ", assembly.GetManifestResourceNames());
                webBrowser.DocumentText = $"<h1>Ошибка</h1><p>Ресурс {fileName} не найден.</p>" +
                                          $"<p>Список доступных ресурсов в сборке: <br><b>{allRes}</b></p>";
                return;
            }

            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                string content = reader.ReadToEnd();
                webBrowser.DocumentText = content;
            }
        }

        private void InfoForm_Load(object sender, EventArgs e)
        {

        }
    }
}