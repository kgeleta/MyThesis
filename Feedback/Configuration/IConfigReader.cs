namespace Feedback.Configuration
{
    public interface IConfigReader
    {
        /// <summary>
        /// Reads single value of first node named <param name="nodeName"/>.
        /// </summary>
        /// <param name="nodeName">Name of node to read.</param>
        /// <param name="attribute">Name of attribute to read.</param>
        /// <returns>Node's value as string.</returns>
        string ReadSingleNode(string nodeName, string attribute);

        /// <summary>
        /// Reads values of all nodes named <param name="nodeName"/>.
        /// </summary>
        /// <param name="nodeName">Name of nodes to read.</param>
        /// <param name="attribute">Name of attribute to read.</param>
        /// <returns>All node's values as array of string.</returns>
        string[] ReadArrayNode(string nodeName, string attribute);
    
    }
}