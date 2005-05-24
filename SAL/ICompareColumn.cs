using System;
using System.Data;

namespace com.common.sal
{
	public interface ICompareColumn
	{
		ColDiffResult Compare( DataColumn x, DataColumn y );
	}
}
