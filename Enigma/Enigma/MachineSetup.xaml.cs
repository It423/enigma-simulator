// MachineSetup.xaml.cs
// <copyright file="MachineSetup.xaml.cs"> This code is protected under the MIT License. </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EnigmaUtilities;
using EnigmaUtilities.Components;

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
        }

        /// <summary>
        /// Beigns encryption using the current enigma settings.
        /// </summary>
        /// <param name="sender"> The origin of the event. </param>
        /// <param name="e"> The event arguments. </param>
        private void Begin_Click(object sender, RoutedEventArgs e)
        {
            // Use settings to create enigma machine instance
            ////EnigmaMachine em = new EnigmaMachine(...);

            // Create encryption window
            ////EnigmaWriter ew = new EnigmaWriter(em);

            // Show encryptor, hide settings
            ////ew.Show();
            this.Close();
        }
    }
}
