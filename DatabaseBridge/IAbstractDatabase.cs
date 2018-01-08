using System;
namespace DatabaseBridge
{
    interface IAbstractDatabase<ADAPTER_TYPE>
     where ADAPTER_TYPE : System.Data.Common.DbDataAdapter, new()
    {
        System.Data.Common.DbParameter AddInParameter(System.Data.Common.DbCommand cmd, string paramName, System.Data.DbType paramType, int size, object value);
        System.Data.Common.DbParameter AddInParameter(System.Data.Common.DbCommand cmd, string paramName, System.Data.DbType paramType, object value);
        System.Data.Common.DbParameter AddOutParameter(System.Data.Common.DbCommand cmd, string paramName, System.Data.DbType paramType, object value);
        System.Data.Common.DbParameter AddParameter(System.Data.Common.DbCommand cmd, string paramName, System.Data.DbType paramType, System.Data.ParameterDirection direction, int size, object value);
        System.Data.Common.DbParameter AddParameter(System.Data.Common.DbCommand cmd, string paramName, System.Data.DbType paramType, System.Data.ParameterDirection direction, object value);
        System.Data.Common.DbTransaction BeginTransaction();
        System.Data.Common.DbConnection Connection { get; }
        void Dispose();
        System.Data.DataSet ExecuteDataSet(System.Data.Common.DbCommand cmd);
        int ExecuteNonQuery(System.Data.Common.DbCommand cmd, System.Data.Common.DbTransaction txn);
        int ExecuteNonQuery(System.Data.Common.DbCommand cmd);
        System.Data.Common.DbDataReader ExecuteReader(System.Data.Common.DbCommand cmd, System.Data.CommandBehavior behavior);
        System.Data.Common.DbDataReader ExecuteReader(System.Data.Common.DbCommand cmd);
        T ExecuteScalar<T>(System.Data.Common.DbCommand cmd, T defaultValue);
        ADAPTER_TYPE GetAdapter();
        System.Data.Common.DbCommand GetSqlStringCommand(string sqlString);
        System.Data.Common.DbCommand GetSqlStringCommand(string sqlStringFormat, params object[] args);
        System.Data.Common.DbCommand GetStoredProcedureCommand(string storedProcName);
    }
}
