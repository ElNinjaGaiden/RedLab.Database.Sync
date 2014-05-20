using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedLab.Database.Sync
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length >= 3)
            {
                var checkOutFolder = args[0];
                var foldersToSync = args[1];
                var connectionString = args[2];

                Console.WriteLine("Checkout folder: {0}", checkOutFolder);
                Console.WriteLine("Subfolders to sync: {0}", foldersToSync);
                Console.WriteLine("Connection string: {0}", connectionString);

                var totalSyncedFiles = new Database().Sync(checkOutFolder, foldersToSync, connectionString);
                Console.WriteLine("Total synced files: {0}", totalSyncedFiles); 
            }
            else
            {
                Console.WriteLine("No arguments supplied");
            }

        }
    }
}
