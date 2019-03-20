using System.Collections.Generic;
using SharpDown.Models.Tags;

namespace SharpDown.Models
{
    /// <summary type="class">
    ///     Represents a member within an assembly
    /// </summary>
    /// <sharpdown>
    ///     type: class
    ///     accessability: public
    ///     namespace: SharpDown.Models
    ///     name: MemberModel
    ///     inheritance: Object > MemberModel
    ///     lkjsdsdfjlkjkjkld
    /// </sharpdown>
    public class MemberModel
    {
        /// <summary>
        ///     Gets or Sets the assembly this member belongs too
        /// </summary>   
        /// 
        public AssemblyModel Assembly {get; set;}

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
        public string NameID {get; set;}

        /// <summary>
        ///     Gets or Sets the name of this member
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or Sets the collection of Tags for this member
        /// </summary>
        public List<Tag> Tags {get; set;}
    }
}