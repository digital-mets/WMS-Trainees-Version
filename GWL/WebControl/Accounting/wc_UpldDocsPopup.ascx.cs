using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using GearsLibrary;
using DevExpress.Web;
using System.IO;

namespace GWL.WebControl
{
    public partial class wc_UpldDocsPopup : System.Web.UI.UserControl
    {
        public string HeaderText
        {
            get { return UpldDocsPopup.HeaderText; }
            set { UpldDocsPopup.HeaderText = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Initialize popup parameter
            if (!IsPostBack && Visible)
            {
            }
            if (Visible)
            {
                // gvDetail.DataBind();
            }
            #endregion
        }

        protected void OnFileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            ASPxUploadControl _UploadControl = sender as ASPxUploadControl;
            string strPathName = Page.MapPath("~/App_Data/");
            string strFileName = e.UploadedFile.FileName;
            Boolean IsSuccess = true;
            Int32 intCounter = 0;

            if (!Directory.Exists(strPathName))
            {
                Directory.CreateDirectory(strPathName);
            }

            do
            {
                try
                {
                    IsSuccess = true;
                    if (File.Exists(strPathName + strFileName))
                    {
                        File.Delete(strPathName + strFileName);
                    }
                    e.UploadedFile.SaveAs(strPathName + strFileName, false);
                }
                catch (Exception ex)
                {
                    IsSuccess = false;
                    intCounter++;
                    strFileName = "(" + intCounter.ToString() + ")" + e.UploadedFile.FileName;
                }
            }
            while (!IsSuccess && intCounter <= 20);

            if (IsSuccess)
            {
                if (Session["_FileList"] == null)
                {
                    Session["_FileList"] = strFileName;
                }
                else
                {
                    Session["_FileList"] += "|" + strFileName;
                }
            }
            else
            {
                e.ErrorText = "Error encountered uploading file" + e.UploadedFile.FileName;
                e.IsValid = false;
                if (Session["_ErrorMssg"] == null)
                {
                    Session["_ErrorMssg"] = e.UploadedFile.FileName;
                }
                else
                {
                    Session["_ErrorMssg"] += ", " + e.UploadedFile.FileName;
                }
            }
        }

        protected void OnFilesUploadComplete(object sender, FilesUploadCompleteEventArgs e)
        {
            ASPxUploadControl _UploadControl = sender as ASPxUploadControl;
            string test = e.ErrorText;
            e.CallbackData = Session["_FileList"].ToString();
            if (Session["_ErrorMssg"] == null) {
                e.ErrorText = "";
            }
            else
            {
                e.ErrorText = Session["_ErrorMssg"].ToString();
                // ^^^ Do File Clean Up
            }
            Session["_FileList"] = null;
            Session["_ErrorMssg"] = null;
        }
        protected void cp_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            String strPath = "", strUFolder = "", strDFolder = "";

            DataTable dtTemp = Gears.RetriveData2(
                "SELECT DPATH.Value AS DocsPath, UPLD.Value AS UploadFolder, DOCS.Value AS DocsFolder " +
                "FROM IT.SystemSettings DPATH " +
                "LEFT JOIN IT.SystemSettings UPLD ON UPLD.Code = 'DOCSUPLOAD' " +
                "LEFT JOIN IT.SystemSettings DOCS ON DOCS.Code = 'DOCSFOLDER' " +
                "WHERE DPATH.Code = 'DOCSPATH'"
                , Session["ConnString"].ToString());

            if (dtTemp.Rows.Count > 0)
            {
                strPath = dtTemp.Rows[0]["DocsPath"].ToString() + "\\";
                strUFolder = dtTemp.Rows[0]["UploadFolder"].ToString() + "\\";
                strDFolder = dtTemp.Rows[0]["DocsFolder"].ToString() + "\\";
            }
            dtTemp.Dispose();

            if (strPath == null)
            {
                cp.JSProperties["cp_errormssg"] = "Missing required system settings";
                return;
            }

            #region Upload
            if (e.Parameter.Substring(0, 7) == "Upload:")
            {
                if (txtDocNumber.Text == "")
                {
                    cp.JSProperties["cp_errormssg"] = "Please provide all required parameters";
                }
                else
                {
                    try
                    {
                        // 2023-12-04   TL  Added indicator for auxiliary file
                        String strTableName = (txtDocType.Text == "AUX" ? "IT.AuxiliaryDocs" : "IT.Documents");
                        // 2023-12-04   TL  (End)
                        String strTransType = txtTransType.Text;
                        String strDocNumber = txtDocNumber.Text;
                        int _Index = 0;

                        if (txtMode.Text == "APPEND") {
                            dtTemp = Gears.RetriveData2(
                                "SELECT FileCount FROM " + strTableName + " " +
                                "WHERE TransType = '" + strTransType + "' AND DocNumber = '" + strDocNumber + "'"
                                , Session["ConnString"].ToString());
                            if (dtTemp.Rows.Count > 0)
                            {
                                _Index = Convert.ToInt16(dtTemp.Rows[0][0]);
                            }
                            dtTemp.Dispose();
                        }

                        String strPathName = strPath + strDFolder;
                        String strSrcePath = Page.MapPath("~/App_Data/");
                        String[] SrceFiles = e.Parameter.Substring(7).Split('|');
                        String[] DestFiles = new String[SrceFiles.Length];
                        String strFileList = "";
                        Boolean _Rejected = false;

                        // 2023-12-15   TL  Clear out old files first if prior docs has been rejected
                        if (txtMode.Text == "UPLOAD" && txtDocType.Text != "AUX") {
                            dtTemp = Gears.RetriveData2(
                                "SELECT ISNULL(FileList,'') AS FileList FROM IT.Documents " +
                                "WHERE TransType = '" + strTransType + "' AND DocNumber = '" + strDocNumber + "' AND ISNULL(Rejected,0) = 1"
                                , Session["ConnString"].ToString());
                            
                            if (dtTemp.Rows.Count > 0)
                            {
                                _Rejected = true;
                                foreach (DataRow _row in dtTemp.Rows)
                                {
                                    String[] _FileList = _row["FileList"].ToString().Split('|');
                                    for (int i = 0; i < _FileList.Length; i++)
                                    {
                                        if (File.Exists(strPathName + "\\" + _FileList[i]))
                                        {
                                            File.Delete(strPathName + "\\" + _FileList[i]);
                                        }
                                    }
                                }
                            }
                            dtTemp.Dispose();
                        }
                        // 2023-12-15   TL  (End)

                        // 2023-12-04   TL  Added indicator for auxiliary file
                        String strPrefix = (txtDocType.Text == "AUX" ? "A" : "");
                        // 2023-12-04   TL  (End)
                        for (int i = 0; i < SrceFiles.Length; i++)
                        {
                            DestFiles[i] = strTransType + '-' + strDocNumber + "(" + strPrefix + (_Index+i+1).ToString() + ") " + SrceFiles[i];
                            if (File.Exists(strPathName + DestFiles[i]))
                            {
                                File.Delete(strPathName + DestFiles[i]);
                            }
                            File.Move(strSrcePath + SrceFiles[i], strPathName + DestFiles[i]);
                            strFileList = strFileList + "|" + DestFiles[i];
                        }
                        strFileList = strFileList.Substring(1);

                        String LastEditedBy = Session["userid"].ToString();
                        String LastEditedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        String _SQLcmd;
                        if (txtMode.Text == "UPLOAD") {
                            // 2023-12-04   TL  Added Auxiliary File
                            if (txtDocType.Text == "AUX") {
                                _SQLcmd =
                                    "INSERT INTO IT.AuxiliaryDocs " +
                                    "(TransType, DocNumber, FileList, FileCount) " +
                                    "VALUES " +
                                    "('" + strTransType + "','" + strDocNumber + 
                                    "','" + strFileList + "'," + (_Index + SrceFiles.Length).ToString() +")";
                            }
                            else {
                            // 2023-12-04   TL  (End)
                                if (_Rejected)
                                {
                                    _SQLcmd =
                                        "UPDATE IT.Documents " +
                                        "SET FileList = '" + strFileList +
                                        "',FileCount = " + (_Index + SrceFiles.Length).ToString() +
                                        " ,LastUploaded = GETDATE(), Rejected = 0" +
                                        " ,Received = CASE WHEN ISNULL((SELECT Transmittal FROM IT.TransTypeParam WHERE TransType = '" + strTransType + "'),0) = 0 THEN 'N/A' ELSE '-N-' END" +
                                        " ,Complete = CASE WHEN ISNULL((SELECT ForCompletion FROM IT.TransTypeParam WHERE TransType = '" + strTransType + "'),0) = 0 THEN 1 ELSE 0 END" +
                                        " ,Verified = CASE WHEN ISNULL((SELECT ForVerify FROM IT.TransTypeParam WHERE TransType = '" + strTransType + "'),0) = 0 THEN 1 ELSE 0 END" +
                                        " WHERE TransType = '" + strTransType + "' AND DocNumber = '" + strDocNumber + "'";
                                }
                                else
                                {
                                    _SQLcmd =
                                        "INSERT INTO IT.Documents " +
                                        "(TransType, DocNumber, DocDate, FileList, FileCount, UploadedBy, DateUploaded, Received, Complete, Verified) " +
                                        "VALUES " +
                                        "('" + strTransType + "','" + strDocNumber +
                                        "','" + Convert.ToDateTime(dteDocDate.Value).ToString("yyyy-MM-dd") +
                                        "','" + strFileList + "'," + (_Index + SrceFiles.Length).ToString() +
                                        ",'" + LastEditedBy + "','" + LastEditedDate + "'" +
                                        ",CASE WHEN ISNULL((SELECT Transmittal FROM IT.TransTypeParam WHERE TransType = '" + strTransType + "'),0) = 0 THEN 'N/A' ELSE '-N-' END" +
                                        ",CASE WHEN ISNULL((SELECT ForCompletion FROM IT.TransTypeParam WHERE TransType = '" + strTransType + "'),0) = 0 THEN 1 ELSE 0 END" +
                                        ",CASE WHEN ISNULL((SELECT ForVerify FROM IT.TransTypeParam WHERE TransType = '" + strTransType + "'),0) = 0 THEN 1 ELSE 0 END)";
                                }
                            }
                        }
                        else {
                            _SQLcmd =
                                "UPDATE IT.Documents " +
                                "SET FileList = FileList + '|" + strFileList +
                                "',FileCount = " + (_Index + SrceFiles.Length).ToString() +
                                " WHERE TransType = '" + strTransType + "' AND DocNumber = '" + strDocNumber + "'";
                        }

                        string strErrmssg = ExecuteSQL(_SQLcmd);

                        if (strErrmssg == "")
                        {
                            Entity.Functions.AuditTrail(strTransType, strDocNumber,
                                LastEditedBy, LastEditedDate, txtMode.Text, HttpContext.Current.Session["ConnString"].ToString());

                            cp.JSProperties["cp_uploadmssg"] = SrceFiles.Length.ToString() + " File(s) have been uploaded ";
                        }
                        else
                        {
                            cp.JSProperties["cp_errormssg"] = strErrmssg;
                        }
                    }
                    catch (Exception ex)
                    {
                        cp.JSProperties["cp_errormssg"] = "Unable to continue with upload \nPlease provide following message to IT for detail:\n" + ex.ToString();
                    }
                }
            }
            #endregion
            #region GetDir
            else if (e.Parameter.Substring(0, 7) == "GetDir:")
            {
                if (cboFileList.ClientEnabled && cboFileList.ClientVisible)
                {
                    cboFileList.Items.Clear();
                    String[] FileList = Directory.GetFiles(strPath + strUFolder);
                    for (int i = 0; i < FileList.Length; i++)
                    {
                        cboFileList.Items.Add(Path.GetFileName(FileList[i]));
                    }
                }
            }
            #endregion
            #region Transfer
            else if (e.Parameter.Substring(0, 9) == "Transfer:")
            {
                try
                {
                    String strPathName = strPath + strDFolder;
                    String strTransType = txtTransType.Text;
                    String strDocNumber = txtDocNumber.Text;
                    int _Index = 0;

                    if (txtMode.Text == "APPEND")
                    {
                        dtTemp = Gears.RetriveData2(
                            "SELECT FileCount FROM IT.Documents " +
                            "WHERE TransType = '" + strTransType + "' AND DocNumber = '" + strDocNumber + "'"
                            , Session["ConnString"].ToString());
                        if (dtTemp.Rows.Count > 0)
                        {
                            _Index = Convert.ToInt16(dtTemp.Rows[0][0]);
                        }
                        dtTemp.Dispose();
                    }

                    String strDestFile = strTransType + '-' + strDocNumber + "(" + (_Index+1).ToString() + ") " + cboFileList.Text;
                    if (File.Exists(strPathName + strDestFile))
                    {
                        File.Delete(strPathName + strDestFile);
                    }
                    String _CurrentDir = Directory.GetCurrentDirectory();
                    Directory.SetCurrentDirectory(strPath + strUFolder);
                    File.Move(cboFileList.Text, "..\\" + strDFolder + strDestFile);
                    Directory.SetCurrentDirectory(_CurrentDir);

                    String LastEditedBy = Session["userid"].ToString();
                    String LastEditedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    String _SQLcmd;
                    if (txtMode.Text == "UPLOAD")
                    {
                        _SQLcmd =
                            "INSERT INTO IT.Documents " +
                            "(TransType, DocNumber, DocDate, FileList, FileCount, UploadedBy, DateUploaded) " +
                            "VALUES " +
                            "('" + strTransType + "','" + strDocNumber +
                            "','" + Convert.ToDateTime(dteDocDate.Value).ToString("yyyy-MM-dd") +
                            "','" + strDestFile + "'," + (_Index + 1).ToString() +
                            ",'" + LastEditedBy + "','" + LastEditedDate + "')";
                    }
                    else
                    {
                        _SQLcmd =
                            "UPDATE IT.Documents " +
                            "SET FileList = FileList + '|" + strDestFile +
                            "',FileCount = " + (_Index + 1).ToString() +
                            " WHERE TransType = '" + strTransType + "' AND DocNumber = '" + strDocNumber + "'";
                    }

                    string strErrmssg = ExecuteSQL(_SQLcmd);

                    Entity.Functions.AuditTrail(strTransType, strDocNumber,
                        LastEditedBy, LastEditedDate, txtMode.Text, HttpContext.Current.Session["ConnString"].ToString());

                    cp.JSProperties["cp_uploadmssg"] = "File have been transferred";
                }
                catch (Exception ex)
                {
                    cp.JSProperties["cp_errormssg"] = "Unable to continue with transfer \nPlease provide following message to IT for detail:\n" + ex.ToString();
                }
            }
            #endregion
        }

        protected String GetExtension(string _PathName)
        {
            int intIndex;
            string strExtension;
            strExtension = _PathName;
            intIndex = strExtension.LastIndexOf('.');
            if (intIndex == -1)
            {
                strExtension = "";
            }
            else
            {
                strExtension = strExtension.Substring(intIndex);
            }
            return strExtension;
        }

        protected String ExecuteSQL(String strSQLCmd)
        {
            String strErrMssg = "";
            using (SqlConnection sqlconn = new SqlConnection(Session["ConnString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand(strSQLCmd))
                {
                    try
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 600;
                        sqlconn.Open();
                        cmd.Connection = sqlconn;
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        strErrMssg = ex.ToString();
                    }
                    sqlconn.Close();
                }
            }

            return strErrMssg;
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }
    }
}