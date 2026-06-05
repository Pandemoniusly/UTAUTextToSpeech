using System.Diagnostics;
using System.Media;
using System.Text;

namespace utautexttospeech
{
    public partial class Form1 : Form
    {

        public string FilePathh = "C:/";
        public string CachePath = "C:/";
        public static Process UtauRunning;
        public static ProgressBar progress;
        public static TextBox textbox;
        public Form1()
        {
            InitializeComponent();
            progress = progressBar1;
            textbox = textBox2;
            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
        }
        public static async Task<bool> RenderDoneAsync(DirectoryInfo directory, string cache)
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(100));
            int SecondCount = 0;
            Thread.Sleep(200);
            progress.Value = 0;
            while (!cts.Token.IsCancellationRequested)
            {
                foreach (FileInfo file in directory.GetFiles())
                {
                    if (file.Exists & file.FullName != Path.Combine(cache, "UTAU_TTS_OUTPUT.wav"))
                    {
                        try
                        {
                            File.Copy(file.FullName, Path.Combine(cache, "UTAU_TTS_OUTPUT.wav"));
                            SoundPlayer yippee = new SoundPlayer(Properties.Resources.fast);
                            yippee.Play();
                            progress.Value = 100;
                            if (UtauRunning != null)
                            {
                                UtauRunning.Kill();
                            }
                            return true;
                        }
                        catch
                        {
                            textbox.Text = "utau tts exists";
                            SoundPlayer EVILERROR = new SoundPlayer(Properties.Resources.RAHH);
                            EVILERROR.Play();
                            if (UtauRunning != null)
                            {
                                UtauRunning.Kill();
                            }
                            return false;
                        }
                    }
                }
                try
                {
                    await Task.Delay(200, cts.Token);
                    SecondCount += 1;
                    if (SecondCount == 5)
                    {
                        progress.Value += 1;
                        SecondCount = 0;
                    }
                }
                catch (TaskCanceledException)
                {
                    break;
                }
            }
            SoundPlayer waghhh = new SoundPlayer(Properties.Resources.tooslow);
            waghhh.Play();
            progress.Value = 100;
            if (UtauRunning != null)
            {
                UtauRunning.Kill();
            }
            return false;
        }
        public static async Task<bool> FileDoneAsync(string path)
        {
            using var cts = new CancellationTokenSource(2500);
            Thread.Sleep(200);
            while (!cts.Token.IsCancellationRequested)
            {
                if (File.Exists(path))
                {
                    try
                    {
                        using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                        {
                            return true;
                        }
                    }
                    catch (IOException)
                    {

                    }

                    try
                    {
                        await Task.Delay(200, cts.Token);
                    }
                    catch (TaskCanceledException)
                    {
                        break;
                    }
                }
            }
            return false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (UtauRunning != null) UtauRunning.Kill();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Get the full path of the selected file
                FilePathh = openFileDialog1.FileName;

                // Display the path in your TextBox (if you added one)
                if (!FilePathh.EndsWith(".ustx"))
                {
                    textbox.Text = "not .ustx";
                    SoundPlayer EVILERROR = new SoundPlayer(Properties.Resources.RAHH);
                    EVILERROR.Play();
                    FilePathh = "C:/";
                }
                textBox3.Text = FilePathh;
            }
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            string enteredText = textBox2.Text;

            if (enteredText == "")
            {
                textbox.Text = "THERES NO TEXT";
                SoundPlayer EVILERROR = new SoundPlayer(Properties.Resources.RAHH);
                EVILERROR.Play();
                return;
            }
            else if (FilePathh == "C:/" || FilePathh == "")
            {
                textbox.Text = "THERES NO USTX";
                SoundPlayer EVILERROR = new SoundPlayer(Properties.Resources.RAHH);
                EVILERROR.Play();
                return;
            }
            else if (CachePath == "C:/" || CachePath == "")
            {
                textbox.Text = "THERES NO CACHE";
                SoundPlayer EVILERROR = new SoundPlayer(Properties.Resources.RAHH);
                EVILERROR.Play();
                return;
            }
            enteredText = enteredText.Replace(",", ",R,");
            enteredText = enteredText.Replace(".", ",R,R,");

            // Load the OpenUtau project file

            string[] lines = File.ReadAllLines(FilePathh);
            string[] newLines = lines;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("    lyric: "))
                {
                    newLines[i] = "    lyric: " + enteredText;
                }
                else if (lines[i].StartsWith("    duration: "))
                {
                    newLines[i] = "    duration: " + enteredText.Replace(",R,", string.Empty).Length * 70;
                }
            }
            File.WriteAllLines(FilePathh, newLines, Encoding.UTF8);
            DirectoryInfo directory = new DirectoryInfo(CachePath);

            // Delete all files inside the folder
            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }
            if (UtauRunning != null)
            {
                UtauRunning.Kill();
            }
            if (await FileDoneAsync(Path.GetFullPath(FilePathh)))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = FilePathh,
                    UseShellExecute = true, // Required to open files with their default application
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                UtauRunning = Process.Start(startInfo);
                await RenderDoneAsync(directory, CachePath);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                // Get the full path of the selected file
                CachePath = folderBrowserDialog1.SelectedPath;
                if (!CachePath.EndsWith("Cache"))
                {
                    textbox.Text = "doesnt end with cache";
                    SoundPlayer EVILERROR = new SoundPlayer(Properties.Resources.RAHH);
                    EVILERROR.Play();
                    FilePathh = "C:/";
                }
                // Display the path in your TextBox (if you added one)
                textBox4.Text = Path.Combine(CachePath, "UTAU_TTS_OUTPUT.wav");
            }
        }
    }
}
