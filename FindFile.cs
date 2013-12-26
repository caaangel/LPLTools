using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LPLTools.FindFile;

namespace LPLTools.FindFile
{
    public class NewStatusEventArgs : System.EventArgs
    {
        public NewStatusEventArgs(string status)
        {
            Status = status;
        }

        public string Status { get; set; }
    }

    public class FindFile
    {
        protected SearchController controller = null;
        protected List<FileInfo> resultList = new List<FileInfo>();

        public event EventHandler<NewStatusEventArgs> OnNewFolder;
        public event EventHandler<NewStatusEventArgs> OnNewFile;

        public FindFile(EventHandler<NewStatusEventArgs> onNewFolder, 
                        EventHandler<NewStatusEventArgs> onNewFile)
        {
            OnNewFile = onNewFile;
            OnNewFolder = onNewFolder;
        }

        private void DoOnNewFolder(string folder)
        {
            if (OnNewFolder != null)
            {
                OnNewFolder(this, new NewStatusEventArgs(folder));
            }
        }
        private void DoOnNewFile(string filename)
        {
            if (OnNewFile != null)
            {
                OnNewFile(this, new NewStatusEventArgs(filename));
            }
        }

        private bool IsWantedFile(FileInfo fileInfo)
        {
            return controller.SizeOptions.IsValidFile(fileInfo) &&
                   controller.DateOptions.IsValidFile(fileInfo);
        }
        
        private void SearchFolder(DirectoryInfo dirInfo)
        {
            DoOnNewFolder(dirInfo.FullName);

            FileInfo[] files = dirInfo.GetFiles(controller.FileMask);
            for (int i = 0; i < files.Length; i++)
            {
                DoOnNewFile(files[i].Name);
                if (IsWantedFile(files[i]))
                {
                    resultList.Add(files[i]);
                }
            }

            if (controller.SearchSubs)
            {
                DirectoryInfo[] folders = dirInfo.GetDirectories();
                for (int i = 0; i < folders.Length; i++)
                {
                    SearchFolder(folders[i]);
                }
            }
        }

        public List<FileInfo> Search(SearchController searchController)
        {
            if (searchController == null) return null;

            controller = searchController;
            resultList.Clear();

            DirectoryInfo dirInfo = new DirectoryInfo(controller.Folder);
            SearchFolder(dirInfo);

            return resultList;
        }
    }
}
