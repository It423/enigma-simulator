// MachineSetup.xaml.cs
// <copyright file="MachineSetup.xaml.cs"> This code is protected under the MIT License. </copyright>
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Linq;
using EnigmaUtilities;
using EnigmaUtilities.Components;
using EnigmaUtilities.Data;
using EnigmaUtilities.Data.XML;
using Microsoft.Win32;

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
            this.RingSettingsActive = false;
            this.ResetData();
        }

        /// <summary>
        /// Gets or sets the data about the current rotors.
        /// </summary>
        public RotorData[] RotorData { get; set; }

        /// <summary>
        /// Gets or sets the data about the current reflector.
        /// </summary>
        public ReflectorData ReflectorData { get; set; }

        /// <summary>
        /// Gets or sets the ring settings.
        /// </summary>
        public int[] RingSettings { get; set; }

        /// <summary>
        /// Gets or sets the array of rotor positions.
        /// </summary>
        public int[] RotorPositions { get; set; }

        /// <summary>
        /// Gets or sets the plug board settings.
        /// </summary>
        public string[] PlugboardSettings { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the ring settings are currently being viewed.
        /// </summary>
        public bool RingSettingsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether there are four rotors or not.
        /// </summary>
        public bool FourRotors { get; set; }

        /// <summary>
        /// Resets the settings on the machine.
        /// </summary>
        private void ResetData()
        {
            // Reset data
            this.FourRotors = false;
            this.RingSettings = new int[4] { 0, 0, 0, 0 };
            this.RotorPositions = new int[4] { 0, 0, 0, 0 };
            this.PlugboardSettings = new string[10] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
            this.ReflectorData = new ReflectorData("Reflector B", "YRUHQSLDPXNGOKMIEBFZCWVJAT");
            this.RotorData = new RotorData[4] 
            {
                new RotorData("Beta", "LEYJVCNIXWPBQMDRTAKZGFUHOS", string.Empty),
                new RotorData("III", "BDFHJLCPRTXVZNYEIWGAKMUSQO", "W"),
                new RotorData("II", "AJDKSIRUXBLHWTMCQGZNPYFVOE", "F"),
                new RotorData("I", "EKMFLGDQVZNTOWYHXUSPAIBRCJ", "R")
            };

            // Display correct content
            this.DisplayCorrectSettings();
        }

        /// <summary>
        /// Displays the correct rotor settings and keep them in range of 0-25.
        /// </summary>
        private void DisplayCorrectSettings()
        {
            // Display correct rotor data
            for (int i = 0; i < 4; i++)
            {
                // Keep settings in range of 26
                this.RingSettings[i] = EnigmaUtilities.Resources.Mod(this.RingSettings[i], 26);
                this.RotorPositions[i] = EnigmaUtilities.Resources.Mod(this.RotorPositions[i], 26);

                // Display correct rotor positon
                Label rotorPosition = (Label)this.FindName(string.Format("RotorPositionDsp{0}", i));
                rotorPosition.Content = this.RotorPositions[i].ToChar().ToString().ToUpper();

                // Display correct ring settings
                Label ringSetting = (Label)this.FindName(string.Format("RingSettingDsp{0}", i));
                ringSetting.Content = this.RingSettings[i] + 1;

                // Display the correct name 
                Label rotor = (Label)this.FindName(string.Format("Rotor{0}", i));
                rotor.Content = this.SetDisplayLength(this.RotorData[i].Name, 40);
            }

            // Display correct plugboard settings
            for (int i = 0; i < 10; i++)
            {
                TextBox setting = (TextBox)this.FindName(string.Format("Plugboard{0}", i));
                setting.Text = this.PlugboardSettings[i];
            }

            // Display the correct reflector name
            this.Reflector.Content = this.SetDisplayLength(this.ReflectorData.Name, 40);

            // Display correct checkbox for four rotors
            this.FourRotorCheckBox.IsChecked = this.FourRotors;
        }

        /// <summary>
        /// Removes repeated characters across the plug board.
        /// </summary>
        /// <param name="text"> The text in the plug board text box. </param>
        /// <param name="textBoxIndex"> The index of the plug board text box. </param>
        /// <returns> The new string in the text box after repeats are removed. </returns>
        private string RemovePlugboardRepeats(string text, int textBoxIndex)
        {
            // Iterate over plug board settings
            for (int i = 0; i < text.Length; i++)
            {
                // Check if the character has already been used
                for (int j = 0; j < 10; j++)
                {
                    if (j == textBoxIndex)
                    {
                        // Don't check the text box being changed
                        continue;
                    }
                    else
                    {
                        // Check if the text box being check contians the letter
                        TextBox checkingTextbox = (TextBox)this.FindName(string.Format("Plugboard{0}", j.ToString()));
                        if (checkingTextbox.Text.Contains(text[i].ToString()))
                        {
                            // Remove the letter if the textbox contains it
                            text = text.Remove(i, 1);
                            i--;
                            break;
                        }
                    }
                }
            }

            return text;
        }

        /// <summary>
        /// Sets the length of a text to fit a certain length.
        /// </summary>
        /// <param name="text"> The text to change. </param>
        /// <param name="length"> The desired length. </param>
        /// <returns> The text set to the correct length. </returns>
        private string SetDisplayLength(string text, int length)
        {
            // Check if the string is too long
            if (text.Length > length)
            {
                // Set the length and add dots on the end
                text = text.Substring(0, length - 3);
                text += "...";
            }

            return text;
        }

        /// <summary>
        /// Opens a file dialog and reads the contents of the file selected by the user.
        /// </summary>
        /// <returns> The contents of the file selected. </returns>
        private string OpenFileFromUser()
        {
            // Create a default directory to store component if it does not exist
            string path = Directory.GetCurrentDirectory() + "\\Components";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Create the save dialog
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.RestoreDirectory = true;
            ofd.InitialDirectory = path;
            ofd.Filter = "XML|*.xml|All files|*.*";
            ofd.Title = "Open Component";

            // Open the dialog
            string text = string.Empty;
            if ((bool)ofd.ShowDialog(this))
            {
                // Read the contents of the selected file
                try
                {
                    text = File.ReadAllText(ofd.FileName);
                }
                catch
                {
                    MessageBoxResult msg = MessageBox.Show(this, "The component failed to load!", "Error!");
                }
            }

            return text;
        }

        /// <summary>
        /// Parses the contents of a xml file.
        /// </summary>
        /// <param name="rotorIndex"> The index of the rotor. </param>
        /// <param name="text"> The content of the xml file. </param>
        /// <remarks> If the rotor index is lower than 0 it is classed as a reflector. </remarks>
        private void ParseXMLFile(int rotorIndex, string text)
        {
            // Only run if the xml exists
            if (text != string.Empty)
            {
                // Parse the xml
                XElement xml;
                try
                {
                    xml = XElement.Parse(text);
                }
                catch (XmlException)
                {
                    MessageBoxResult msg = MessageBox.Show(this, "The XML in invalid!", "Error!");
                    return;
                }

                // Turn the xml into reflector/rotor data and save
                if (rotorIndex < 0)
                {
                    ReflectorData data = xml.ToReflectorData();
                    if (data == null)
                    {
                        MessageBoxResult msg = MessageBox.Show(this, "The XML is invalid!", "Error!");
                    }
                    else
                    {
                        this.ReflectorData = data;
                    }
                }
                else
                {
                    RotorData data = xml.ToRotorData();
                    if (data == null)
                    {
                        MessageBoxResult msg = MessageBox.Show(this, "The XML is invalid!", "Error!");
                    }
                    else
                    {
                        this.RotorData[rotorIndex] = data;
                    }
                }
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
            int firstRotor = this.FourRotors ? 0 : 1; // Get the first rotor number
            Rotor[] rotors = new Rotor[4 - firstRotor]; // Create an array of rotors
            for (int i = firstRotor; i < 4; i++)
            {
                // Generate the rotor
                rotors[i - firstRotor] = new Rotor(this.RingSettings[i], this.RotorPositions[i], this.RotorData[i].Wiring, this.RotorData[i].TunringNotches, i - firstRotor);
            }

            // Create an instance of the reflector and plugboard
            Reflector reflector = new Reflector(ReflectorData.Wiring);
            Plugboard plugboard = new Plugboard(this.PlugboardSettings);

            // Use settings to create enigma machine instance
            EnigmaMachine em = new EnigmaMachine(reflector, rotors, plugboard);

            // Create encryption window
            EnigmaWriter ew = new EnigmaWriter(em);

            // Show encryptor, hide settings
            ew.Show();
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
            this.FourRotors = (bool)this.FourRotorCheckBox.IsChecked;

            // Display correct rotors
            if (this.FourRotors)
            {
                int currentFourthRotorIndex = this.RingSettingsActive ? 0 : 4; // Get the currently displayed fourth rotor
                this.RotorSettingsGrid.ColumnDefinitions[currentFourthRotorIndex].Width = new GridLength(1, GridUnitType.Star);
                this.ComponentGrid.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
            }
            else
            {
                this.RotorSettingsGrid.ColumnDefinitions[0].Width = new GridLength(0);
                this.RotorSettingsGrid.ColumnDefinitions[4].Width = new GridLength(0);
                this.ComponentGrid.RowDefinitions[2].Height = new GridLength(0);
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
                if (sender.Equals(this.FindName(string.Format("RotorPositionDec{0}", i))))
                {
                    // If the button is a decrease rotor position, decrease the rotor position
                    this.RotorPositions[i]--;
                    break;
                }
                else if (sender.Equals(this.FindName(string.Format("RotorPositionInc{0}", i))))
                {
                    // If the button is a increase rotor position, increase the rotor position
                    this.RotorPositions[i]++;
                    break;
                }
                else if (sender.Equals(this.FindName(string.Format("RingSettingDec{0}", i))))
                {
                    // If the button is a decrease ring setting, decrease the rotor position
                    this.RingSettings[i]--;
                    break;
                }
                else if (sender.Equals(this.FindName(string.Format("RingSettingInc{0}", i))))
                {
                    // If the button is a increase ring setting, increase the rotor position
                    this.RingSettings[i]++;
                    break;
                }
            }

            // Display the correct settings
            this.DisplayCorrectSettings();
        }

        /// <summary>
        /// Handles the change of text in a plug board text box.
        /// </summary>
        /// <param name="sender"> The origin of the event. </param>
        /// <param name="e"> The event arguments. </param>
        private void Plugboard_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Convert the origin of the event to a text box
            TextBox senderTextbox = (TextBox)sender;

            // Get the current selected index and make the string upper case
            int selectedIndex = senderTextbox.SelectionStart; 
            string text = senderTextbox.Text.ToUpper();

            // Filter out non-alphabetic characters remove duplicates and set length to 2
            text = Regex.Replace(text, "[^A-Z]", string.Empty);
            text = EnigmaUtilities.Resources.RemoveDuplicateCharacters(text);
            text = text.Length > 2 ? text.Substring(0, 2) : text;

            // Get the index of the current textbox
            int textBoxIndex = 0;
            for (int i = 0; i < 10; i++)
            {
                if (this.FindName(string.Format("Plugboard{0}", i)).Equals(sender))
                {
                    textBoxIndex = i;
                }
            }

            // Remove repeated characters across the plugboard
            text = this.RemovePlugboardRepeats(text, textBoxIndex);

            // Store new plugboard setting
            this.PlugboardSettings[textBoxIndex] = text;
            senderTextbox.Text = text;

            // Reset the selected index
            senderTextbox.SelectionStart = selectedIndex;
        }

        /// <summary>
        /// Handles the click of the clear settings button.
        /// </summary>
        /// <param name="sender"> The origin of the event. </param>
        /// <param name="e"> The event arguments. </param>
        private void ClearSettings_Click(object sender, RoutedEventArgs e)
        {
            this.ResetData();
        }

        /// <summary>
        /// Handles the click of the create new component button.
        /// </summary>
        /// <param name="sender"> The origin of the event. </param>
        /// <param name="e"> The event arguments. </param>
        private void CreateNewComponent_Click(object sender, RoutedEventArgs e)
        {
            CreateComponent cc = new CreateComponent();
            cc.Show();
        }

        /// <summary>
        /// Handles the click of the browse rotor buttons.
        /// </summary>
        /// <param name="sender"> The origin of the event. </param>
        /// <param name="e"> The event arguments. </param>
        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            // Get button index
            int rotorIndex = -1;
            for (int i = 0; i < 4; i++)
            {
                if (this.FindName(string.Format("BrowseRotor{0}", i)).Equals(sender))
                {
                    // If the sender is a rotor browse button tell the program which one it is and that its not a reflector
                    rotorIndex = i;
                }
            }

            // Get the contents of a file from the user
            string text = this.OpenFileFromUser();

            // Parse the file
            this.ParseXMLFile(rotorIndex, text);
        }
    }
}
