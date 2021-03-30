using System;
using System.Data;

namespace Server
{
    internal class PoartaLogica
    {
        internal static int CalculRezultat(ref DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                IncarcareSiCalculValori(dr, ref dt);
            }

            //cautam elementul logic OUTPUT si returnam OutputResut
            foreach(DataRow dr in dt.Rows)
            {
                int CT = Int32.Parse(dr["CurrentType"].ToString());
                int OR = dr["OutputResult"] is "Server" ? Int16.MaxValue : Int32.Parse(dr["OutputResult"].ToString());

                if (CT==6)
                {
                    return OR;
                }
            }
            return Int16.MaxValue; //eroare, nu sa gasit rezultatul
        }

        private static void IncarcareSiCalculValori(DataRow dr, ref DataTable dt)
        {
            //Inexistent = Int16.MinValue; Server= Int16.MaxValue;
            int FSID = dr["FirstSourceID"].ToString() is "Inexistent" ? Int16.MinValue : Int32.Parse(dr["FirstSourceID"].ToString());
            int SSID = dr["SecondSourceID"].ToString() is "Inexistent" ? Int16.MinValue : Int32.Parse(dr["SecondSourceID"].ToString());
            int FST = dr["FirstSourceType"].ToString() is "Inexistent" ? Int16.MinValue : Int32.Parse(dr["FirstSourceType"].ToString());
            int SST = dr["SecondSourceType"].ToString() is "Inexistent" ? Int16.MinValue : Int32.Parse(dr["SecondSourceType"].ToString());
            int DID = dr["DestinationID"].ToString() is "Inexistent" ? Int16.MinValue : Int32.Parse(dr["DestinationID"].ToString());
            int DT = dr["DestinationType"].ToString() is "Inexistent" ? Int16.MinValue : Int32.Parse(dr["DestinationType"].ToString());
            int CID = Int32.Parse(dr["CurrentID"].ToString());
            int CT = Int32.Parse(dr["CurrentType"].ToString());
            int OR = dr["OutputResult"] is "Server" ? Int16.MaxValue : Int32.Parse(dr["OutputResult"].ToString());
            int FI = dr["FirstInput"] is "Server" ? Int16.MaxValue : Int32.Parse(dr["FirstInput"].ToString());
            int SI = dr["SecondInput"] is "Server" ? Int16.MaxValue : Int32.Parse(dr["SecondInput"].ToString());
            string IID = dr["InputID"] is DBNull ? "Inexistent" : dr["InputID"].ToString();
            string GN = dr["GateName"].ToString();

            bool bFI = Convert.ToBoolean(FI);
            bool bSI = Convert.ToBoolean(SI);
            bool bOR;

            if (CT == 4 || CT == 5) //Input 0, 1
            {
                return;
            }

            if (FST == 4 || FST == 5)
            {
                dr.BeginEdit();
                dr["FirstInput"] = FST is 4 ? 0 : 1;
                dr.EndEdit();
            }

            if (SST == 4 || SST == 5)
            {
                dr.BeginEdit();
                dr["SecondInput"] = SST is 4 ? 0 : 1;
                dr.EndEdit();
            }

            if ((FST == 4 || FST == 5) && (SST == 4 || SST == 5))
            {
                bool value1 = Convert.ToBoolean(FST is 4 ? 0 : 1);
                bool value2 = Convert.ToBoolean(SST is 4 ? 0 : 1);

                bOR = Operatii(CT, value1, value2);

                dr.BeginEdit();
                dr["OutputResult"] = Convert.ToInt32(bOR);
                dr.EndEdit();

                //facem update cu output result unde aceasta poarta este input
                UpdateOutputResultInAltePortiInput(ref dt, CID, bOR);
            }
            else if ((CT == 3 || CT == 6) && (FST == 4 || FST == 5)) //NOT, Output && Input 0, 1
            {
                bool value1 = Convert.ToBoolean(FST is 4 ? 0 : 1);
                bool value2 = Convert.ToBoolean(SST is 4 ? 0 : 1);

                bOR = Operatii(CT, value1, value2);

                dr.BeginEdit();
                dr["OutputResult"] = Convert.ToInt32(bOR);
                dr.EndEdit();

                //facem update cu output result unde aceasta poarta este input
                UpdateOutputResultInAltePortiInput(ref dt, CID, bOR);
            }
            //prima conditie sa fie indeplinita FirstInput trebuie sa fie initializat
            //cea de a doua conditie sa fie indeplinita trebuie ca FirstInput si SecondInput sa fie initializate
            //cazul pentru restul tipurilor de input (excludem Input 0, 1)
            else if (((CT == 3 || CT == 6) && FI != Int16.MaxValue) || (FI != Int16.MaxValue && SI != Int16.MaxValue))
            {
                bOR = Operatii(CT, bFI, bSI);

                dr.BeginEdit();
                dr["OutputResult"] = Convert.ToInt32(bOR);
                dr.EndEdit();

                //facem update cu output result unde aceasta poarta este input
                UpdateOutputResultInAltePortiInput(ref dt, CID, bOR);
            }
            else //cand poarta curenta are ca intrari ce trebuie mai intai calculate si apoi se calculeaza outputul porti curente
            {
                DataRow FirstInputRow = null;
                DataRow SecondInputRow = null;

                foreach(DataRow dr1 in dt.Rows)
                {
                    int CurrentID = Int32.Parse(dr1["CurrentID"].ToString());
                    if (CurrentID==FSID)
                    {
                        FirstInputRow = dr1;
                    }
                    if (CurrentID==SSID)
                    {
                        SecondInputRow = dr1;
                    }
                }

                //calculam intrarile
                if (CT==3 || CT==6) //NOT, Output
                {
                    IncarcareSiCalculValori(FirstInputRow, ref dt);
                    IncarcareSiCalculValori(dr, ref dt);
                }
                else //restul elementelor logige
                {
                    if (!(FST==4 || FST==5))
                    {
                        IncarcareSiCalculValori(FirstInputRow, ref dt);
                    }
                    if (!(SST==4 || SST==5))
                    {
                        IncarcareSiCalculValori(SecondInputRow, ref dt);
                    }
                    //IncarcareSiCalculValori(dr, ref dt);
                }
            }
        }

        private static void UpdateOutputResultInAltePortiInput(ref DataTable dt, int cID, bool bOR)
        {
            //facem update cu output result unde aceasta poarta este input
            foreach (DataRow dr in dt.Rows)
            {
                //string test = dr["FirstSourceID"].ToString();
                int FSID = dr["FirstSourceID"].ToString() is "Inexistent" ? Int16.MinValue : Int32.Parse(dr["FirstSourceID"].ToString());
                int SSID = dr["SecondSourceID"].ToString() is "Inexistent" ? Int16.MinValue : Int32.Parse(dr["SecondSourceID"].ToString());
                int CID = Int32.Parse(dr["CurrentID"].ToString());

                if (((FSID==cID) || (SSID==cID)) && (CID!=cID))
                {
                    dr.BeginEdit();
                    if (FSID==cID)
                    {
                        dr["FirstInput"] = Convert.ToInt32(bOR);
                    }
                    if (SSID==cID)
                    {
                        dr["SecondInput"] = Convert.ToInt32(bOR);
                    }
                    dr.EndEdit();
                }
            }
        }

        private static bool Operatii(int cT, bool value1, bool value2)
        {
            switch (cT)
            {
                case 0: //AND
                    return value1 & value2;
                case 1: //OR
                    return value1 | value2;
                case 2: //XOR
                    return value1 ^ value2;
                case 3: //NOT
                    return !value1;
                case 6: //OUTPUT
                    return value1;
                default:
                    return false;
            }
        }
    }
}