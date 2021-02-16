using System.Collections.Generic;

namespace Store.Contractors
{
    public class SelectionField : Field
    {

        public IReadOnlyDictionary<string, string> Options { get; }
        public SelectionField(string label, string name, string value, IReadOnlyDictionary<string, string> options) 
            : base (label, name, value)
        {
            Options = options;
        }
    }
}
