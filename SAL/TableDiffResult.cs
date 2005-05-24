using System;
using System.Collections;

namespace com.common.sal
{
	#region Enums
	/// <summary>
	/// Enum representing the type of differences between two tables of the same name
	/// </summary>
	public enum TableDiffType
	{
		/// <summary>
		/// Table was missing/deleted in the target database
		/// </summary>
		TableDeleted,

		/// <summary>
		/// The table was changed in the target database
		/// </summary>
		TableChanged,

		/// <summary>
		/// No differences in the table found between the source and target databases
		/// </summary>
		TableNotChanged,

		/// <summary>
		/// Table was added in the target database
		/// </summary>
		TableAdded
	}
	#endregion

	public class TableDiffResult
	{
		#region Member Variables
		private string m_tablename;
		private ColDiffResult[] m_colResults;
		private TableDiffType m_tableDiffType;
		#endregion

		#region Constructors
		public TableDiffResult() {
			m_tablename = null;
			m_colResults = null;
		}

		public TableDiffResult( string table, ColDiffResult[] columns ) {
			m_tablename = table;
			m_colResults = columns;
		}
		#endregion

		#region Methods
		public void AddColDiffResult( ColDiffResult colDiff ) {
			ArrayList list = new ArrayList();
			if( m_colResults != null ) {
				for(int i=0; i<m_colResults.Length; i++) {
					list.Add( m_colResults[i] );
				}				
			}

			list.Add( colDiff );
			m_colResults = (ColDiffResult[]) list.ToArray( typeof(ColDiffResult[]) );
		}
		#endregion

		#region Properties
		public string TableName {
			get { return m_tablename; }
			set { m_tablename = value; }
		}

		public ColDiffResult[] ColumnDiffResults {
			get { return m_colResults; }
			set { m_colResults = value; }
		}

		public TableDiffType DiffType {
			get { return m_tableDiffType; }
			set { m_tableDiffType = value; }
		}
		#endregion
	}//end class
}
