using System.Collections.Generic;

namespace SharpDown.Models
{
    public class TypeMemberModel : MemberModel
    {
        public List<MethodModel> Constructors {get; set;}
        public List<FieldMemberModel> Fields {get; set;}
        public List<PropertyMemberModel> Properties {get; set;}
        public List<EventMemberModel> Events {get; set;}
        public List<MethodMemberModel> Methods {get; set;}
    }
}