using System.Data;

namespace com.common.sal.schema
{
	/// <summary>
	/// ISchema provides generic operations that schema objects can perform
	/// </summary>
	public interface ISchema
	{
		/// <summary>
		/// Gets the name of the schema
		/// </summary>
		string Name {
			get;
		}

		/// <summary>
		/// Changes the database connection of a schema
		/// </summary>
		/// <param name="connection">The connection string for the new connection</param>
		void ChangeConnection( string connection );

		/// <summary>
		/// Changes the database the schema is using
		/// </summary>
		/// <param name="database">The name of the database to use</param>
		void ChangeDataBase( string database );

		/// <summary>
		/// Populates the schema with the table and column mappings of the database
		/// the schema is using.
		/// </summary>
		/// <remarks>
		/// If a schema object has already been populated with table and column mappings of a database,
		/// subsequent calls of Populate() will clear the existing mappings and re-populate itself
		/// with the table and column mappings. Calling Populate() only generates the schema information,
		/// and does not retrieve any rows for any table in the database.
		/// </remarks>
		void Populate();

		/// <summary>
		/// Retrieves a table from the schema
		/// </summary>
		/// <param name="name">The name of the table to be retrieved</param>
		/// <returns>A DataTable representing the table retrieved</returns>
		/// <remarks>
		/// If the table specified does not exist in the schema, or if the user has not called
		/// Populate(),
		/// a null value is returned
		/// </remarks>
		DataTable GetTable( string name );

		/// <summary>
		/// Retrieves all the tables in the schema
		/// </summary>
		/// <returns>A DataTableCollection object containing all the tables of the schema</returns>
		DataTableCollection GetTables();

		/// <summary>
		/// Retrieves the tables specified from the schema
		/// </summary>
		/// <param name="names">An array of strings the tables to be retrieved</param>
		/// <returns>
		/// DataTableCollection of all the tables retrieved. It will not include
		/// tables that do not exist in the schema.
		/// </returns>
		DataTableCollection GetTables( string[] names );
	}//end interface
}//end namespace
