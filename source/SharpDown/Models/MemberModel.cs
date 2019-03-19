using SharpDown.Interfaces;
using System.Collections.Generic;

namespace SharpDown.Models
{
    public abstract class MemberModel
    {
        public string Assembly {get; set;}
        public string NameSpace {get; set;}
        public char MemberType {get; set;}
        public string Name {get; set;}
    }
}