using System;
using NUnit.Framework;

namespace com.common.sal
{
	#region Enums
	public enum ColDiffType
	{
		ColumnDeleted,
		ColumnChanged,
		ColumnAdded,
		ColumnNotChanged
	}
	#endregion

	public class ColDiffResult
	{
		#region Member Variables
		private string m_colName;
		private string m_colDataType1;
		private string m_colDataSize1;
		private string m_colDataType2;
		private string m_colDataSize2;
		private ColDiffType m_diffType;
		#endregion

		#region Constructors
		public ColDiffResult() {
			InitializeMembers();
		}
		#endregion

		#region Methods
		private void InitializeMembers() {
			m_colName = null;
			m_colDataType1 = null;
			m_colDataSize1 = null;
			m_colDataType2 = null;
			m_colDataSize2 = null;
		}
		#endregion

		#region Properties
		public string ColumnName {
			get { return m_colName; }
			set { m_colName = value; }
		}

		public string SourceColumnType {
			get { return m_colDataType1; }
			set { m_colDataType1 = value; }
		}

		public string SourceColumnSize {
			get { return m_colDataSize1; }
			set { m_colDataSize1 = value; }
		}

		public string TargetColumnType {
			get { return m_colDataType2; }
			set { m_colDataType2 = value; }
		}

		public string TargetColumnSize {
			get { return m_colDataSize2; }
			set { m_colDataSize2 = value; }
		}

		public ColDiffType DiffType {
			get { return m_diffType; }
			set { m_diffType = value; }
		}
		#endregion
	}//end class

	#region Test Fixtures
	[TestFixture]
	public class ColDiffResultTest
	{
		[Test]
		public void EmptyConstructorTest() {
			ColDiffResult res = new ColDiffResult();
			Assert.IsNotNull( res );
		}

		[Test]
		[ExpectedException( typeof(AssertionException) )]
		public void ChangeColumnName() {
			ColDiffResult res = new ColDiffResult();
			res.ColumnName = "I like big breasts";
			string columnName = "I like big breasts";
			res.ColumnName = "Old Man Retardo";

			Assert.AreEqual( res.ColumnName, columnName );
		}
	}//end class
	#endregion
}