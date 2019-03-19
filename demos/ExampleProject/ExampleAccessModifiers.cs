using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleProject
{
    /// <summary>
    ///     This is an example of a class
    /// </summary>
    public class ExampleAccessModifiers
    {
        /// <summary>
        ///     A private int field
        /// </summary>
        private int _privateIntField;

        /// <summary>
        ///     A protected int field
        /// </summary>
        protected int _protectedIntField;

        /// <summary>
        ///     A public int field
        /// </summary>
        public int _publicIntField;


        /// <summary>
        ///     A private readonly int field
        /// </summary>
        private readonly int _privateReadOnlyIntField;

        /// <summary>
        ///     A protected readonly int field
        /// </summary>
        protected readonly int _protectedReadOnlyIntField;

        /// <summary>
        ///     A public readonly int field
        /// </summary>
        public readonly int _publicReadOnlyIntField;


        /// <summary>
        ///     A private const int field set to 10
        /// </summary>
        private const int _privateConstIntField = 10;

        /// <summary>
        ///     A protected const int field set to 11
        /// </summary>
        protected const int _protectedConstIntField = 11;

        /// <summary>
        ///     A public const int field set to 12
        /// </summary>
        public const int _publicConstIntField = 12;


        /// <summary>
        ///     A private static int field
        /// </summary>
        private static int _privateStaticIntField;

        /// <summary>
        ///     A protected static int field
        /// </summary>
        protected static int _protectedStaticIntField;

        /// <summary>
        ///     A public static int field
        /// </summary>
        public static int _publicStaticIntField;


        /// <summary>
        ///     A private static readonly int field
        /// </summary>
        private static readonly int _privateStaticReadonlyIntField;

        /// <summary>
        ///     A protected static readonly int field
        /// </summary>
        protected static readonly int _protectedStaticReadonlyIntField;

        /// <summary>
        ///     A public static readonly int field
        /// </summary>
        public static readonly int _publicStaticReadonlyIntField;


        /// <summary>
        ///     A private int property
        /// </summary>
        private int PrivateIntProperty { get; set; }

        /// <summary>
        ///     A protected int property
        /// </summary>
        protected int ProtectedIntProperty { get; set; }

        /// <summary>
        ///     A public int property
        /// </summary>
        public int PublicIntProperty { get; set; }

        /// <summary>
        ///     A public int property with a private setter
        /// </summary>
        public int PublicPrivateIntProperty { get; private set; }

        /// <summary>
        ///     A public int property with a private getter.
        /// </summary>
        /// <see cref="int"/>
        public int PrivatePublicIntProperty { private get; set; }

        /// <summary>
        ///     Constructor for example access modifiers
        /// </summary>
        public ExampleAccessModifiers()
        {

        }

        /// <summary>
        ///     Overload constructor for example access modifers
        /// </summary>
        /// <param name="i">Int parameter </param>
        public ExampleAccessModifiers(int i)
        {

        }


    }
}
