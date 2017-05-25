using System;

namespace GoogleDriveNC
{
    class Program
    {
        static void Main(string[] args)
        {
            string credentialPath = "C:/Users/Lily/";

            SyncFile gDrive = new SyncFile(credentialPath);
            //gDrive.testCredential();
            string fileUploadId = gDrive.uploadFile("bill_12_12_2017.csv", "i hope it works well", null, "application/vnd.google-apps.spreadsheet", "D:/work/bill_12_12_2017.csv");

            System.Console.WriteLine("upload successfully " + fileUploadId);
            System.Console.ReadKey(true);
        }
    }
}