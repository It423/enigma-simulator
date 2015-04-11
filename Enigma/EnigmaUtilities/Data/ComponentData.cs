// ComponentData.cs
// <copyright file="ComponentData.cs"> This code is protected under the MIT License. </copyright>
namespace EnigmaUtilities.Data
{
    /// <summary>
    /// A class that holds data about components to be used.
    /// </summary>
    public abstract class ComponentData
    {        
        /// <summary>
        /// Gets or sets the name of the component.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the wiring to be used for this component.
        /// </summary>
        public string Wiring { get; set; }
    }
}
