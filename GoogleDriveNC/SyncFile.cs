using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleDriveNC
{
    class SyncFile
    {
        static string ApplicationName = "Drive API .NET Quickstart";

        private DriveService drive = null;

        public SyncFile (string credentialPath)
        {
            initialSyncFile(credentialPath);
        }

        private void initialSyncFile (string credentialPath)
        {
            UserCredential authorization = Authorization.Authorize(credentialPath);

            //TODO: handle credentail creating error

            drive = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = authorization,
                ApplicationName = ApplicationName,
            });
        }

        private File getMetadata (String name, String description, String parentId, String mimeType)
        {
            var fileMetadata = new File()
            {
                Name = name,
                MimeType = mimeType,
                Description = description
            };

            return fileMetadata;
        }

        public String uploadFile (String name, String description, String parentId, String mimeType, String filePath)
        {
            File metadata = getMetadata(name, description, parentId, mimeType);

            FilesResource.CreateMediaUpload request;
            using (var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Open))
            {
                request = drive.Files.Create(metadata, stream, mimeType);
                request.Fields = "id";
                request.Upload();
            }
            var file = request.ResponseBody;

            return file.Id;
        }

        public void testCredential ()
        {
            FilesResource.ListRequest listRequest = drive.Files.List();
            listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
                .Files;
            Console.WriteLine("Files:");
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    Console.WriteLine("{0} ({1})", file.Name, file.Id);
                }
            }
            else
            {
                Console.WriteLine("No files found.");
            }
        }
    }
}
