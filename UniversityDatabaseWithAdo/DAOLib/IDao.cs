using System.Collections.Generic;

namespace DAOLib
{
    /// <summary>
    /// generic interface that describes methods Dao objects.
    /// </summary>
    /// <typeparam name="T">the type of element for which the dao will be created.</typeparam>
    public interface IDao<T>
    {
        /// <summary>
        /// The method that is used to write data about the object.
        /// </summary>
        /// <param name="element">The object whose information will be recorded.</param>
        /// <returns>A parameter defined by a specific Dao class.</returns>
        public object Create(T element);
        /// <summary>
        /// A method that reads all the information from the storage.
        /// </summary>
        /// <returns>List of objects created based on the received data.</returns>
        /// <exception cref="System.Data.SqlClient.SqlException">Thrown if there are no table with necessary name.</exception>
        public List<T> ReadAll();
        /// <summary>
        /// The method that is used to update the object data in the storage.
        /// </summary>
        /// <param name="oldElement">The object with the old data.</param>
        /// <param name="newElement">The object with the new data.</param>
        public void Update(T oldElement, T newElement);
        /// <summary>
        /// The method that is used to delete the object data in the storage.
        /// </summary>
        /// <param name="element">A data object which should be removed from the storage.</param>
        public void Delete(T element);
        /// <summary>
        /// A method that can be used to close a storage connection if necessary.
        /// </summary>
        public void CloseConnect();
    }
}
