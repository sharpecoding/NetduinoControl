using System;
using System.Collections;

namespace MicroApiServer
{
    public class VariableCollection : IEnumerable
    {
        private readonly ArrayList _variables;

        public IEnumerable AllKeys
        {
            get
            {
                for (int i = 0; i < _variables.Count; i++)
                {
                    yield return ((Variable)_variables[i]).Name;
                }
            }
        }

        public object this[string key]
        {
            get { return Get(key); }
            set { Set(key, value); }
        }

        public VariableCollection()
        {
            _variables = new ArrayList();
        }

        public void Set(string name, object value)
        {
            Variable found = Find(name);
            if (found != null)
                found.Value = value;
            else
            {
                Variable variable = new Variable { Name = name, Value = value };
                _variables.Add(variable);
            }
        }

        public void Remove(string name)
        {
            Variable found = Find(name);

            if (found != null)
                _variables.Remove(found);
            else
                throw new Exception("Element not found");
        }

        private Variable Find(string name)
        {
            Variable found = null;
            foreach (object item in _variables)
            {
                Variable element = (Variable)item;
                if (element.Name == name)
                    found = element;
            }

            return found;
        }

        public object Get(string name)
        {
            Variable found = Find(name);

            if (found != null)
                return found.Value;
            else
                throw new Exception("Element not found");
        }

        public string GetString(string name)
        {
            return (string) Get(name);
        }

        public int GetInt(string name)
        {
            return (int) Get(name);
        }

        public class Variable
        {
            public string Name { get; set; }
            public object Value { get; set; }

            public int IntValue()
            {
                return (int) Value;
            }

            public string StringValue()
            {
                return (string) Value;
            }
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < _variables.Count; i++)
            {
                yield return _variables[i];
            }
        }
    }
}
