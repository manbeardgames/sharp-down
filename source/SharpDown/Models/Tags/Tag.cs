using System.Collections.Generic;

namespace SharpDown.Models.Tags
{
    /// <summary>
    ///     Represents the tags associated with a member such as
    ///     summary, params, returns, example and remarks
    /// </summary>
    public class Tag
    {
        /// <summary>
        ///     The value of the tag
        /// </summary>
        public string Value;

        /// <summary>
        ///     THe name of the tag (e.g. summary)
        /// </summary>
        public string Name;

        /// <summary>
        ///     Any tags that are children of this tag. (e.g. para or code tags)
        /// </summary>
        public List<Tag> Children;

        /// <summary>
        ///     Creates a new tag instance
        /// </summary>
        public Tag()
        {
            Children = new List<Tag>();
        }
    }
}