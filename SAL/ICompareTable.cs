using System.Data;

namespace com.common.sal
{
	public interface ICompareTable
	{
		TableDiffResult Compare(DataTable source, DataTable target );
	}//end interface
}
