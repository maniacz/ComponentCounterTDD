using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Windows.Forms;

namespace ComponentCounter
{
    public class DBHelper
    {
        public OracleConnection Connection { get; private set; }
        private string connectionString;
        private string opWithDrawer;
        private string sqlQueryReturningComponentCount;

        public DBHelper(Line lineToConnect)
        {
            Dictionary<string, string> sqlQuery = new Dictionary<string, string>();

            string sqlNonFlex = string.Concat("select h1.szuflada, count(pv.VALUE) ilość, h1.itempartno ",
                            "from (select itempartno, ",
                            "decode(itempartno, 'A0038109', '1', 'A0038110', '2', 'A0035512', '3', 'A0038271', '4', 'A0032886', '5', 'A0038111', '6', 'A0032887', '7', 'A0032888', '8') szuflada ",
                            "from  ",
                              "(select op, itempartno, decode(itempartno, 'A0038109', '1', 'A0038110', '2', 'A0035512', '3', 'A0038271', '4', 'A0032886', '5', 'A0038111', '6', 'A0032887', '7', 'A0032888', '8') szuflada ",
                              "from acc_bomitem_setup ",
                              "where op = :opDrawer1) ",
                            "where op = :opDrawer2 and szuflada is not null ",
                            "group by szuflada, itempartno) h1  ",
                              "left join ",
                                    "ACC_PROCDATA_VALUE pv ",
                                "on h1.szuflada = pv.value ",
                              "and pv.DATAID in ( %parameters% ) ",
                                "and pv.CTIME between :timeFrom and :timeTo ",
                            "group by h1.szuflada, h1.itempartno ",
                            "order by h1.szuflada");

            sqlQuery.Add("nonFlex", sqlNonFlex);

            string sqlFlex = string.Concat("select h1.szuflada, count(pv.VALUE) ilość, h1.itempartno ",
                            "from (select itempartno, ",
                            "decode(itempartno, 'A0065901', '1', 'A0071454', '2', 'A0071455', '3', 'A0071456', '4', 'A0071457', '5', 'A0071458', '6', 'A0071459', '7', 'A0071460',",
                                            "'8', 'A0076018', '9', 'A0076019', '10', 'A0076020', '11', 'A0076021', '12', 'A0076022', '13', 'A0076023', '14') szuflada ",
                            "from  ",
                              "(select op, itempartno, decode(itempartno, 'A0065901', '1', 'A0071454', '2', 'A0071455', '3', 'A0071456', '4', 'A0071457', '5', 'A0071458', '6', 'A0071459', '7', 'A0071460', ",
                                                                                                "'8', 'A0076018', '9', 'A0076019', '10', 'A0076020', '11', 'A0076021', '12', 'A0076022', '13', 'A0076023', '14') szuflada ",
                              "from acc_bomitem_setup ",
                              "where op = :opDrawer1) ",
                            "where op = :opDrawer2 and szuflada is not null ",
                            "group by szuflada, itempartno) h1  ",
                              "left join ",
                                    "ACC_PROCDATA_VALUE pv ",
                                "on h1.szuflada = pv.value ",
                              "and pv.DATAID in ( %parameters% ) ",
                                "and pv.CTIME between :timeFrom and :timeTo ",
                            "group by h1.szuflada, h1.itempartno ",
                            "order by to_number(h1.szuflada)");

            sqlQuery.Add("flex", sqlFlex);

            switch (lineToConnect)
            {
                case Line.EPS_FIAT:
                    connectionString = ConnectionStrings["FACTORY_6103"];
                    opWithDrawer = "140";
                    sqlQueryReturningComponentCount = sqlQuery["nonFlex"];
                    break;
                case Line.EPS_FIAT2:
                    connectionString = ConnectionStrings["FACTORY_B404"];
                    opWithDrawer = "140";
                    sqlQueryReturningComponentCount = sqlQuery["nonFlex"];
                    break;
                case Line.EPS_VW1:
                    connectionString = ConnectionStrings["FACTORY_6104"];
                    opWithDrawer = "140";
                    sqlQueryReturningComponentCount = sqlQuery["nonFlex"];
                    break;
                case Line.EPS_VW2:
                    connectionString = ConnectionStrings["FACTORY_6105"];
                    opWithDrawer = "140";
                    sqlQueryReturningComponentCount = sqlQuery["nonFlex"];
                    break;
                case Line.EPS_VW4:
                    connectionString = ConnectionStrings["FACTORY_B403"];
                    opWithDrawer = "61";
                    sqlQueryReturningComponentCount = sqlQuery["nonFlex"];
                    break;
                case Line.EPS_FORD:
                    connectionString = ConnectionStrings["FACTORY_B405"];
                    opWithDrawer = "60B";
                    sqlQueryReturningComponentCount = sqlQuery["nonFlex"];
                    break;
                case Line.EPS_FLEX_FORD:
                    connectionString = ConnectionStrings["FACTORY_B406"];
                    opWithDrawer = "60";
                    sqlQueryReturningComponentCount = sqlQuery["flex"];
                    break;
                    /*
                case Line.EPS_FLEX_VW:
                    connectionString = ConnectionStrings["FACTORY_B406"];
                    opWithDrawer = "61";
                    sqlQueryReturningComponentCount = sqlQuery["flex"];
                    break;
                    */
                default:
                    throw new Exception("Wrong line to connect to.");
            }
        }



        public ConnectionState Connect()
        {
            try
            {
                if (Connection == null)
                {
                    Connection = new OracleConnection(connectionString);
                }
                Connection.Open();
            }
            catch (Exception)
            {
                //TODO: Zaloguj błąd połączenia
                return ConnectionState.Broken;
            }

            return Connection.State;
        }

        public byte GetComponentUsageCountPerCycle(string itemPartNo, string partNo)
        {
            byte result;
            string sql = @"select QTY_PER from ACC_BOMITEM_SETUP where ITEMPARTNO = :itemPartNo and PARTNO = :partNo";
            using (Connection = new OracleConnection(connectionString))
            using (OracleCommand query = new OracleCommand(sql, Connection))
            {
                try
                {
                    Connection.Open();
                    query.Parameters.Add("itemPartNo", itemPartNo);
                    query.Parameters.Add("partNo", partNo);
                    OracleDataReader dataReader = query.ExecuteReader();
                    dataReader.Read();
                    result = byte.Parse(dataReader["QTY_PER"].ToString());
                }
                catch (Exception)
                {
                    //TODO: Zaloguj błąd
                    throw;
                }
            }
            return result;
        }

        public DataSet GetDrawerCountData(DateTime timeFrom, DateTime timeTo)
        {
            DataSet result;
            List<int> procDataCfgDrawerRecIds = GetProcDataCfgDrawerRecIds();

            OracleDataAdapter adp = null;
            using (Connection = new OracleConnection(connectionString))
            using (OracleCommand query = new OracleCommand(sqlQueryReturningComponentCount, Connection))
            {
                try
                {
                    Connection.Open();
                    query.Parameters.Add("opDrawer1", opWithDrawer);
                    query.Parameters.Add("opDrawer2", opWithDrawer);

                    List<string> parameters = new List<string>();
                    foreach (var recId in procDataCfgDrawerRecIds)
                    {
                        string paramName = ":recId" + parameters.Count.ToString();
                        parameters.Add(paramName);
                        OracleParameter param = new OracleParameter(paramName, OracleDbType.Varchar2);
                        param.Value = recId;
                        query.Parameters.Add(param);
                    }

                    query.Parameters.Add("timeFrom", timeFrom);
                    query.Parameters.Add("timeTo", timeTo);

                    query.CommandText = sqlQueryReturningComponentCount.Replace("%parameters%", string.Join(",", parameters.ToArray()));

                    adp = new OracleDataAdapter(query);
                    result = new DataSet();
                    adp.Fill(result);
                }
                catch (Exception)
                {
                    //TODO: Zaloguj błąd
                    throw;
                }
                finally
                {
                    adp.Dispose();
                }
            }
            return result;
        }

        public List<int> GetProcDataCfgDrawerRecIds()
        {
            string sql = @"select REC_ID from ACC_PROCDATA_CFG where OP = :op and (LOWER(NAME) = 'drewer number' or LOWER(NAME) = 'drawer no' or LOWER(NAME) = 'box number' " +
                @"or LOWER(NAME) = 'ford - feeder no' or LOWER(NAME) = 'vw - drawer no')";
            List<int> recIds = new List<int>();

            using (OracleConnection connection = new OracleConnection(connectionString))
            using (OracleCommand query = new OracleCommand(sql, connection))
            {
                try
                {
                    connection.Open();
                    query.Parameters.Add("op", opWithDrawer);
                    OracleDataReader dataReader = query.ExecuteReader();

                    while (dataReader.Read())
                    {
                        if (!dataReader.IsDBNull(0))
                        {
                            recIds.Add(dataReader.GetInt32(0));
                        }
                    }
                }
                catch (Exception)
                {
                    //TODO: Obsłuż wyjątek
                    throw;
                }
            }
            return recIds;
        }

        internal DataSet GetAllLinesDrawerCountData(DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            DBHelper dbFiat = new DBHelper(Line.EPS_FIAT);
            DataSet templateDataSet = dbFiat.GetDrawerCountData(dateTimeFrom, dateTimeFrom);

            int dataSetRowCount = templateDataSet.Tables[0].Rows.Count;

            DBHelper db = null;
            for (int i = 0; i < dataSetRowCount; i++)
            {
                int usedComponentCount = 0;
                int totalComponentCount = 0;

                foreach (Line line in Enum.GetValues(typeof(Line)))
                {
                    //if (line.Equals(Line.ALL) || line.ToString().StartsWith("EPS_VW"))
                    if (line.Equals(Line.ALL))
                        continue;

                    try
                    {
                        db = DBConnectionStaticSimpleFactory.CreateDBHelper(line);
                        DataSet ds = db.GetDrawerCountData(dateTimeFrom, dateTimeTo);
                        usedComponentCount = Int32.Parse(ds.Tables[0].Rows[i][1].ToString());
                        totalComponentCount += usedComponentCount;
                    }
                    catch (Exception)
                    {
                        //todo: Zaloguj błąd
                        throw;
                    }
                }
                templateDataSet.Tables[0].Rows[i][1] = totalComponentCount;
            }

            return templateDataSet;
            /*
            DataSet ds = db.GetDrawerCountData(dateTimeFrom, dateTimeTo);

            for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
            {
                //DataRow dataRow = new DataRow("0").
                int t1;
                if (Int32.TryParse(ds1.Tables[0].Rows[i][1].ToString(), out t1))
                {
                    bool temp = false;
                }

                try
                {
                    drawerNo = Int32.Parse(ds1.Tables[0].Rows[i][0].ToString());
                    //usedComponentCount = Int32.Parse(ds1.Tables[0].Rows[i][1].ToString());
                    //usedComponentPartNo = Int32.Parse(ds1.Tables[0].Rows[i][2].ToString());
                }
                catch (Exception)
                {
                    bool temp = false;
                    throw;
                }

                DataSet allLinesDataSet = new DataSet();
                //allLinesDataSet.
                ds1.Tables[0].Rows[0][0] = drawerNo + usedComponentCount;


                Int32.TryParse(ds1.Tables[0].Rows[i][1].ToString(), out t1);
                ds1.Tables[0].Rows[i][1] = ((int)ds1.Tables[0].Rows[i][1] + (int)ds2.Tables[0].Rows[i][1]);
                byte p = 0;
            }

            return ds1;
            */
        }

        public static class DBConnectionStaticSimpleFactory
        {
            public static DBHelper CreateDBHelper(Line lineToConnect)
            {
                DBHelper db = new DBHelper(lineToConnect);
                return db;
            }
        }

        #region "Connection strings"
        Dictionary<string, string> ConnectionStrings = new Dictionary<string, string>()
        {
            { "LOCAL_HR", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SID = xe)));User Id=hr ;Password=hr" },
            { "LOCAL_6103", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=149.223.244.5)(PORT=1521)))(CONNECT_DATA=(SID = xe)));User Id=acc ;Password=acc" },
            { "LOCAL_6104", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=149.223.242.52)(PORT=1521)))(CONNECT_DATA=(SID = xe)));User Id=acc ;Password=acc" },
            { "LOCAL_6105", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=149.223.244.1)(PORT=1521)))(CONNECT_DATA=(SID = xe)));User Id=acc ;Password=acc" },
            { "LOCAL_7103", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=149.223.244.4)(PORT=1521)))(CONNECT_DATA=(SID = xe)));User Id=acc ;Password=acc" },
            { "LOCAL_7104", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=149.223.242.51)(PORT=1521)))(CONNECT_DATA=(SID = xe)));User Id=acc ;Password=acc" },
            { "LOCAL_7105", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=149.223.244.2)(PORT=1521)))(CONNECT_DATA=(SID = xe)));User Id=acc ;Password=acc" },
            //{ "LOCAL_RBN", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=ocalhost)(PORT=1521)))(CONNECT_DATA=(SID = xe)));User Id=acc ;Password=acc" },
            { "LOCAL_B416", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=149.223.244.46)(PORT=1521)))(CONNECT_DATA=(SID = xe)));User Id=acc ;Password=acc" },
            { "LOCAL_B600", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=149.223.244.42)(PORT=1521)))(CONNECT_DATA=(SID = xe)));User Id=acc ;Password=acc" },
            { "LOCAL_B404", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.213.7.1)(PORT=1521)))(CONNECT_DATA=(SID = xe)));User Id=acc ;Password=acc" },
            { "LOCAL_B414", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.213.6.1)(PORT=1521)))(CONNECT_DATA=(SID = xe)));User Id=acc ;Password=acc" },
            { "LOCAL_B403", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.213.11.1)(PORT=1521)))(CONNECT_DATA=(SID = xe)));User Id=acc ;Password=acc" },
            { "LOCAL_B413", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.213.10.1)(PORT=1521)))(CONNECT_DATA=(SID = xe)));User Id=acc ;Password=acc" },
            { "LOCAL_B405", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.213.9.1)(PORT=1521)))(CONNECT_DATA=(SID = xe)));User Id=acc ;Password=acc" },
            { "LOCAL_B415", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.213.28.1)(PORT=1521)))(CONNECT_DATA=(SID = xe)));User Id=acc ;Password=acc" },
            { "LOCAL_B406", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.213.90.1)(PORT=1521)))(CONNECT_DATA=(SID = xe)));User Id=acc ;Password=acc" },
            { "LOCAL_B417", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.213.89.1)(PORT=1521)))(CONNECT_DATA=(SID = xe)));User Id=acc ;Password=acc" },
            { "LOCAL_B418", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.213.88.1)(PORT=1521)))(CONNECT_DATA=(SID = xe)));User Id=acc ;Password=acc" },
            { "FACTORY_6103", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=149.223.242.10)(PORT=1521)))(CONNECT_DATA=(SID = tsspacc)));User Id=acc_eps1 ;Password=acc" },
            { "FACTORY_6104", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.214.6.19)(PORT=1521)))(CONNECT_DATA=(SID = bibpl12a)));User Id=acc_vweps1 ;Password=acc" },
            { "FACTORY_6105", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.214.6.19)(PORT=1521)))(CONNECT_DATA=(SID = bibpl12a)));User Id=acc_vweps2 ;Password=acc" },
            { "FACTORY_7103", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=149.223.242.10)(PORT=1521)))(CONNECT_DATA=(SID = tsspacc)));User Id=acc_col1 ;Password=acc" },
            { "FACTORY_7104", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.214.6.19)(PORT=1521)))(CONNECT_DATA=(SID = bibpl12a)));User Id=acc_vwcol1 ;Password=acc" },
            { "FACTORY_7105", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.214.6.19)(PORT=1521)))(CONNECT_DATA=(SID = bibpl12a)));User Id=acc_vwcol2 ;Password=acc" },
            { "FACTORY_RBN", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=149.223.242.10)(PORT=1521)))(CONNECT_DATA=(SID = tsspacc)));User Id=acc_rbna ;Password=acc" },
            { "FACTORY_B416", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=149.223.242.10)(PORT=1521)))(CONNECT_DATA=(SID = tsspacc)));User Id=acc_bracket ;Password=acc" },
            { "FACTORY_B600", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=149.223.242.10)(PORT=1521)))(CONNECT_DATA=(SID = tsspacc)));User Id=acc_d2xx_eps1 ;Password=acc" },
            { "FACTORY_B404", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=149.223.242.10)(PORT=1521)))(CONNECT_DATA=(SID = tsspacc)));User Id=acc_fiateps2 ;Password=acc" },
            { "FACTORY_B414", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=149.223.242.10)(PORT=1521)))(CONNECT_DATA=(SID = tsspacc)));User Id=acc_fiatcol2 ;Password=acc" },
            { "FACTORY_B403", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.214.6.19)(PORT=1521)))(CONNECT_DATA=(SID = bibpl12a)));User Id=acc_vweps4 ;Password=acc" },
            { "FACTORY_B413", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.214.6.19)(PORT=1521)))(CONNECT_DATA=(SID = bibpl12a)));User Id=acc_vwcol4 ;Password=acc" },
            { "FACTORY_B405", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=149.223.242.10)(PORT=1521)))(CONNECT_DATA=(SID = tsspacc)));User Id=acc_fordeps1 ;Password=acc" },
            { "FACTORY_B415", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=149.223.242.10)(PORT=1521)))(CONNECT_DATA=(SID = tsspacc)));User Id=acc_fordcol1 ;Password=acc" },
            { "FACTORY_B406", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.214.6.19)(PORT=1521)))(CONNECT_DATA=(SID = bibpl12a)));User Id=acc_flex_b406 ;Password=acc" },
            { "FACTORY_B417", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.214.6.19)(PORT=1521)))(CONNECT_DATA=(SID = bibpl12a)));User Id=acc_vwcol_b417 ;Password=acc" },
            { "FACTORY_B418", "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.214.6.19)(PORT=1521)))(CONNECT_DATA=(SID = bibpl12a)));User Id=acc_fordcol_b418 ;Password=acc" }
        };
        #endregion
    }


    public enum Line
    {
        ALL,
        EPS_FIAT,
        EPS_FIAT2,
        EPS_VW1,
        EPS_VW2,
        EPS_VW4,
        EPS_FORD,
        EPS_FLEX_FORD
    }
}
