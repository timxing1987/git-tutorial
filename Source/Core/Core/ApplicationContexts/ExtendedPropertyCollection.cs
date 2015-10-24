using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Cedar.Core.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Cedar.Core.ApplicationContexts
{
    /// <summary>
    ///     Define the context item's extended property collection.
    /// </summary>
    [CollectionDataContract(Namespace = "http://www.Cedar.co/", ItemName = "Property", KeyName = "Key",
        ValueName = "Value", IsReference = true)]
    [Serializable]
    public class ExtendedPropertyCollection : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>,
        IEnumerable<KeyValuePair<string, object>>, IEnumerable
    {
        /// <summary>
        /// </summary>
        private readonly Dictionary<string, object> innerDictionary = new Dictionary<string, object>();

        /// <summary>
        ///     Initializes a new instance of the ExtendedPropertyCollection class.
        /// </summary>
        public ExtendedPropertyCollection()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the ExtendedPropertyCollection class.
        /// </summary>
        /// <param name="contextItem">The context item.</param>
        public ExtendedPropertyCollection(ContextItem contextItem)
        {
            Guard.ArgumentNotNull(contextItem, "contextItem");
            ContextItem = contextItem;
        }

        /// <summary>
        ///     Gets or sets the context item.
        /// </summary>
        /// <value>The context item.</value>
        public ContextItem ContextItem { get; set; }

        /// <summary>
        ///     Gets the keys.
        /// </summary>
        /// <value>The keys.</value>
        public ICollection<string> Keys
        {
            get { return innerDictionary.Keys; }
        }

        /// <summary>
        ///     Gets the values.
        /// </summary>
        /// <value>The values.</value>
        public ICollection<object> Values
        {
            get { return innerDictionary.Values; }
        }

        /// <summary>
        ///     Gets or sets the <see cref="T:System.Object" /> with the specified key.
        /// </summary>
        /// <value>The value.</value>
        public object this[string key]
        {
            get { return innerDictionary.ContainsKey(key) ? innerDictionary[key] : null; }
            set
            {
                EnsureCanWrite();
                innerDictionary[key] = value;
            }
        }

        /// <summary>
        ///     Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count
        {
            get { return innerDictionary.Count; }
        }

        /// <summary>
        ///     Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is read only; otherwise, <c>false</c>.
        /// </value>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        ///     Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(string key, object value)
        {
            EnsureCanWrite();
            innerDictionary[key] = value;
        }

        /// <summary>
        ///     Determines whether the specified key contains key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///     <c>true</c> if the specified key contains key; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsKey(string key)
        {
            return innerDictionary.ContainsKey(key);
        }

        /// <summary>
        ///     Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            EnsureCanWrite();
            return innerDictionary.Remove(key);
        }

        /// <summary>
        ///     Tries the get value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public bool TryGetValue(string key, out object value)
        {
            return innerDictionary.TryGetValue(key, out value);
        }

        /// <summary>
        ///     Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(KeyValuePair<string, object> item)
        {
            EnsureCanWrite();
            innerDictionary.Add(item.Key, item.Value);
        }

        /// <summary>
        ///     Clears this instance.
        /// </summary>
        public void Clear()
        {
            EnsureCanWrite();
            innerDictionary.Clear();
        }

        /// <summary>
        ///     Determines whether [contains] [the specified item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///     <c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(KeyValuePair<string, object> item)
        {
            return innerDictionary.Contains(item);
        }

        /// <summary>
        ///     Copies to.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            var array2 = innerDictionary.ToArray();
            for (var i = arrayIndex; i < Math.Min(innerDictionary.Count, array.Length); i++)
            {
                array[i] = array2[i + arrayIndex];
            }
        }

        /// <summary>
        ///     Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<string, object> item)
        {
            EnsureCanWrite();
            return innerDictionary.Remove(item.Key);
        }

        /// <summary>
        ///     Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return innerDictionary.GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            IEnumerable enumerable = innerDictionary;
            return enumerable.GetEnumerator();
        }

        private void EnsureCanWrite()
        {
            if (ContextItem != null && ContextItem.ReadOnly)
            {
                throw new InvalidOperationException(
                    ResourceUtility.Format(Resources.ExceptionCannotModifyReadonlyProperties));
            }
        }
    }
}