namespace T4Generators.Database
{
    /// <summary>
    /// Defines interface for building method body.
    /// </summary>
    internal interface IMethodBuilder
    {
        /// <summary>
        /// Gets the method body without definition and curly braces.
        /// </summary>
        string GetMethodBody();
    }
}