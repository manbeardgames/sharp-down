namespace SharpDown.Models
{
    /// <summary>
    ///     Represents an field member type
    /// </summary>            
    public class FieldMemberModel : MemberModel
    {
        public readonly MemberModel Member;

        public FieldMemberModel(MemberModel member)
        {
            Member = member;
        }
        
    }
}