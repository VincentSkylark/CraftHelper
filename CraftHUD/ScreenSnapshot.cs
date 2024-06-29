using Microsoft.VisualBasic.Devices;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CraftHUD
{
    public class ScreenSnapshot : Form
    {
        private Button loadBtn;
        private PictureBox pictureBox;
        private Button captureBtn;
        private ComboBox processList;
        private Label maxTryLabel;
        private TextBox maxTry;
        private Panel panel1;

        private int _maxTry { get; set; }
        private GlobalKeyboardHook _globalKeyboardHook;
        private Label instructionLabel;
        private Button craftFlaskBtn;
        private Button altSpamBtn;
        private Button fusingBtn;
        private CancellationTokenSource _cancellationTokenSource;

        public ScreenSnapshot()
        {
            _maxTry = 100;
            InitializeComponent();
            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += OnKeyPressed;
        }

        private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            if (e.KeyboardData.VirtualCode != GlobalKeyboardHook.F10)
                return;
            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
            {
                _cancellationTokenSource.Cancel();
                e.Handled = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) { _globalKeyboardHook?.Dispose(); }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            loadBtn = new Button();
            pictureBox = new PictureBox();
            captureBtn = new Button();
            processList = new ComboBox();
            panel1 = new Panel();
            altSpamBtn = new Button();
            craftFlaskBtn = new Button();
            instructionLabel = new Label();
            maxTryLabel = new Label();
            maxTry = new TextBox();
            fusingBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // loadBtn
            // 
            loadBtn.Location = new Point(20, 91);
            loadBtn.Name = "loadBtn";
            loadBtn.Size = new Size(75, 23);
            loadBtn.TabIndex = 1;
            loadBtn.Text = "load";
            loadBtn.UseVisualStyleBackColor = true;
            loadBtn.Click += loadBtn_Click;
            // 
            // pictureBox
            // 
            pictureBox.Location = new Point(211, 54);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(960, 540);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 2;
            pictureBox.TabStop = false;
            pictureBox.MouseClick += pictureBox_Click;
            // 
            // captureBtn
            // 
            captureBtn.Location = new Point(117, 91);
            captureBtn.Name = "captureBtn";
            captureBtn.Size = new Size(75, 23);
            captureBtn.TabIndex = 3;
            captureBtn.Text = "capture";
            captureBtn.UseVisualStyleBackColor = true;
            captureBtn.Click += captureBtn_Click;
            // 
            // processList
            // 
            processList.FormattingEnabled = true;
            processList.Location = new Point(20, 54);
            processList.Name = "processList";
            processList.Size = new Size(172, 23);
            processList.TabIndex = 4;
            // 
            // panel1
            // 
            panel1.Controls.Add(fusingBtn);
            panel1.Controls.Add(altSpamBtn);
            panel1.Controls.Add(craftFlaskBtn);
            panel1.Controls.Add(instructionLabel);
            panel1.Controls.Add(maxTryLabel);
            panel1.Controls.Add(maxTry);
            panel1.Location = new Point(20, 135);
            panel1.Name = "panel1";
            panel1.Size = new Size(172, 459);
            panel1.TabIndex = 8;
            // 
            // altSpamBtn
            // 
            altSpamBtn.Location = new Point(0, 159);
            altSpamBtn.Name = "altSpamBtn";
            altSpamBtn.Size = new Size(132, 23);
            altSpamBtn.TabIndex = 9;
            altSpamBtn.Text = "Alteration Spam";
            altSpamBtn.UseVisualStyleBackColor = true;
            altSpamBtn.Click += altSpamBtn_Click;
            // 
            // craftFlaskBtn
            // 
            craftFlaskBtn.Location = new Point(0, 109);
            craftFlaskBtn.Name = "craftFlaskBtn";
            craftFlaskBtn.Size = new Size(132, 23);
            craftFlaskBtn.TabIndex = 11;
            craftFlaskBtn.Text = "Craft Magic Flask";
            craftFlaskBtn.UseVisualStyleBackColor = true;
            craftFlaskBtn.Click += craftFlaskBtn_Click;
            // 
            // instructionLabel
            // 
            instructionLabel.AutoSize = true;
            instructionLabel.Location = new Point(2, 2);
            instructionLabel.Name = "instructionLabel";
            instructionLabel.Size = new Size(96, 15);
            instructionLabel.TabIndex = 10;
            instructionLabel.Text = "Press F10 to Stop";
            // 
            // maxTryLabel
            // 
            maxTryLabel.AutoSize = true;
            maxTryLabel.Location = new Point(3, 34);
            maxTryLabel.Name = "maxTryLabel";
            maxTryLabel.Size = new Size(92, 15);
            maxTryLabel.TabIndex = 9;
            maxTryLabel.Text = "max try number";
            // 
            // maxTry
            // 
            maxTry.Location = new Point(3, 52);
            maxTry.Name = "maxTry";
            maxTry.Size = new Size(100, 23);
            maxTry.TabIndex = 8;
            maxTry.Text = "100";
            maxTry.TextChanged += maxTry_TextChanged;
            // 
            // fusingBtn
            // 
            fusingBtn.Location = new Point(0, 213);
            fusingBtn.Name = "fusingBtn";
            fusingBtn.Size = new Size(129, 23);
            fusingBtn.TabIndex = 12;
            fusingBtn.Text = "Fusing Spam";
            fusingBtn.UseVisualStyleBackColor = true;
            fusingBtn.Click += fusingBtn_Click;
            // 
            // ScreenSnapshot
            // 
            ClientSize = new Size(1183, 621);
            Controls.Add(panel1);
            Controls.Add(processList);
            Controls.Add(captureBtn);
            Controls.Add(pictureBox);
            Controls.Add(loadBtn);
            Name = "ScreenSnapshot";
            Load += ScreenSnapshot_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            processList.Items.Clear();
            var processNames = ScreenshotHelper.GetAllWindowHandleNames();
            foreach (var processName in processNames)
            {
                processList.Items.Add(processName);
            }
        }

        private void ScreenSnapshot_Load(object sender, EventArgs e)
        {

        }



        private void captureBtn_Click(object sender, EventArgs e)
        {
            // Clear the PictureBox before capturing the screenshot
            pictureBox.Image = null;

            // Get the process name from the TextBox (you can change this to your desired process name)
            string processName = processList.Text.Trim();

            // Find the process by its name
            Process process = Process.GetProcessesByName(processName).FirstOrDefault();

            if (process != null)
            {
                // Find the window handle of the process
                IntPtr hWnd = process.MainWindowHandle;

                // Get the window position and dimensions
                User32Helper.GetWindowRect(hWnd, out Rectangle windowRect);

                // Calculate the window size
                int width = windowRect.Width - windowRect.X;
                int height = windowRect.Height - windowRect.Y;

                var image = ScreenshotHelper.GetBitmapScreenshot(process.ProcessName);

                // Display the screenshot in the PictureBox
                pictureBox.Image = image;
            }
            else
            {
                MessageBox.Show("Process not found or the process window is not visible.");
            }
        }

        private void pictureBox_Click(object sender, MouseEventArgs e)
        {
            int appCoordX = e.X * 2;
            int appCoordY = e.Y * 2;
            MessageBox.Show($"Coordinate {appCoordX}, {appCoordY}");

        }

        private void maxTry_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(maxTry.Text, out int result))
            {
                _maxTry = result;
            }
            else
            {
                _maxTry = 100;
            }
        }

        private void craftFlaskBtn_Click(object sender, EventArgs e)
        {
            var currencyData = DataHelper.LoadCoord();
            DataHelper.FocusGameWindow();
            _cancellationTokenSource = new CancellationTokenSource();
            Thread thread = new Thread(() => CraftHelper.CraftFlask(currencyData, _cancellationTokenSource.Token));
            thread.SetApartmentState(ApartmentState.STA); // Set the thread to STA mode
            thread.Start();
        }

        private void altSpamBtn_Click(object sender, EventArgs e)
        {
            var currencyData = DataHelper.LoadCoord();
            DataHelper.FocusGameWindow();
            _cancellationTokenSource = new CancellationTokenSource();
            Thread thread = new Thread(() => CraftHelper.AltSpamCraft(currencyData, _cancellationTokenSource.Token));
            thread.SetApartmentState(ApartmentState.STA); // Set the thread to STA mode
            thread.Start();
        }

        private void fusingBtn_Click(object sender, EventArgs e)
        {
            var currencyData = DataHelper.LoadCoord();
            DataHelper.FocusGameWindow();
            _cancellationTokenSource = new CancellationTokenSource();
            Thread thread = new Thread(() => CraftHelper.SixLink(currencyData, _cancellationTokenSource.Token));
            thread.SetApartmentState(ApartmentState.STA); // Set the thread to STA mode
            thread.Start();
        }
    }
}
