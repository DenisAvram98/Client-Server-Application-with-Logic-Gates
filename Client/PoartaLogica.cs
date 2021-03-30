using System;
using System.Data;
using System.Drawing;
using MindFusion.Diagramming;

namespace Client
{
    internal class PoartaLogica
    {
        internal static AnchorPattern Ancorare(int index)
        {
            AnchorPattern ap = new AnchorPattern();
            AnchorPoint apo1, apo2, apo3;
            
            if (index==0) //AND
            {
                apo1 = new AnchorPoint(0, 25, true, false, MarkStyle.Circle, Color.Blue);
                apo2 = new AnchorPoint(0, 57, true, false, MarkStyle.Circle, Color.Blue);
                apo3 = new AnchorPoint(89, 42, false, true, MarkStyle.Circle, Color.Green);

                ap.Points.Add(apo1);
                ap.Points.Add(apo2);
                ap.Points.Add(apo3);
                return ap;
            }
            else if (index == 1) //OR
            {
                apo1 = new AnchorPoint(0, 27, true, false, MarkStyle.Circle, Color.Blue);
                apo2 = new AnchorPoint(0, 59, true, false, MarkStyle.Circle, Color.Blue);
                apo3 = new AnchorPoint(89, 44, false, true, MarkStyle.Circle, Color.Green);

                ap.Points.Add(apo1);
                ap.Points.Add(apo2);
                ap.Points.Add(apo3);
                return ap;
            }
            else if (index==2) //XOR
            {
                apo1 = new AnchorPoint(0, 27, true, false, MarkStyle.Circle, Color.Blue);
                apo2 = new AnchorPoint(0, 59, true, false, MarkStyle.Circle, Color.Blue);
                apo3 = new AnchorPoint(89, 44, false, true, MarkStyle.Circle, Color.Green);

                ap.Points.Add(apo1);
                ap.Points.Add(apo2);
                ap.Points.Add(apo3);
                return ap;
            }
            else if (index == 3) //NOT
            {
                apo1 = new AnchorPoint(0, 45, true, false, MarkStyle.Circle, Color.Blue);
                apo2 = new AnchorPoint(89, 45, false, true, MarkStyle.Circle, Color.Green);

                ap.Points.Add(apo1);
                ap.Points.Add(apo2);
                return ap;
            }
            else if (index == 4) //INPUT 0
            {
                apo1 = new AnchorPoint(89, 43, false, true, MarkStyle.Circle, Color.Green);

                ap.Points.Add(apo1);
                return ap;
            }
            else if (index==5) //INPUT 1
            {
                apo1 = new AnchorPoint(89, 43, false, true, MarkStyle.Circle, Color.Green);

                ap.Points.Add(apo1);
                return ap;
            }
            else if (index == 6) //OUTPUT (terminator)
            {
                apo1 = new AnchorPoint(0, 43, true, false, MarkStyle.Circle, Color.Blue);

                ap.Points.Add(apo1);
                return ap;
            }
            else
            {
                return null;
            }
        }

        internal static bool TwoInOneOut (int inCount, int outCount) //functie ajutatoare VerificareLegaturi
        {
            if (inCount == 2 && outCount == 1)
            {
                return true;
            }
            return false;
        }

        internal static bool ZeroInOneOut (int inCount, int outCount) //functie ajutatoare VerificareLegaturi
        {
            if (inCount==0 && outCount==1)
            {
                return true;
            }
            return false;
        }

        internal static bool VerificareLegaturi(int index, int inCount, int outCount)
        {
            if (index == 0) //AND
            {
                return TwoInOneOut(inCount, outCount);
            }
            else if (index == 1) //OR
            {
                return TwoInOneOut(inCount, outCount);
            }
            else if (index == 2) //XOR
            {
                return TwoInOneOut(inCount, outCount);
            }
            else if (index == 3) //NOT
            {
                if (inCount==1 && outCount==1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (index == 4) //INPUT 0
            {
                return ZeroInOneOut(inCount, outCount);
            }
            else if (index == 5) //INPUT 1
            {
                return ZeroInOneOut(inCount, outCount);
            }
            else if (index == 6) //OUTPUT (terminator)
            {
                if (inCount==1 && outCount==0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        internal static DataTable CreareTemplateDataTable()
        {
            DataTable dt = new DataTable();
            DataColumn dc;

            dc = new DataColumn();
            dc.ColumnName = "FirstSourceID";
            dc.DataType = Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "SecondSourceID";
            dc.DataType = Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "FirstSourceType";
            dc.DataType = Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "SecondSourceType";
            dc.DataType = Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "DestinationID";
            dc.DataType = Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "DestinationType";
            dc.DataType = Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "CurrentID";
            dc.DataType = Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "CurrentType";
            dc.DataType = Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "OutputResult";
            dc.DataType = Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "FirstInput";
            dc.DataType = Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "SecondInput";
            dc.DataType = Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "InputID";
            dc.DataType = Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "GateName";
            dc.DataType = Type.GetType("System.String");
            dt.Columns.Add(dc);

            return dt;
        }
    }
}