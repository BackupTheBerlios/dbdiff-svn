using System;

namespace com.common.sal
{
	/// <summary>
	/// Represents an exception thrown by methods of an object that implements ISchema
	/// </summary>
	public class SchemaException : ApplicationException
	{
		/// <summary>
		/// Just calls the parent constructor
		/// </summary>
		public SchemaException() : 
			base() {
		}

		/// <summary>
		/// Simply passes on the message provided by the parent class
		/// </summary>
		/// <param name="message"></param>
		public SchemaException( string message ) : 
			base( message ) {
		}
	}//end class
}
