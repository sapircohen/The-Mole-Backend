using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;

/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
public class DBservices
{
    public SqlDataAdapter da;
    public DataTable dt;

    public DBservices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    //private String BuildInsertCommand(Person person)
    //{
    //    String command;

    //    StringBuilder sb = new StringBuilder();
    //    // use a string builder to create the dynamic string
    //    sb.AppendFormat("Values('{0}', '{1}' ,'{2}', {3},{4},'{5}','{6}',{7},'{8}','{9}','{10}','{11}'); select SCOPE_IDENTITY();", person.Name, person.FamilyName, person.Gender, person.Age.ToString(),person.Height.ToString(),person.Image,person.Address,1.ToString(),person.IsPremium,person.CellPhone,person.Password,person.Email);
    //    String prefix = "INSERT INTO PersonNewTbl " + "(Name, FamilyName, Gender, Age,Height,Image,Address,IsActive,IsPremium,CellPhone,Password,Email)";
    //    command = prefix + sb.ToString();

    //    return command;
    //}

    //---------------------------------------------------------------------------------
    // Create the SqlCommand
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

        return cmd;
    }

    public List<List<string>> GetEdges(string conString, string tableName)
    {
        SqlConnection con = null;
        List<List<string>> edgesList = new List<List<string>>();
        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM " + tableName;
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                List<string> pages = new List<string>();

                string page = Convert.ToString(dr["OriginPage"]);
                string linkedPage = Convert.ToString(dr["LinkedPage"]);
                pages.Add(page);
                pages.Add(linkedPage);

                edgesList.Add(pages);
            }

            return edgesList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }

    public List<string> GetVertecies(string conString, string tableName)
    {
        SqlConnection con = null;

        List<string> verteciesList = new List<string>();
        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM " + tableName;
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {
                string vertex = dr["TitlePage"].ToString();
                verteciesList.Add(vertex);
            }

            return verteciesList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }

}
