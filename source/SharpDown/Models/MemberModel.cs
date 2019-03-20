using SharpDown.Interfaces;
using System.Collections.Generic;

namespace SharpDown.Models
{
    /// <summary>
    ///     Represents a member within an assembly
    /// </summary>    
    public class MemberModel
    {
        /// <summary>
        ///     Gets or Sets the name of the assembly this member belongs to
        /// </summary>   
        public string Assembly {get; set;}

        /// <summary>
        ///     Gets or Sets the namespace of this member
        /// </summary>   
        public string NameSpace {get; set;}

        /// <summary>
        ///     Gets or Sets the character that represents the type of member
        /// </summary>   
        public char MemberType {get; set;}

        /// <summary>
        ///     Gets or Sets the name id of this member
        /// </summary>   
        public string Name {get; set;}
    }
}