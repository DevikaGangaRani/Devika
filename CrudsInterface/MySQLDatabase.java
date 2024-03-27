// MySQL CRUDS class

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.ResultSetMetaData;

class cMySQLCruds implements iCRUDS
{
	private Connection conn;
    private String query;
    private PreparedStatement statement;
    private ResultSet queryResult;
    private String[] fields;

    public cMySQLCruds(String pDbName, String[] pFieldNames) throws SQLException 
    {
        fields = pFieldNames;
    	conn = DriverManager.getConnection("jdbc:mysql://138.68.140.83/" + pDbName, "Rani", "Rani@123");
        System.out.println("Connected to the database.");
    }

    public int insertRecord(String[] pRecordDetails, String pTableName) throws SQLException
    {
        int counter = 0;
        StringBuilder queryBuilder = new StringBuilder("INSERT INTO ").append(pTableName);

        queryBuilder.append(" VALUES (");
 
        for (counter = 0; counter < pRecordDetails.length; counter++) 
        {
            queryBuilder.append("?");
            if (counter < pRecordDetails.length - 1) 
            {
                queryBuilder.append(", ");
            }
        }

        queryBuilder.append(")");
        query = queryBuilder.toString();
        statement = conn.prepareStatement(query);
        
        for (counter = 0; counter < pRecordDetails.length; counter++) 
        {
            statement.setString(counter + 1, pRecordDetails[counter]);
        }

        int rowInserted = statement.executeUpdate();
        return rowInserted;
    }

    private int getRowCount(String pTableName) throws SQLException 
    {
        int rowCount = 0;
        query = "SELECT COUNT(*) FROM " + pTableName;
        statement = conn.prepareStatement(query);
        queryResult = statement.executeQuery();
        if (queryResult.next())
        {
            rowCount = queryResult.getInt(1);
        }
        return rowCount;
    }

    public String[][] loadRecords(String pTableName) throws SQLException 
    {
        int rowCount = getRowCount(pTableName), counter = 0;
        query = "SELECT * FROM " + pTableName;
        statement = conn.prepareStatement(query);
        queryResult = statement.executeQuery();

        ResultSetMetaData metaData = queryResult.getMetaData();
        int columnCount = metaData.getColumnCount();

        String[][] records = new String[rowCount][columnCount];

        int index = 0;
        while (queryResult.next()) 
        {
            for (counter = 0; counter < columnCount; counter++) 
            {
                records[index][counter] = queryResult.getString(counter + 1);
            }
            index++;
        }
        return records;
    }
}