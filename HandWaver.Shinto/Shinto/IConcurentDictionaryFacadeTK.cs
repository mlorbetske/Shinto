using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinto
{
    /// <summary>
    /// A facade for concurrent dictionaries
    /// </summary>
    /// <typeparam name="TKey">The type of the key of the dictionary</typeparam>
    /// <typeparam name="TValue">The type of the value of the dictionary</typeparam>
    public interface IConcurrentDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        /// <summary>
        /// Gets a value that indicates whether the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2"/> is empty.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2"/> is empty; otherwise, false.
        /// </returns>
        bool IsEmpty { get; }

        /// <summary>
        /// Attempts to add the specified key and value to the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2"/>.
        /// </summary>
        /// <returns>
        /// true if the key/value pair was added to the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2"/> successfully. If the key already exists, this method returns false.
        /// </returns>
        /// <param name="key">The key of the element to add.</param><param name="value">The value of the element to add. The value can be a null reference (Nothing in Visual Basic) for reference types.</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null reference (Nothing in Visual Basic).</exception><exception cref="T:System.OverflowException">The dictionary already contains the maximum number of elements, <see cref="F:System.Int32.MaxValue"/>.</exception>
        bool TryAdd(TKey key, TValue value);


        /// <summary>
        /// Attempts to remove and return the value with the specified key from the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2"/>.
        /// </summary>
        /// <returns>
        /// true if an object was removed successfully; otherwise, false.
        /// </returns>
        /// <param name="key">The key of the element to remove and return.</param><param name="value">When this method returns, <paramref name="value"/> contains the object removed from the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2"/> or the default value of if the operation failed.</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is a null reference (Nothing in Visual Basic).</exception>
        bool TryRemove(TKey key, out TValue value);

        /// <summary>
        /// Compares the existing value for the specified key with a specified value, and if they are equal, updates the key with a third value.
        /// </summary>
        /// <returns>
        /// true if the value with <paramref name="key"/> was equal to <paramref name="comparisonValue"/> and replaced with <paramref name="newValue"/>; otherwise, false.
        /// </returns>
        /// <param name="key">The key whose value is compared with <paramref name="comparisonValue"/> and possibly replaced.</param><param name="newValue">The value that replaces the value of the element with <paramref name="key"/> if the comparison results in equality.</param><param name="comparisonValue">The value that is compared to the value of the element with <paramref name="key"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is a null reference.</exception>
        bool TryUpdate(TKey key, TValue newValue, TValue comparisonValue);
  
        /// <summary>
        /// Copies the key and value pairs stored in the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2"/> to a new array.
        /// </summary>
        /// <returns>
        /// A new array containing a snapshot of key and value pairs copied from the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2"/>.
        /// </returns>
        KeyValuePair<TKey, TValue>[] ToArray();
  
        /// <summary>
        /// Adds a key/value pair to the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2"/> if the key does not already exist.
        /// </summary>
        /// <returns>
        /// The value for the key. This will be either the existing value for the key if the key is already in the dictionary, or the new value for the key as returned by valueFactory if the key was not in the dictionary.
        /// </returns>
        /// <param name="key">The key of the element to add.</param><param name="valueFactory">The function used to generate a value for the key</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is a null reference (Nothing in Visual Basic).-or-<paramref name="valueFactory"/> is a null reference (Nothing in Visual Basic).</exception><exception cref="T:System.OverflowException">The dictionary already contains the maximum number of elements, <see cref="F:System.Int32.MaxValue"/>.</exception>
        TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory);
  
        /// <summary>
        /// Adds a key/value pair to the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2"/> if the key does not already exist.
        /// </summary>
        /// <returns>
        /// The value for the key. This will be either the existing value for the key if the key is already in the dictionary, or the new value if the key was not in the dictionary.
        /// </returns>
        /// <param name="key">The key of the element to add.</param><param name="value">the value to be added, if the key does not already exist</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is a null reference (Nothing in Visual Basic).</exception><exception cref="T:System.OverflowException">The dictionary already contains the maximum number of elements, <see cref="F:System.Int32.MaxValue"/>.</exception>
        TValue GetOrAdd(TKey key, TValue value);

        /// <summary>
        /// Adds a key/value pair to the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2"/> if the key does not already exist, or updates a key/value pair in the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2"/> if the key already exists.
        /// </summary>
        /// <returns>
        /// The new value for the key. This will be either be the result of addValueFactory (if the key was absent) or the result of updateValueFactory (if the key was present).
        /// </returns>
        /// <param name="key">The key to be added or whose value should be updated</param><param name="addValueFactory">The function used to generate a value for an absent key</param><param name="updateValueFactory">The function used to generate a new value for an existing key based on the key's existing value</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is a null reference (Nothing in Visual Basic).-or-<paramref name="addValueFactory"/> is a null reference (Nothing in Visual Basic).-or-<paramref name="updateValueFactory"/> is a null reference (Nothing in Visual Basic).</exception><exception cref="T:System.OverflowException">The dictionary already contains the maximum number of elements, <see cref="F:System.Int32.MaxValue"/>.</exception>
        TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory);
  
        /// <summary>
        /// Adds a key/value pair to the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2"/> if the key does not already exist, or updates a key/value pair in the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2"/> if the key already exists.
        /// </summary>
        /// <returns>
        /// The new value for the key. This will be either be addValue (if the key was absent) or the result of updateValueFactory (if the key was present).
        /// </returns>
        /// <param name="key">The key to be added or whose value should be updated</param><param name="addValue">The value to be added for an absent key</param><param name="updateValueFactory">The function used to generate a new value for an existing key based on the key's existing value</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is a null reference (Nothing in Visual Basic).-or-<paramref name="updateValueFactory"/> is a null reference (Nothing in Visual Basic).</exception><exception cref="T:System.OverflowException">The dictionary already contains the maximum number of elements, <see cref="F:System.Int32.MaxValue"/>.</exception>
        TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory);
    }
}


