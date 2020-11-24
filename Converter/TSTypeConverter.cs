using System;
using System.Collections.Generic;
using System.Text;
using TSCodeBuilder.Helpers;

namespace TSCodeBuilder.Converter
{
    public class TSTypeConverter
    {

        // Simple Types 
        // TODO: Incomplete
        public static Dictionary<Type, string> PrimitiveTypes = new Dictionary<Type, string>
        {
            [typeof(int)] = "number",
            [typeof(IntPtr)] = "number",
            [typeof(float)] = "number",
            [typeof(double)] = "number",
            [typeof(bool)] = "boolean",
            [typeof(byte)] = "boolean",
            [typeof(string)] = "string",
            [typeof(void)] = "void",
            [typeof(byte)] = "????", // TODO!
        };

        public Dictionary<Type, string> TypeMapExtensions { get; } = new Dictionary<Type, string>();

        // TODO: Incomplete
        public Dictionary<Type, Func<Type, string>> GenericCollectionTypeConverters => new Dictionary<Type, Func<Type, string>>
        {
            [typeof(List<>)] = SequenceConverter,
            [typeof(IList<>)] = SequenceConverter,
            [typeof(IEnumerable<>)] = SequenceConverter,
            [typeof(Dictionary<,>)] = DictionaryConverter,
            [typeof(Array)] = ArrayConverter
        };

        public TSTypeConverter()
        {
        }

        public TSTypeConverter(Dictionary<Type, string> extensions)
        {
            extensions.ThrowIfArgumentNull("Failed to create py type converter because type map extension dict was null");
            TypeMapExtensions = extensions;
        }

        public string Convert(Type type)
        {
            // Check if primitive
            if (PrimitiveTypes.ContainsKey(type))
            {
                return PrimitiveTypes[type];
            }

            // Check if public facing type
            else if (TypeMapExtensions.ContainsKey(type))
            {
                return TypeMapExtensions[type];
            }

            else if (type.IsArray)
            {
                return ArrayConverter(type);
            }

            // Check if generic collection type
            else if (type.IsGenericType)
            {
                var genType = type.GetGenericTypeDefinition();
                if (GenericCollectionTypeConverters.ContainsKey(genType))
                {
                    var converter = GenericCollectionTypeConverters[genType];
                    return converter(type);
                }
                return null;
            }

            else
            {
                // unable to provide a type hint
                return null;
            }
        }

        private string ArrayConverter(Type type)
        {
            var arTyep = Convert(type.GetElementType());
            return $"Array<{arTyep}>";
        }

        private string DictionaryConverter(Type type)
        {
            var key = type.GetGenericArguments()[0];
            var val = type.GetGenericArguments()[1];
            var tsKey = Convert(key);
            var tsVal = Convert(val);
            return $"Record<{tsKey}, {tsVal}>";
        }

        public string SequenceConverter(Type type)
        {
            var arg = type.GetGenericArguments()[0];
            var tsArg = Convert(arg);
            return $"Array<{tsArg}>";
        }
    }
}
