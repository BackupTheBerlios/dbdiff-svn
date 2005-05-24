using System;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using com.common.sal;
using com.common.sal.schema;

namespace DbReporter
{
	public class DbReporter : Form
	{
		private GroupBox gbConnectTo;
		private TextBox txtPass;
		private Label lblPass;
		private Label label1;
		private TextBox txtUser;
		private Label lblServer;
		private TextBox txtServer;
		private Label lblDatabase;
		private TextBox txtDatabase;
		private ComboBox cbProvider;
		private Label lblProvider;
		private Button btnGetTables;
		private ListBox lbTables;
		private Container components = null;

		private ISchema schema;

		public DbReporter() {
			InitializeComponent();

			cbProvider.Items.Add( ProviderType.SqlServer.ToString() );
		}

		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.gbConnectTo = new System.Windows.Forms.GroupBox();
			this.btnGetTables = new System.Windows.Forms.Button();
			this.lblProvider = new System.Windows.Forms.Label();
			this.cbProvider = new System.Windows.Forms.ComboBox();
			this.txtDatabase = new System.Windows.Forms.TextBox();
			this.lblDatabase = new System.Windows.Forms.Label();
			this.txtServer = new System.Windows.Forms.TextBox();
			this.lblServer = new System.Windows.Forms.Label();
			this.txtPass = new System.Windows.Forms.TextBox();
			this.lblPass = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtUser = new System.Windows.Forms.TextBox();
			this.lbTables = new System.Windows.Forms.ListBox();
			this.gbConnectTo.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbConnectTo
			// 
			this.gbConnectTo.Controls.Add(this.btnGetTables);
			this.gbConnectTo.Controls.Add(this.lblProvider);
			this.gbConnectTo.Controls.Add(this.cbProvider);
			this.gbConnectTo.Controls.Add(this.txtDatabase);
			this.gbConnectTo.Controls.Add(this.lblDatabase);
			this.gbConnectTo.Controls.Add(this.txtServer);
			this.gbConnectTo.Controls.Add(this.lblServer);
			this.gbConnectTo.Controls.Add(this.txtPass);
			this.gbConnectTo.Controls.Add(this.lblPass);
			this.gbConnectTo.Controls.Add(this.label1);
			this.gbConnectTo.Controls.Add(this.txtUser);
			this.gbConnectTo.Location = new System.Drawing.Point(16, 16);
			this.gbConnectTo.Name = "gbConnectTo";
			this.gbConnectTo.Size = new System.Drawing.Size(256, 232);
			this.gbConnectTo.TabIndex = 0;
			this.gbConnectTo.TabStop = false;
			this.gbConnectTo.Text = "Connect To";
			// 
			// btnGetTables
			// 
			this.btnGetTables.Location = new System.Drawing.Point(168, 192);
			this.btnGetTables.Name = "btnGetTables";
			this.btnGetTables.TabIndex = 10;
			this.btnGetTables.Text = "Get Tables";
			this.btnGetTables.Click += new System.EventHandler(this.btnGetTables_Click);
			// 
			// lblProvider
			// 
			this.lblProvider.Location = new System.Drawing.Point(16, 152);
			this.lblProvider.Name = "lblProvider";
			this.lblProvider.Size = new System.Drawing.Size(56, 23);
			this.lblProvider.TabIndex = 9;
			this.lblProvider.Text = "Provider:";
			// 
			// cbProvider
			// 
			this.cbProvider.Location = new System.Drawing.Point(72, 152);
			this.cbProvider.Name = "cbProvider";
			this.cbProvider.Size = new System.Drawing.Size(168, 21);
			this.cbProvider.TabIndex = 8;
			// 
			// txtDatabase
			// 
			this.txtDatabase.Location = new System.Drawing.Point(72, 120);
			this.txtDatabase.Name = "txtDatabase";
			this.txtDatabase.Size = new System.Drawing.Size(168, 20);
			this.txtDatabase.TabIndex = 7;
			this.txtDatabase.Text = "";
			// 
			// lblDatabase
			// 
			this.lblDatabase.Location = new System.Drawing.Point(16, 120);
			this.lblDatabase.Name = "lblDatabase";
			this.lblDatabase.Size = new System.Drawing.Size(56, 23);
			this.lblDatabase.TabIndex = 6;
			this.lblDatabase.Text = "Database:";
			// 
			// txtServer
			// 
			this.txtServer.Location = new System.Drawing.Point(72, 88);
			this.txtServer.Name = "txtServer";
			this.txtServer.Size = new System.Drawing.Size(168, 20);
			this.txtServer.TabIndex = 5;
			this.txtServer.Text = "";
			// 
			// lblServer
			// 
			this.lblServer.Location = new System.Drawing.Point(16, 88);
			this.lblServer.Name = "lblServer";
			this.lblServer.Size = new System.Drawing.Size(56, 23);
			this.lblServer.TabIndex = 4;
			this.lblServer.Text = "Server:";
			// 
			// txtPass
			// 
			this.txtPass.Location = new System.Drawing.Point(72, 56);
			this.txtPass.Name = "txtPass";
			this.txtPass.PasswordChar = '*';
			this.txtPass.Size = new System.Drawing.Size(168, 20);
			this.txtPass.TabIndex = 3;
			this.txtPass.Text = "";
			// 
			// lblPass
			// 
			this.lblPass.Location = new System.Drawing.Point(16, 56);
			this.lblPass.Name = "lblPass";
			this.lblPass.Size = new System.Drawing.Size(56, 23);
			this.lblPass.TabIndex = 2;
			this.lblPass.Text = "Pass:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "User:";
			// 
			// txtUser
			// 
			this.txtUser.Location = new System.Drawing.Point(72, 24);
			this.txtUser.Name = "txtUser";
			this.txtUser.Size = new System.Drawing.Size(168, 20);
			this.txtUser.TabIndex = 0;
			this.txtUser.Text = "";
			// 
			// lbTables
			// 
			this.lbTables.Location = new System.Drawing.Point(16, 264);
			this.lbTables.Name = "lbTables";
			this.lbTables.Size = new System.Drawing.Size(256, 199);
			this.lbTables.TabIndex = 1;
			// 
			// Tattler
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(288, 478);
			this.Controls.Add(this.lbTables);
			this.Controls.Add(this.gbConnectTo);
			this.Name = "Tattler";
			this.Text = "Db Reporter";
			this.gbConnectTo.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		[STAThread]
		static void Main() {
			Application.Run( new DbReporter() );
		}

		private void btnGetTables_Click(object sender, EventArgs e) {
			//////////////////////////////////////////////////////////
			//TODO: Dynamically load provider specific assemblies
			//NOTE: There seems to be a bug in the way .NET loads
			//assemblies in the GAC, so dynamic loading of an assembly
			//does not work for me right now. Need to investigate

			//string assemblyName = 
			//	ConfigurationSettings.AppSettings
			//	["SALAssembly"];
			//string typeName = "com.common.sal.schema.SqlSchema";
			//Assembly.Load("SqlSchema");
			//////////////////////////////////////////////////////////

			string connection;
			connection = "Server=" + txtServer.Text + ";Database=" + txtDatabase.Text +
				";User ID=" + txtUser.Text + ";Password=" + txtPass.Text;
			
			//schema = Assembly.Load( assemblyName ).CreateInstance( typeName ) as ISchema;
			schema = new SqlSchema( connection ) as ISchema; 
			if( schema != null ) {
				//List user tables
				try {
					schema.Populate();
					DataTableCollection dtc = schema.GetTables();
					foreach( DataTable dt in dtc ) {
						lbTables.Items.Add( dt );
					}

					//Alphabetical order
					lbTables.Sorted = true;
				}
				catch( SchemaException se ) {
					MessageBox.Show( se.Message, "SchemaException");
				}
			}
		}
	}//end class
}//end namespace
