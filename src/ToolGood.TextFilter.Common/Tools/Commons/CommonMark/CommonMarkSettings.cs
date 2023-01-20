﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonMark
{
    /// <summary>
    /// Class used to configure the behavior of <see cref="CommonMarkConverter"/>.
    /// </summary>
    /// <remarks>This class is not thread-safe so any changes to a instance that is reused (for example, the 
    /// <see cref="CommonMarkSettings.Default"/>) has to be updated while it is not in use otherwise the
    /// behaviour is undefined.</remarks>
    public sealed class CommonMarkSettings
    {
        /// <summary>Initializes a new instance of the <see cref="CommonMarkSettings" /> class.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public CommonMarkSettings()
        { }


        /// <summary>
        /// Gets or sets a value indicating whether soft line breaks should be rendered as hard line breaks.
        /// </summary>
        public bool RenderSoftLineBreaksAsLineBreaks { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the parser tracks precise positions in the source data for
        /// block and inline elements. This is disabled by default because it incurs an additional performance cost to
        /// keep track of the original position.
        /// Setting this to <see langword="true"/> will populate <see cref="Syntax.Inline.SourcePosition"/>, 
        /// <see cref="Syntax.Inline.SourceLength"/>, <see cref="Syntax.Block.SourcePosition"/> and 
        /// <see cref="Syntax.Block.SourceLength"/> properties with correct information, otherwise the values
        /// of these properties are undefined.
        /// This also controls if these values will be written to the output.
        /// </summary>
        public bool TrackSourcePosition { get; set; }

        private CommonMarkAdditionalFeatures _additionalFeatures;

        /// <summary>
        /// Gets or sets any additional features (that are not present in the current CommonMark specification) that
        /// the parser and/or formatter will recognize.
        /// </summary>
        public CommonMarkAdditionalFeatures AdditionalFeatures
        {
            get { return this._additionalFeatures; }
            set { this._additionalFeatures = value; this._inlineParsers = null; this._inlineParserSpecialCharacters = null; }
        }


        private static readonly CommonMarkSettings _default = new CommonMarkSettings();

        /// <summary>
        /// The default settings for the converter. If the properties of this instance are modified, the changes will be applied to all
        /// future conversions that do not specify their own settings.
        /// </summary>
        public static CommonMarkSettings Default { get { return _default; } }

        #region [ Properties that cache structures used in the parsers ]

        private Func<Parser.Subject, Syntax.Inline>[] _inlineParsers;

        /// <summary>
        /// Gets the delegates that parse inline elements according to these settings.
        /// </summary>
        internal Func<Parser.Subject, Syntax.Inline>[] InlineParsers
        {
            get
            {
                var p = this._inlineParsers;
                if (p == null)
                {
                    p = Parser.InlineMethods.InitializeParsers(this);
                    this._inlineParsers = p;
                }

                return p;
            }
        }

        private char[] _inlineParserSpecialCharacters;

        /// <summary>
        /// Gets the characters that have special meaning for inline element parsers according to these settings.
        /// </summary>
        internal char[] InlineParserSpecialCharacters
        {
            get
            {
                var v = this._inlineParserSpecialCharacters;
                if (v == null)
                {
                    var p = this.InlineParsers;
                    var vs = new List<char>(20);
                    for (var i = 0; i < p.Length; i++)
                        if (p[i] != null)
                            vs.Add((char)i);

                    v = this._inlineParserSpecialCharacters = vs.ToArray();
                }

                return v;
            }
        }

        #endregion
    }
}
