// iCRUDS interface

interface iCRUDS
{
	int insertRecord(String[] pRecordDetails, String pTableName) throws Exception;
	String[][] loadRecords(String pTableName) throws Exception;
}