using BusinessLayer.AppStorage.FileResults;
using System;

namespace BusinessLayer.ReturnResult
{
    public class clsReturnResult
    {
        public enum enResult { Success, Error, InvalidInputs, BotNotFound }

        public string Detail { get; private set; }

        public enResult Result { get; private set; }

        internal clsReturnResult(enResult result, string detail = null)
        {
            this.Detail = (detail == null) ? 
                null : $"[{DateTime.Now.ToString()}] " + detail;

            this.Result = result;
        }


        internal static clsReturnResult FileReturnResultMaker(clsFileResults.enFileResult Result)
        {
            switch (Result)
            {
                case clsFileResults.enFileResult.NotExist:
                    return new clsReturnResult(enResult.Error, "The file is not found.");
                case clsFileResults.enFileResult.InvalidInputs:
                    return new clsReturnResult(enResult.Error, "Invalid date");
                case clsFileResults.enFileResult.Empty:
                    return new clsReturnResult(enResult.Error, "The file is empty.");
                case clsFileResults.enFileResult.Success:
                    return new clsReturnResult(enResult.Success,"The operation was succussed.");
                default:
                    return new clsReturnResult(enResult.Error, "Unknown Error.");
            }
        }
    }
}
