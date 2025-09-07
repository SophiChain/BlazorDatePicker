using System.Text;

namespace SophiChain.BlazorDatePicker.Utilities
{
    /// <summary>
    /// Represents a builder for creating CSS classes used in a component.
    /// </summary>
    public struct CssBuilder
    {
        private StringBuilder? _stringBuilder;

        /// <summary>
        /// Creates a new instance of CssBuilder with the specified initial value.
        /// </summary>
        public static CssBuilder Default(string value) => new(value);

        /// <summary>
        /// Creates an empty instance of CssBuilder.
        /// </summary>
        public static CssBuilder Empty() => new();

        /// <summary>
        /// Creates an empty instance of CssBuilder.
        /// </summary>
        public CssBuilder()
        {
            _stringBuilder = new StringBuilder();
        }

        /// <summary>
        /// Initializes a new instance of the CssBuilder class with the specified initial value.
        /// </summary>
        public CssBuilder(string? value) : this()
        {
            if (value is not null)
            {
                _stringBuilder.Append(value);
            }
        }

        /// <summary>
        /// Adds a raw string to the builder that will be concatenated with the next class or value added to the builder.
        /// </summary>
        public CssBuilder AddValue(string? value)
        {
            if (value is not null)
            {
                _stringBuilder.Append(value);
            }
            return this;
        }

        /// <summary>
        /// Adds a CSS class to the builder with a space separator.
        /// </summary>
        public CssBuilder AddClass(string? value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (_stringBuilder.Length > 0)
                    _stringBuilder.Append(' ');
                _stringBuilder.Append(value);
            }
            return this;
        }

        /// <summary>
        /// Adds a conditional CSS class to the builder with a space separator.
        /// </summary>
        public CssBuilder AddClass(string? value, bool when) => when ? AddClass(value) : this;

        /// <summary>
        /// Adds a conditional CSS class to the builder with space separator.
        /// </summary>
        public CssBuilder AddClass(string? value, bool? when) => when == true ? AddClass(value) : this;

        /// <summary>
        /// Adds a conditional CSS class to the builder with a space separator.
        /// </summary>
        public CssBuilder AddClass(string? value, Func<bool>? when) => AddClass(value, when is not null && when());

        /// <summary>
        /// Adds a conditional CSS class to the builder with a space separator.
        /// </summary>
        public CssBuilder AddClass(Func<string?> value, bool when = true) => when ? AddClass(value()) : this;

        /// <summary>
        /// Adds a conditional CSS class to the builder with a space separator.
        /// </summary>
        public CssBuilder AddClass(Func<string?> value, Func<bool>? when = null) => AddClass(value, when is not null && when());

        /// <summary>
        /// Adds a conditional nested CssBuilder to the builder with a space separator.
        /// </summary>
        public CssBuilder AddClass(CssBuilder builder, bool when = true) => when ? AddClass(builder.Build()) : this;

        /// <summary>
        /// Adds a conditional CSS class to the builder with a space separator.
        /// </summary>
        public CssBuilder AddClass(CssBuilder builder, Func<bool>? when = null) => AddClass(builder, when is not null && when());

        /// <summary>
        /// Finalizes the completed CSS classes as a string.
        /// </summary>
        public string Build() => _stringBuilder.ToString().Trim();

        /// <inheritdoc />
        public override string ToString() => Build();
    }
}
