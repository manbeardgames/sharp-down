using System;
using System.Collections.Generic;
using System.Linq;

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

        public TypeMemberModel this[string memberName]
        {
            get 
            {
                if(string.IsNullOrEmpty(memberName) || Types.Count == 0)
                {
                    throw new IndexOutOfRangeException();
                }
                else
                {
                    return Types.FirstOrDefault(x => x.Member.NameID == memberName);
                }
            }
        }
    }
}