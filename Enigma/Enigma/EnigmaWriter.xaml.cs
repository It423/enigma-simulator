// EnigmaWriter.xaml.cs
// <copyright file="EnigmaWriter.xaml.cs"> This code is protected under the MIT License. </copyright>
using System;
using System.Windows;
using System.Windows.Controls;
using EnigmaUtilities;

namespace Enigma
{
    /// <summary>
    /// Interaction logic for EnigmaWriter.xaml
    /// </summary>
    public partial class EnigmaWriter : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnigmaWriter" /> class.
        /// </summary>
        /// <param name="em"> The enigma machine to use for encryption. </param>
        public EnigmaWriter(EnigmaMachine em)
        {
            // Initialize components and enigma machine
            this.InitializeComponent();
            this.EnigmaMachine = em;

            // Display correct amount of rotors
            if (this.EnigmaMachine.Rotors.Length == 3)
            {
                // Set grid length to 0
                this.DisplayGrid.ColumnDefinitions[3].Width = new GridLength(0);
            }

            // Display the correct rotor positions
            this.DisplayCorrectRotors();

            // Apply to on key down event handlers
            this.Input.TextChanged += this.Input_TextChanged;

            // Apply to window closing event handler
            this.Closing += this.Window_Closing;
        }

        /// <summary>
        /// Gets or sets the enigma machine used for encryption.
        /// </summary>
        public EnigmaMachine EnigmaMachine { get; set; }

        /// <summary>
        /// Displays the correct rotor positions.
        /// </summary>
        protected void DisplayCorrectRotors()
        {
            for (int i = 0; i < this.EnigmaMachine.Rotors.Length; i++)
            {
                // Get the corrosponding label to the rotor
                Label rotorDisplay = (Label)this.FindName(string.Format("RotorDsp{0}", i.ToString()));

                // Change the content to suit the rotor
                rotorDisplay.Content = this.EnigmaMachine.Rotors[i].RotorSetting.ToChar().ToString().ToUpper();
            }
        }

        /// <summary>
        /// Updates the output box then the text is changed in the input box.
        /// </summary>
        /// <param name="sender"> The origin of the event. </param>
        /// <param name="e"> The event arguments. </param>
        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Get the content of the input box
            string input = this.Input.Text;

            // Encrypt each character
            string output = string.Empty;
            foreach (char c in input)
            {
                char encryptedChar = this.EnigmaMachine.Encrypt(c);
                if (encryptedChar != '\0')
                {
                    output += encryptedChar;
                }

                // Add space after every 5th character
                if (output.Replace(" ", string.Empty).Length % 5 == 0)
                {
                    output += " ";
                }
            }

            // Set contents of output box
            this.Output.Text = output;

            // Display correct rotor positions
            this.DisplayCorrectRotors();

            // Reset enigma machine
            this.EnigmaMachine.Reset();
        }

        /// <summary>
        /// Opens the machine setup window on closing of the current window.
        /// </summary>
        /// <param name="sender"> The origin of the event. </param>
        /// <param name="e"> The event arguments. </param>
        private void Window_Closing(object sender, EventArgs e)
        {
            // Open set up window on closure
            MachineSetup ms = new MachineSetup();
            ms.Show();
        }
    }
}
