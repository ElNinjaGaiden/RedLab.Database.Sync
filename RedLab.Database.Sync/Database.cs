using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace RedLab.Database.Sync
{
    internal class Database
    {
        internal int Sync(string checkOutFolder, string foldersToSync, string connectionString)
        {
            var syncedFiles = 0;

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                try
                {
                    var _foldersToSync = foldersToSync.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var subFolder in _foldersToSync)
                    {
                        var filesToSync = ResolveSubFolderFiles(Path.Combine(checkOutFolder, subFolder));

                        foreach (var file in filesToSync)
                        {
                            Console.WriteLine("Executing {0}", file);
                            var query = File.ReadAllText(file, Encoding.GetEncoding("iso-8859-1"));

                            using (SqlCommand command = new SqlCommand(query, con))
                            {
                                command.ExecuteNonQuery();
                                syncedFiles++;
                            }
                        }
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }

            return syncedFiles;
        }

        internal List<string> ResolveSubFolderFiles(string folderPath)
        {
            var fileNames = new List<string>();

            if (Directory.Exists(folderPath))
            {
                fileNames.AddRange(Directory.EnumerateFiles(folderPath, "*.sql"));
            }

            return fileNames;
        }
    }
}
