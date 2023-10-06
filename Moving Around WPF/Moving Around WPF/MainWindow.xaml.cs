using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows;
using System;

namespace Moving_Around_WPF
{
    public partial class MainWindow : Window
    {
        // Constants for keyboard event flags
        private const int KEYEVENTF_EXTENDEDKEY = 0x0001;
        private const int KEYEVENTF_KEYUP = 0x0002;

        // Importing the keybd_event function from user32.dll
        [DllImport("user32.dll", SetLastError = true)]
        private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        // DispatcherTimer to handle key presses
        private DispatcherTimer timer;

        // Boolean to toggle between "w" and "s" key presses
        private bool isWKeyPressed = true;

        // Constructor for the MainWindow class
        public MainWindow()
        {
            InitializeComponent();

            // Initialize the timer
            timer = new DispatcherTimer();
            // Set the interval to 3 seconds
            timer.Interval = TimeSpan.FromSeconds(3);
            // Attach the Timer_Tick method to the timer's Tick event
            timer.Tick += Timer_Tick;
        }

        // Event handler for the "Start" button click
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            // Start the timer to trigger key presses
            timer.Start();
        }

        // Event handler for the timer's Tick event
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Generate a random interval between 1 and 4 seconds
            Random random = new Random();
            int randomInterval = random.Next(1000, 4001); // Generate a random number between 1000 and 4000 milliseconds
            TimeSpan interval = TimeSpan.FromMilliseconds(randomInterval);

            if (isWKeyPressed)
            {
                // Simulate key press for "w"
                keybd_event((byte)KeyInterop.VirtualKeyFromKey(Key.W), 0, KEYEVENTF_EXTENDEDKEY, 0);

                // Set a timer to release the "w" key after the random interval
                var releaseTimer = new DispatcherTimer();
                releaseTimer.Interval = interval;
                releaseTimer.Tick += (s, args) =>
                {
                    // Simulate key release for "w"
                    keybd_event((byte)KeyInterop.VirtualKeyFromKey(Key.W), 0, KEYEVENTF_KEYUP, 0);
                    // Stop the timer
                    releaseTimer.Stop();
                };
                releaseTimer.Start();
            }
            else
            {
                // Simulate key press for "s"
                keybd_event((byte)KeyInterop.VirtualKeyFromKey(Key.S), 0, KEYEVENTF_EXTENDEDKEY, 0);

                // Set a timer to release the "s" key after the random interval
                var releaseTimer = new DispatcherTimer();
                releaseTimer.Interval = interval;
                releaseTimer.Tick += (s, args) =>
                {
                    // Simulate key release for "s"
                    keybd_event((byte)KeyInterop.VirtualKeyFromKey(Key.S), 0, KEYEVENTF_KEYUP, 0);
                    // Stop the timer
                    releaseTimer.Stop();
                };
                releaseTimer.Start();
            }

            // Toggle between "w" and "s" key presses for the next iterations
            isWKeyPressed = !isWKeyPressed;
        }
    }
}