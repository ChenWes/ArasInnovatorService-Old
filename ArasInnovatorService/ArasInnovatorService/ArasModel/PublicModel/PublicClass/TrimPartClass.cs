using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ArasInnovatorService.Common;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class TrimPartClass
    {
        private int _iDisplayPageIndex;
        public int DisplayPageIndex
        {
            get
            {
                if (this._iDisplayPageIndex == 0)
                {
                    this._iDisplayPageIndex = int.Parse(ConfigHelper.GetAPPConfigValue("DefaultPageIndex"));
                }
                return this._iDisplayPageIndex;
            }
            set
            {
                this._iDisplayPageIndex = value;
            }
        }

        private int _iDisplayPageSize;
        public int DisplayPageSize
        {
            get
            {
                if (this._iDisplayPageSize == 0)
                {
                    this._iDisplayPageSize = int.Parse(ConfigHelper.GetAPPConfigValue("DefaultPageSize"));
                }
                return this._iDisplayPageSize;
            }
            set
            {
                this._iDisplayPageSize = value;
            }
        }

        private bool _successFlag = true;
        public bool SuccessFlag
        {
            get
            {
                return this._successFlag;
            }
            set
            {
                this._successFlag = value;
            }
        }

        private string _sErrorString = "";
        public string ErrorString
        {
            get
            {
                return this._sErrorString;
            }
            set
            {
                this._sErrorString = value;
            }
        }

        private string _sErrorDetail = "";
        public string ErrorDetail
        {
            get
            {
                return this._sErrorDetail;
            }
            set
            {
                this._sErrorDetail = value;
            }
        }

        private string _sErrorCode = "";
        public string ErrorCode
        {
            get
            {
                return this._sErrorCode;
            }
            set
            {
                this._sErrorCode = value;
            }
        }

        //get image path
        private string _sGetReturnString = "";
        public string GetReturnString
        {
            get
            {
                return this._sGetReturnString;
            }
            set
            {
                this._sGetReturnString = value;
            }
        }

        //delete or get operation ID
        private string _sOperationID = "";
        public string OperationID
        {
            get { return this._sOperationID; }
            set { this._sOperationID = value; }
        }

        private eumImageType _eGetImageType;
        public eumImageType GetImageType
        {
            get
            {
                if (this._eGetImageType == null)
                {
                    return eumImageType.UsePrimaryPath;
                }
                else
                {
                    return this._eGetImageType;
                }
            }
            set { this._eGetImageType = value; }
        }        

        private SelectionFilter _selectionFilter = null;
        public SelectionFilter SelectionFilter
        {
            get { return this._selectionFilter; }
            set { this._selectionFilter = value; }
        }


        public TrimPartList TrimPartList { get; set; }        
    }
}