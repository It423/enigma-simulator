// CreateComponent.xaml.cs
// <copyright file="CreateComponent.xaml.cs"> This code is protected under the MIT License. </copyright>using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using EnigmaUtilities;

namespace Enigma
{
    /// <summary>
    /// Interaction logic for CreateComponent.xaml
    /// </summary>
    public partial class CreateComponent : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateComponent" /> class.
        /// </summary>
        public CreateComponent()
        {
            this.InitializeComponent();
            this.CorrospondingWiring = new int[26] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        }

        /// <summary>
        /// Gets or sets the array of wiring that corrosponds with other wiring while in reflector mode.
        /// </summary>
        private int[] CorrospondingWiring { get; set; }

        /// <summary>
        /// Prevents duplicate wiring.
        /// </summary>
        /// <param name="text"> The text in a wiring text box. </param>
        /// <param name="textBoxIndex"> The index of the text box. </param>
        /// <returns> The new text according to if it already is a used letter. </returns>
        private string StopDuplicateWiring(string text, int textBoxIndex)
        {
            // Iterate over each wiring connection
            for (int i = 0; i < 26; i++)
            {
                if (i == textBoxIndex)
                {
                    // Don't check the connection being changed
                    continue;
                }
                else if (((TextBox)this.FindName(i.ToChar(false).ToString())).Text == text)
                {
                    // If the wiring is the same as another box don't change it
                    text = string.Empty;
                    break;
                }
            }

            return text;
        }

        /// <summary>
        /// Changes to values of the wiring for reflector mode.
        /// </summary>
        /// <param name="text"> The text in the wiring text box. </param>
        /// <param name="textBoxIndex"> The index of the text box. </param>
        private void ReflectorWiringChanged(string text, int textBoxIndex)
        {
            // Data store of settings to change
            string otherHalfName = string.Empty;
            string otherHalfValue = string.Empty;

            if (text == string.Empty)
            {
                // Clear other half of connection if its being removed
                otherHalfName = this.CorrospondingWiring[textBoxIndex].ToChar(false).ToString();

                // Clear connections
                this.CorrospondingWiring[textBoxIndex] = -1;
                this.CorrospondingWiring[otherHalfName[0].ToInt()] = -1;
            }
            else
            {
                // Create connection if its being created
                otherHalfName = text;
                otherHalfValue = textBoxIndex.ToChar(false).ToString();

                // Delete other connections to this connection
                for (int i = 0; i < 26; i++)
                {
                    if (this.CorrospondingWiring[i] == textBoxIndex)
                    {
                        // Remove the wiring
                        this.CorrospondingWiring[i] = -1;
                        this.ChangeWiringText(i.ToChar(false).ToString(), string.Empty);
                    }
                }

                // Create connections
                this.CorrospondingWiring[textBoxIndex] = text[0].ToInt();
                this.CorrospondingWiring[text[0].ToInt()] = textBoxIndex;
            }

            // Change text
            this.ChangeWiringText(otherHalfName, otherHalfValue);
        }

        /// <summary>
        /// Changes the value of a wiring text box.
        /// </summary>
        /// <param name="wiringBoxName"> The name of the text box. </param>
        /// <param name="newValue"> The new value for it. </param>
        private void ChangeWiringText(string wiringBoxName, string newValue)
        {
            TextBox otherHalf = (TextBox)this.FindName(wiringBoxName);
            otherHalf.TextChanged -= this.Wiring_TextChanged; // Stop endless loop of calling this method
            otherHalf.Text = newValue;
            otherHalf.TextChanged += this.Wiring_TextChanged;
        }

        /// <summary>
        /// Handles the reflector check box being changed.
        /// </summary>
        /// <param name="sender"> The origin of the event. </param>
        /// <param name="e"> The event arguments. </param>
        private void ReflectorCheckBox_Changed(object sender, RoutedEventArgs e)
        {
            // Change the display according to the change
            if ((bool)this.ReflectorCheckBox.IsChecked)
            {
                this.SettingsGrid.RowDefinitions[2].Height = new GridLength(0);
            }
            else
            {
                this.SettingsGrid.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
            }

            // Reset settings
            this.CorrospondingWiring = new int[26] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
            this.TurningNotches.Text = string.Empty;
            for (int i = 0; i < 26; i++)
            {
                TextBox tb = (TextBox)this.FindName(i.ToChar().ToString().ToUpper());
                tb.Text = string.Empty;
            }
        }

        /// <summary>
        /// Handles the turning notches text changing.
        /// </summary>
        /// <param name="sender"> The origin of the event. </param>
        /// <param name="e"> The event arguments. </param>
        private void TurningNotches_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Get the text
            string text = ((TextBox)sender).Text.ToLower();

            // Make only alphabetic characters and remove duplicates
            text = Regex.Replace(text, "[^a-z]", string.Empty);
            text = EnigmaUtilities.Resources.RemoveDuplicateCharacters(text);

            // Order letters alphabetically
            string newText = string.Empty;
            for (int i = 0; i < 26; i++)
            {
                if (text.Contains(i.ToChar().ToString()))
                {
                    newText += i.ToChar();
                }
            }

            // Set the text to the ordered text
            text = newText.ToUpper();

            // Set the text and selection index
            ((TextBox)sender).Text = text;
            ((TextBox)sender).SelectionStart = text.Length;
        }

        /// <summary>
        /// Handles the wiring text changing.
        /// </summary>
        /// <param name="sender"> The origin of the event. </param>
        /// <param name="e"> The event arguments. </param>
        private void Wiring_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Get the text
            string text = ((TextBox)sender).Text.ToUpper();

            // Make only alphabetic characters, remove duplicates and set length to one
            text = Regex.Replace(text, "[^A-Z]", string.Empty);
            text = EnigmaUtilities.Resources.RemoveDuplicateCharacters(text);
            text = text.Length > 0 ? text.Substring(0, 1) : text;

            // Get the index of the textbox
            int textBoxIndex = 0;
            for (int i = 0; i < 26; i++)
            {
                if (this.FindName(i.ToChar(false).ToString()).Equals(sender))
                {
                    textBoxIndex = i;
                }
            }

            // Stop duplicates being created
            text = this.StopDuplicateWiring(text, textBoxIndex);

            // Set the corrosponding letter to the current letter if its in reflector mode
            if ((bool)this.ReflectorCheckBox.IsChecked)
            {
                this.ReflectorWiringChanged(text, textBoxIndex);
            }

            // Set the text and selection index
            ((TextBox)sender).Text = text;
            ((TextBox)sender).SelectionStart = text.Length;
        }
    }
}
