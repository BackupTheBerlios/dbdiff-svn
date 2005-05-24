using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;

namespace com.common.sal
{
	#region Enumerations
	/// <summary>
	/// Represents the data provider types the SchemaFactory will provide
	/// </summary>
	public enum ProviderType {
		/// <summary>
		/// MSSQL database server
		/// </summary>
		SqlServer
		//Other Provider types (i.e. MS Access, ODBC, etc)
	}
	#endregion

	/// <summary>
	/// Factory class that generates standard interfaces for schema generation
	/// </summary>
	/// <remarks>Use schema factory when you want to access database schemas in a standard,
	/// database independent provider fashion</remarks>
	public class SchemaFactory
	{
		#region Constructors
		//Do not allow this class to be instantiated
		private SchemaFactory() {
		}
		#endregion

		#region Methods
		/// <summary>
		/// Creates a generic, provider independent database connection as an IDbConnection interface
		/// </summary>
		/// <param name="provider">The provider to connect to</param>
		/// <param name="connectionString">The connection string needed to make a connection to the datasource</param>
		/// <returns>The connection represented as a provider independent IDbConnection</returns>
		public static IDbConnection GetConnection( ProviderType provider, string connectionString ) {
			IDbConnection con = null;

			switch( provider ) {		
				//Add other connection instances here
				case ProviderType.SqlServer :
					con = new SqlConnection( connectionString ) as IDbConnection;
					break;
				default :
					con = new SqlConnection( connectionString ) as IDbConnection;
					break;
			}

			return con;
		}

		/// <summary>
		/// Creates a provider independent IDbCommand
		/// </summary>
		/// <param name="command">The query to issue the command</param>
		/// <param name="provider">The provider of the datasource to issue a command against</param>
		/// <param name="con">The connection of the datasource</param>
		/// <returns>A provider independent IDbCommand</returns>
		public static IDbCommand CreateCommand( 
			string command, ProviderType provider, IDbConnection con ) {

			IDbCommand com = null;
			switch( provider ) {
				case ProviderType.SqlServer :
					com = new SqlCommand( command, (SqlConnection) con ) as IDbCommand;
					break;
				default :
					com = new SqlCommand( command, (SqlConnection) con ) as IDbCommand;
					break;
			}

			return com;
		}

		/// <summary>
		/// Creates a provider independent IDbCommand
		/// </summary>
		/// <param name="provider">The provider of the datasource to issue a command against</param>
		/// <returns>A provider independent IDbCommand</returns>
		public static IDbCommand CreateCommand( ProviderType provider ) {
			IDbCommand com = null;
			switch( provider ) {
				case ProviderType.SqlServer :
					com = new SqlCommand() as IDbCommand;
					break;
				default :
					com = new SqlCommand() as IDbCommand;
					break;
			}

			return com;
		}

		/// <summary>
		/// Creates a generic data adapter that is provider independent
		/// </summary>
		/// <param name="provider">The provider of the database to create an adapter against</param>
		/// <returns>An IDbDataAdapter</returns>
		public static IDbDataAdapter CreateDataAdapter( ProviderType provider ) {
			switch( provider ) {
				case ProviderType.SqlServer :
					return new SqlDataAdapter() as IDbDataAdapter;
				default :
					return new SqlDataAdapter() as IDbDataAdapter;
			}
		}

		/// <summary>
		/// Gets the supported data providers
		/// </summary>
		/// <returns>An array of ProviderType enums</returns>
		public static ProviderType[] GetSupportedProviders() {
			ProviderType[] proTypes = (ProviderType[])ProviderType.GetValues( typeof(ProviderType) );

			return proTypes;
		}
		#endregion
	}//end class

	#region Test Fixtures
	[TestFixture]
	public class SchemaFactoryTest
	{
		[Test]
		[ExpectedException( typeof(SqlException) )]
		public void InvalidSqlConnectionString() {
			IDbConnection conn = SchemaFactory.GetConnection(
				ProviderType.SqlServer,
				"Server=HEIDI-KLUM;Database=SourceDB;User ID=jlin;Password=pass");
			conn.Open();
		}

		[Test]
		public void ValidSqlConnectionString() {
			IDbConnection conn = SchemaFactory.GetConnection(
				ProviderType.SqlServer, 
				"Server=(local);Database=SourceDB;User ID=jlin;Password=pass");
			conn.Open();
		}

		[Test]
		public void CreateSqlCommand() {
			IDbConnection con = SchemaFactory.GetConnection( 
				ProviderType.SqlServer, "Server=(local);Database=SourceDB;User ID=jlin;Password=pass" );
			IDbCommand cmd = SchemaFactory.CreateCommand( 
				"select top 1 * from dimRequisitionLineItem", 
				ProviderType.SqlServer,
				con );

			Assert.IsNotNull( cmd );
		}

		[Test]
		public void GetSupportedProviders() {
			ProviderType[] providers = SchemaFactory.GetSupportedProviders();
			ProviderType[] expected = (ProviderType[]) ProviderType.GetValues( typeof(ProviderType) );
			Assert.AreEqual( expected, providers );
		}
	}//end class
	#endregion
}//end namespace