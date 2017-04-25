using System;
using System.Linq;
using System.Reflection;

namespace sfa.Roatp.Register.ApiConsumerTests
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PactProviderAttribute : PactAttribute
    {
        public readonly string ProviderName;

        public PactProviderAttribute(string providerName)
        {
            ProviderName = providerName;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class PactConsumerAttribute : PactAttribute
    {
        public readonly string ConsumerName;

        public PactConsumerAttribute(string consumerName)
        {
            ConsumerName = consumerName;
        }
    }


    public abstract class PactAttribute : Attribute
    {
        public static T GetName<T>(object source)
        {
            var info = source.GetType();
            var attributes = info.GetCustomAttributes(true).OfType<T>().ToList();
            if (!attributes.Any())
            {
                throw new CustomAttributeFormatException("Missing the PactAttribute to specify the name of the provider / consumer");
            }

            return attributes.FirstOrDefault();
        }
    }

}
