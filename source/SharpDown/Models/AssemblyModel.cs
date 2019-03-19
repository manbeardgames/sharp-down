using System.Collections.Generic;

namespace SharpDown.Models
{
    public class AssemblyModel
    {
        public string Name {get; set;}
        public List<TypeMemberModel> Types {get; set;}

        public AssemblyModel()
        {
            Types = new List<TypeMemberModel>();
        }
    }
}