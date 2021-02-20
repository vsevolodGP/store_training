using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Contractors
{
    public class Form
    {
        public string ServiceName { get; }

        public int Step { get; }

        public bool IsFinal { get; }

        private readonly Dictionary<string, string> options;

        public IReadOnlyDictionary<string, string> Options => options;

        private readonly List<Field> fields;

        public IReadOnlyList<Field> Fields => fields;

        public Form (string serviceName,
                     int step,
                     bool isFinal,
                     IReadOnlyDictionary<string, string> options)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
                throw new ArgumentException(nameof(serviceName));

            if (step < 1)
                throw new ArgumentOutOfRangeException(nameof(step));

            ServiceName = serviceName;
            Step = step;
            IsFinal = isFinal;

            if (options == null)
                this.options = new Dictionary<string, string>();
            else
                this.options = options.ToDictionary(option => option.Key, option => option.Value);

            fields = new List<Field>();          
        }

        public static Form CreateFirst(string serviceName)
        {
            return new Form(serviceName, 1, isFinal: false, null);
        }

        public static Form CreateNext(string serviceName, int step, IReadOnlyDictionary<string, string> options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            return new Form(serviceName, step, isFinal: false, options);
        }

        public static Form CreateLast(string serviceName, int step, IReadOnlyDictionary<string, string> options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            return new Form(serviceName, step, isFinal: true, options);
        }

        public Form AddOption(string name, string value)
        {
            options.Add(name, value);

            return this;
        }

        public Form AddField(Field field)
        {
            fields.Add(field);

            return this;
        }

    }
}
