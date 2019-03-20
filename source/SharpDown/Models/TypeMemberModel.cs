using System.Collections.Generic;

namespace SharpDown.Models
{
    /// <summary>
    ///     Represents an type member type
    /// </summary>
    public class TypeMemberModel : MemberModel
    {

        /// <summary>
        ///     Gets or Sets the constructor method members
        /// </summary>
        public List<MethodMemberModel> Constructors { get; set; }

        /// <summary>
        ///     Gets or Sets the field members
        /// </summary>
        public List<FieldMemberModel> Fields { get; set; }

        /// <summary>
        ///     Gets or Sets the property members
        /// </summary>
        public List<PropertyMemberModel> Properties { get; set; }

        /// <summary>
        ///     Gets or Sets the event members
        /// </summary>
        public List<EventMemberModel> Events { get; set; }

        /// <summary>
        ///     Gets or Sets the non-constructor method members
        /// </summary>
        public List<MethodMemberModel> Methods { get; set; }
    }
}