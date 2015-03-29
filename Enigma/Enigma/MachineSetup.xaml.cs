// MachineSetup.xaml.cs
// <copyright file="MachineSetup.xaml.cs"> This code is protected under the MIT License. </copyright>
using System.Windows;
using System.Windows.Controls;
using EnigmaUtilities;

namespace Enigma
{
    /// <summary>
    /// Interaction logic for MachineSetup.xaml
    /// </summary>
    public partial class MachineSetup : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MachineSetup" /> class.
        /// </summary>
        public MachineSetup()
        {
            this.InitializeComponent();

            // Tell the program the default data
            this.FourRotors = false;
            this.RingSettingsActive = false;
            this.RingSettings = new int[4] { 0, 0, 0, 0 };
            this.RotorPositions = new int[4] { 0, 0, 0, 0 };
        }

        /// <summary>
        /// Gets or sets the ring settings.
        /// </summary>
        public int[] RingSettings { get; set; }

        /// <summary>
        /// Gets or sets the array of rotor positions.
        /// </summary>
        public int[] RotorPositions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the ring settings are currently being viewed.
        /// </summary>
        public bool RingSettingsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether there are four rotors or not.
        /// </summary>
        public bool FourRotors { get; set; }

        /// <summary>
        /// Displays the correct rotor settings.
        /// </summary>
        protected void DisplayCorrectSettings()
        {
            for (int i = 0; i < 4; i++)
            {
                // Display correct rotor positon
                Label rotorPosition = (Label)this.FindName(string.Format("RotorPositionDsp{0}", i.ToString()));
                rotorPosition.Content = this.RotorPositions[i].ToChar().ToString().ToUpper();

                // Display correct ring settings
                Label ringSetting = (Label)this.FindName(string.Format("RingSettingDsp{0}", i.ToString()));
                ringSetting.Content = this.RingSettings[i] + 1;
            }
        }

        /// <summary>
        /// Begins encryption using the current enigma settings.
        /// </summary>
        /// <param name="sender"> The origin of the event. </param>
        /// <param name="e"> The event arguments. </param>
        private void Begin_Click(object sender, RoutedEventArgs e)
        {
            // Use the currently selected rotor based settings to create the rotors
            ////int firstRotor = this.FourRotors ? 0 : 1; // Get the first rotor number
            ////Rotor[] rotors = new Rotor[4 - firstRotor]; // Create an array of rotors
            ////for (int i = firstRotor; i < 4; i++)
            ////{
            ////    // Generate the rotor
            ////    rotors[i - firstRotor] = new Rotor(this.RingSettings[i], this.RotorPositions[i], ...);
            ////}

            // Use settings to create enigma machine instance
            ////EnigmaMachine em = new EnigmaMachine(...);

            // Create encryption window
            ////EnigmaWriter ew = new EnigmaWriter(em);

            // Show encryptor, hide settings
            ////ew.Show();
            this.Close();
        }

        /// <summary>
        /// Changes which rotor setup is being viewed.
        /// </summary>
        /// <param name="sender"> The origin of the event. </param>
        /// <param name="e"> The event arguments. </param>
        private void SwitchRotorView_Click(object sender, RoutedEventArgs e)
        {
            // Get which rotor is the first rotro
            int minRotor = this.FourRotors ? 0 : 1;

            if (this.RingSettingsActive)
            {
                // Switch from ring settings to rotor positions
                for (int i = 0; i < 8; i++)
                {
                    if (i < 4 + minRotor)
                    {
                        this.RotorSettingsGrid.ColumnDefinitions[i].Width = new GridLength(0);
                    }
                    else
                    {
                        this.RotorSettingsGrid.ColumnDefinitions[i].Width = new GridLength(1, GridUnitType.Star);
                    }
                }

                // Tell the program it has switched
                this.RingSettingsActive = !this.RingSettingsActive;
                this.SwitchRotorView.Content = "  View Ring Settings  ";
            }
            else
            {
                // Switch from rotor positions to ring settings
                for (int i = 0; i < 8; i++)
                {
                    if (i < 4 && i >= minRotor)
                    {
                        this.RotorSettingsGrid.ColumnDefinitions[i].Width = new GridLength(1, GridUnitType.Star);
                    }
                    else
                    {
                        this.RotorSettingsGrid.ColumnDefinitions[i].Width = new GridLength(0);
                    }
                }

                // Tell the program it has switched
                this.RingSettingsActive = !this.RingSettingsActive;
                this.SwitchRotorView.Content = "  View Rotor Positions  ";
            }
        }

        /// <summary>
        /// Changes values in the program when the four rotor check box has changed.
        /// </summary>
        /// <param name="sender"> The origin of the event. </param>
        /// <param name="e"> The event arguments. </param>
        private void FourRotorCheckBox_Changed(object sender, RoutedEventArgs e)
        {
            // Change the value indicating if there are four rotors
            this.FourRotors = !this.FourRotors;

            // Display correct rotors
            if (this.FourRotors)
            {
                int currentFourthRotorIndex = this.RingSettingsActive ? 0 : 4; // Get the currently displayed fourth rotor
                this.RotorSettingsGrid.ColumnDefinitions[currentFourthRotorIndex].Width = new GridLength(1, GridUnitType.Star);
            }
            else
            {
                this.RotorSettingsGrid.ColumnDefinitions[0].Width = new GridLength(0);
                this.RotorSettingsGrid.ColumnDefinitions[4].Width = new GridLength(0);
            }
        }

        /// <summary>
        /// Handles a click of a rotor position button.
        /// </summary>
        /// <param name="sender"> The origin of the event. </param>
        /// <param name="e"> The event arguments. </param>
        private void RotorSettingsChanger_Click(object sender, RoutedEventArgs e)
        {
            // Iterate over all 4 sets of buttons
            for (int i = 0; i < 4; i++)
            {
                if (sender.Equals(this.FindName(string.Format("RotorPositionDec{0}", i.ToString()))))
                {
                    // If the button is a decrease rotor position, decrease the rotor position
                    this.RotorPositions[i]--;
                }
                else if (sender.Equals(this.FindName(string.Format("RotorPositionInc{0}", i.ToString()))))
                {
                    // If the button is a increase rotor position, increase the rotor position
                    this.RotorPositions[i]++;
                }
                else if (sender.Equals(this.FindName(string.Format("RingSettingDec{0}", i.ToString()))))
                {
                    // If the button is a decrease ring setting, decrease the rotor position
                    this.RingSettings[i]--;
                }
                else if (sender.Equals(this.FindName(string.Format("RingSettingInc{0}", i.ToString()))))
                {
                    // If the button is a increase ring setting, increase the rotor position
                    this.RingSettings[i]++;
                }

                // Keep all settings in range of 26
                this.RingSettings[i] = EnigmaUtilities.Resources.Mod(this.RingSettings[i], 26);
                this.RotorPositions[i] = EnigmaUtilities.Resources.Mod(this.RotorPositions[i], 26);
            }

            // Display the correct settings
            this.DisplayCorrectSettings();
        }
    }
}
