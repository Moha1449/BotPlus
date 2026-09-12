using System;

namespace DataModelLayer.ReturnResult
{
    public class clsReturnResult
    {
        public enum enResult { Success, Error, InvalidInputs, NotFound ,EmptyResult}

        public string Detail { get; private set; }

        public enResult Result { get; private set; }

        public clsReturnResult(enResult result, string detail = null)
        {
            this.Detail = (detail == null) ? 
                null : $"[{DateTime.Now.ToString()}] " + detail;

            this.Result = result;
        }
    }
}
