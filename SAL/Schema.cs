using System.Data;

namespace com.common.sal.schema
{
	/// <summary>
	/// Schema represents a generic, provider independent, database schema. Implements the ISchema interface
	/// </summary>
	public abstract class Schema : ISchema 
	{
		#region Member Variables
		protected string m_name;
		protected IDbConnection m_con;
		protected IDbDataAdapter m_adap;
		protected IDbCommand m_com;
		protected DataSet m_dataset;
		protected ProviderType m_provider;
		#endregion

		#region Constructors
		/// <summary>
		/// Creates an instance of Schema
		/// </summary>
		/// <param name="dataProvider">The provider of the schema</param>
		/// <param name="connectString">The connection string for the provider</param>
		public Schema( ProviderType dataProvider, string connectString ) {
			m_con = SchemaFactory.GetConnection( dataProvider, connectString );
			m_adap = SchemaFactory.CreateDataAdapter( dataProvider );
			m_com = SchemaFactory.CreateCommand( ProviderType.SqlServer );
			m_com.Connection = m_con;
			m_provider = dataProvider;
			m_dataset = new DataSet( m_con.Database );
			m_name = m_con.Database;
		}
		#endregion

		#region ISchema Method Implementations
		public abstract void ChangeConnection(string connection);
		public abstract void ChangeDataBase( string database );
		public abstract void Populate();
		public abstract DataTable GetTable(string name);
		public abstract DataTableCollection GetTables();
		public abstract DataTableCollection GetTables(string[] names);
		#endregion

		#region Properties
		/// <summary>
		/// Gets the name of the database connected to
		/// </summary>
		public string Name {
			get { return m_name; }
		}
		#endregion
	}
}//end namespace