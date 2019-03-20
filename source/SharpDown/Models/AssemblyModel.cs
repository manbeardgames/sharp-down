using System.Collections.Generic;

namespace SharpDown.Models
{
    /// <summary>
    ///     Represents an assembly
    /// </summary>        
    public class AssemblyModel
    {
        /// <summary>
        ///     Gets or Sets the name of the assembly
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or Sets the collection of type members in this assembly
        /// </summary>   
        public List<TypeMemberModel> Types { get; set; }


        /// <summary>
        ///     Creates a new instance of this
        /// </summary>   
        public AssemblyModel()
        {
            Types = new List<TypeMemberModel>();
        }
    }
}