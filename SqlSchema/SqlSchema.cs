using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;

namespace com.common.sal.schema
{
	/// <summary>
	/// SqlSchema represents a SQL Server 2000 database schema.
	/// </summary>
	public class SqlSchema : Schema
	{
		#region Constructors
		/// <summary>
		/// Creates an object representing an MSSQL database schema
		/// </summary>
		/// <param name="connection">The connection string of the MSSQL database</param>
		public SqlSchema( string connection ) : 
			base( ProviderType.SqlServer, connection ) {
		}

		/// <summary>
		/// Creates an object representing a MSSQL database schema
		/// </summary>
		/// <param name="server">The server where the MSSQL database resides</param>
		/// <param name="database">The name of the MSSQL database</param>
		/// <param name="user">The username of the user accessing the MSSQL database</param>
		/// <param name="pass">The password of the user</param>
		public SqlSchema( string server, string database, 
								string user, string pass ) : 
				base( ProviderType.SqlServer, 
						"Server=" + server + ";Database=" + database + 
						";User ID=" + user + ";Password=" + pass ) {
		}
		#endregion

		#region Overriden Schema methods
		/// <summary>
		/// Change the current connection to another MSSQL database
		/// </summary>
		/// <param name="connection">The connection string of the MSSQL database</param>
		public override void ChangeConnection(string connection) {
			if( m_con != null ) {
				m_con.Close();
			}

			m_con = SchemaFactory.GetConnection( ProviderType.SqlServer, connection );
			if( m_con == null )
				throw new SchemaException( "Could not change connection" );
		}

		/// <summary>
		/// Change to a different MSSQL database with the current connection
		/// </summary>
		/// <param name="database">The name of the database to change to</param>
		public override void ChangeDataBase(string database) {
			if( m_con.State == ConnectionState.Closed ) 
				m_con.Open();	
			m_con.ChangeDatabase( database );
			m_name = m_con.Database;
		}

		/// <summary>
		/// Populates the SqlSchema instance with a schema of all the tables in the current connection
		/// </summary>
		public override void Populate() {
			//TODO: Find a more efficient way to do this
			if( m_dataset.Tables.Count > 0 ) m_dataset.Tables.Clear();
			DataSet ds = new DataSet();

			try {
				IDbCommand com = SchemaFactory.CreateCommand(
					"Select name From sysobjects Where xtype = 'U'", 
					ProviderType.SqlServer, 
					m_con );
				com.CommandType = CommandType.Text;
				IDbDataAdapter adapter = SchemaFactory.CreateDataAdapter( ProviderType.SqlServer );
				adapter.SelectCommand = com;
				adapter.Fill( ds );
			}
			catch( SqlException e ) {
				throw new SchemaException( e.Message );
			}
			
			//Really only parsing one table, and one column. The table we are parsing
			//contains the names of all the tables in the database.
			foreach( DataTable t in ds.Tables ) {
				foreach( DataRow r in t.Rows ) {
					foreach( DataColumn c in t.Columns ) {
						string tablename = r[c].ToString();
						string cmdstr = "Select top 1 * From " + tablename;
						try {
							IDbCommand cmd = SchemaFactory.CreateCommand( cmdstr, ProviderType.SqlServer, m_con );
							cmd.CommandType = CommandType.Text;
							DataTable table = FillTable( cmd );
							table.TableName = tablename;
							m_dataset.Tables.Add( table );
						}
						catch( SqlException e ) {
							throw new SchemaException( e.Message );
						}
					}
				}
			}//end foreach
		}

		/// <summary>
		/// Fetch the schema of a table from the current connection
		/// </summary>
		/// <param name="name">The name of the schema to retrieve</param>
		/// <returns>A DataTable representing the schema of the table</returns>
		public override DataTable GetTable(string name) {
			if( m_dataset.Tables.Contains(name) )
				return m_dataset.Tables[name];
			else
				throw new SchemaException( m_con.Database + " does not contain table '" + name + "'" );
		}

		/// <summary>
		/// Fetch the schema of all the tables in the current database connection
		/// </summary>
		/// <returns>A DataTableCollection containing the schemas of all the tables in the database</returns>
		public override DataTableCollection GetTables() {
			if( m_dataset.Tables.Count > 0) 
				return m_dataset.Tables;
			else
				throw new SchemaException( m_con.Database + " does not have tables or is empty" );
		}

		/// <summary>
		/// Fetch the schema of all the tables listed
		/// </summary>
		/// <param name="names">The names of all the tables desired</param>
		/// <returns>A DataTableCollection containing the schemas of all the tables fetched</returns>
		public override DataTableCollection GetTables(string[] names) {
			DataSet ds = new DataSet();
			foreach( string name in names ) {
				if( m_dataset.Tables.Contains( name ) ) {
					string query = "Select Top 1 * From " + name;
					try {
						IDbCommand com = SchemaFactory.CreateCommand( query, ProviderType.SqlServer, m_con );
						DataTable table = this.FillTable( com );
						table.TableName = name;
						ds.Tables.Add( table );
					}
					catch( SqlException e ) {
						throw new SchemaException( e.Message );
					}
				}
				else
					throw new SchemaException( m_con.Database + " does not have table: " + name );
			}//end foreach

			return ds.Tables;
		}
		#endregion

		#region Helper methods
		/// <summary>
		/// Called in order to construct the schema of a table
		/// </summary>
		/// <param name="command">The command representing a query to fetch tables</param>
		/// <returns>A DataTable object containing the schema of the desired table</returns>
		private DataTable FillTable( IDbCommand command ) {
			DataSet dataset = null;
			DataTable t = null;
			try {
				dataset = new DataSet();
				IDbDataAdapter adapter = SchemaFactory.CreateDataAdapter( ProviderType.SqlServer );
				adapter.SelectCommand = command;
				
				adapter.FillSchema( dataset, SchemaType.Mapped );
				t = dataset.Tables[0];
			}
			catch( SqlException e ) {
				throw new SchemaException( e.Message );
			}
			finally {
				dataset.Tables.Clear();
			}

			return t;
		}
		#endregion

	}//end class

	#region TestFixtures
	[TestFixture]
	public class TestSqlSchema
	{
		SqlSchema schema;
		string connect;

		[SetUp]
		public void Init() {
			connect = "Server=(local);Database=SourceDB;User ID=sa;Password=ichiban";
			schema = 
				new SqlSchema( connect );
		}

		[Test]
		public void ChangeConnection() {
			schema.ChangeConnection( "Server=(local);Database=TargetDB;User ID=jlin;Password=pass" );
		}

		[Test]
		public void ChangeToSameConnection() {
			schema.ChangeConnection( connect );
			Assert.IsNotNull( schema );
		}

		[Test]
		[ExpectedException( typeof(ArgumentException) )]
		public void ChangeToInvalidConnection() {
			schema.ChangeConnection( 
				"Server=bergajerga;Database=crapola;SourceDB=sheist;User ID=jabbathehutt;Password=pookie" );
		}

		[Test]
		[ExpectedException( typeof(SqlException) )]
		public void ChangeToInvalidDatabase() {
			schema.ChangeDataBase( "SomeDatabase" );
		}

		[Test]
		public void ChangeToValidDatabase() {
			schema.ChangeDataBase( "TargetDB" );
		}

		[Test]
		public void Populate() {
			schema.Populate();
			Assert.IsNotNull( schema.GetTables() );
		}

		[Test]
		public void PopulateWithExpectedTable() {
			schema.Populate();
			DataTable t = schema.GetTable("dimRequisitionLineItem");
			Assert.IsNotNull( t );
		}

		[Test]
		[ExpectedException( typeof(SchemaException) )]
		public void GetNoTables() {
			DataTableCollection dtc = schema.GetTables();
		}

		[Test]
		[ExpectedException( typeof(SchemaException) )]
		public void GetNonExistentTable() {
			schema.Populate();
			DataTable t = schema.GetTable("SomeTable");
		}

		[Test]
		public void GetExistingTables() {
			schema.Populate();
			string[] tables = { "dimRequisitionLineItem" };
			DataTableCollection dtc = schema.GetTables( tables );
			Assert.IsTrue( dtc.Contains("dimRequisitionLineItem") );
		}

		[Test]
		[ExpectedException( typeof(SchemaException) )]
		public void GetNonExistingTables() {
			schema.Populate();
			string[] tables = { "dimRequisitionLineItem", "DoesntExist1", "DoesntExist2" };
			schema.GetTables( tables );
		}
		
		[Test]
		[ExpectedException( typeof(SchemaException) )]
		public void PopulateWithInsuffUserPermissions() {
			connect= "Server=(local);Database=SourceDB;User ID=jlin;Password=pass";
			schema = new SqlSchema( connect );
			schema.Populate();
		}
	}//end TestFixture
	#endregion
}
