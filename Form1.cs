using NAudio.Wave;
using OpenUtau.Classic;
using OpenUtau.Core.Format;
using OpenUtau.Core.Render;
using OpenUtau.Core.SignalChain;
using OpenUtau.Core.Ustx;
using OpenUtau.Core.Util;
using SharpCompress.Common;
using System.Diagnostics;
using System.IO.Compression;
using System.Media;
using System.Resources;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace utautexttospeech
{
    public partial class Form1 : Form
    {

        public string FilePathh = "C:/";
        public string CachePath = "C:/";
        public Process UtauRunning;
        public Form1()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
        }
        public static async Task<bool> RenderDoneAsync(DirectoryInfo directory,string cache)
        {
            using var cts = new CancellationTokenSource(20000);
            Thread.Sleep(200);
            while (!cts.Token.IsCancellationRequested)
            {
                foreach (FileInfo file in directory.GetFiles())
                {
                    if (file.Exists & file.FullName != Path.Combine(cache, "UTAU_TTS_OUTPUT.wav"))
                    {
                        File.Copy(file.FullName, Path.Combine(cache, "UTAU_TTS_OUTPUT.wav"));
                        SoundPlayer yippee = new SoundPlayer(Properties.Resources.fast);
                        yippee.Play();
                        return true;
                    }
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
            SoundPlayer waghhh = new SoundPlayer(Properties.Resources.tooslow);
            waghhh.Play();
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
                SoundPlayer EVILERROR = new SoundPlayer(Properties.Resources.RAHH);
                EVILERROR.Play();
                return;
            }
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
                    newLines[i] = "    duration: " + enteredText.Length*70;
                }
            }
            File.WriteAllLines(FilePathh, newLines,Encoding.UTF8);
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
                await RenderDoneAsync(directory,CachePath);
            }
            /*{
                UProject project = OpenUtau.Core.Yaml.DefaultDeserializer.Deserialize<UProject>(File.ReadAllText(FilePathh, Encoding.UTF8));
                string file = "";
                var cts = new CancellationTokenSource(30000);
                RenderEngine engine = new RenderEngine(project);
                var trackMixes = engine.RenderTracks(OpenUtau.Core.DocManager.Inst.MainScheduler, ref cts);
                file = OpenUtau.Core.PathManager.Inst.GetExportPath(ExportPath, project.tracks[0]);
                WaveFileWriter.CreateWaveFile16(file, new ExportAdapter(trackMixes[0]).ToMono(1, 0));
                SoundPlayer EVILERROR = new SoundPlayer(Properties.Resources.RAHH);
                EVILERROR.Play();
                return;
            }
            else
            {
                textBox2.Text = "Failed to update text within 2 seconds";
                SoundPlayer EVILERROR = new SoundPlayer(Properties.Resources.RAHH);
                EVILERROR.Play();
            }*/
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                // Get the full path of the selected file
                CachePath = folderBrowserDialog1.SelectedPath;

                // Display the path in your TextBox (if you added one)
                textBox4.Text = Path.Combine(CachePath, "UTAU_TTS_OUTPUT.wav");
            }
        }
    }
}
