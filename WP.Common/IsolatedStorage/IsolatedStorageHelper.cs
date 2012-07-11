using System;
using System.IO.IsolatedStorage;
using System.IO;

namespace WP.Common.IsolatedStorage
{
    public class IsolatedStorageHelper
    {
        /// <summary>
        /// Property that returns current isolated storage for an Application
        /// </summary>
        public static IsolatedStorageFile CurrentIsolatedStorage
        {
            get
            {
                return IsolatedStorageFile.GetUserStoreForApplication();
            }
        }

        /// <summary>
        /// method for creating a directory within isolated storage
        /// </summary>
        /// <param name="directoryName">Name of a directory to be created</param>
        public static void CreateDirectory(string directoryName)
        {
            if (!string.IsNullOrEmpty(directoryName) && !CurrentIsolatedStorage.DirectoryExists(directoryName))
            {
                CurrentIsolatedStorage.CreateDirectory(directoryName);
            }
        }

        /// <summary>
        /// Method for deleting an isolated storage directory
        /// </summary>
        /// <param name="directoryName">Name of a directory to be deleted</param>
        public static void DeleteDirectory(string directoryName)
        {
            if (!string.IsNullOrEmpty(directoryName) && CurrentIsolatedStorage.DirectoryExists(directoryName))
            {
                CurrentIsolatedStorage.DeleteDirectory(directoryName);
            }
        }


        /// <summary>
        /// Method for creating a file in isolated storage
        /// </summary>
        /// <param name="directoryName">Path to directory</param>
        /// <param name="fileNameWithExtention">File name with extention</param>
        /// <param name="content">Content for a file</param>
        /// <param name="createNew">Indicates if a previous version of a file should be deleted before the creation</param>
        public static void CreateFile(string directoryName, string fileNameWithExtention, string content, bool createNew)
        {

            // if file name was not specified then do not create a file
            if (!string.IsNullOrEmpty(fileNameWithExtention))
                return;

            string filePath = GetFilePath(directoryName, fileNameWithExtention);

            if (createNew)
            {
                if (CurrentIsolatedStorage.FileExists(filePath))
                {
                    // if file exists - delete it before creating a new one
                    CurrentIsolatedStorage.DeleteFile(filePath);
                }
            }

            // open writer stream to write a content to a file
            var destinationFile = new StreamWriter(new IsolatedStorageFileStream(filePath, FileMode.OpenOrCreate, CurrentIsolatedStorage));
            
            // write content to a file in isolated storage
            destinationFile.WriteLine(content);
            
            // close writer stream
            destinationFile.Close();
            
        }

        public static string ReadFile(string directoryName, string fileNameWithExtention)
        {
            string content = null;

            try
            {
                if (!string.IsNullOrEmpty(fileNameWithExtention))
                {
                    // open a reader stream to read a file content for isolated storage
                    var fileToRead = new StreamReader(new IsolatedStorageFileStream(
                        GetFilePath(directoryName, fileNameWithExtention)
                        , FileMode.Open
                        , CurrentIsolatedStorage));

                    content = fileToRead.ReadLine();

                    // close reader stream
                    fileToRead.Close();
                }
            }
            catch
            {
                // do something with exception
            }

            return content;
        }


        /// <summary>
        /// Method for deleting a single file from Isolated Storage
        /// </summary>
        /// <param name="directoryName">Directory path for a file</param>
        /// <param name="fileNameWithExtention">File name with extention</param>
        public static void DeleteFile(string directoryName, string fileNameWithExtention)
        {
            if (!string.IsNullOrEmpty(fileNameWithExtention))
            {
                CurrentIsolatedStorage.DeleteFile(GetFilePath(directoryName, fileNameWithExtention));   
            }
        }


        /// <summary>
        /// Utility method that returns a file path based on directory name and input file name parameter
        /// </summary>
        public static string GetFilePath(string directoryName, string fileNameWithExtention)
        {
            if (directoryName != null && !directoryName.EndsWith("\\"))
                directoryName += "\\";

            return (directoryName + fileNameWithExtention);
        }

    }
}
