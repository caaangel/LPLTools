using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LPLTools.FindFile
{
    public enum SearchOptions
    {
        DontCare = 0,
        LessThan = 1,
        Equal = 2,
        LargerThan = 3
    }
    public enum SizeMultiplier
    {
        Bytes = 0,
        Kilobytes = 1,
        Megabytes = 2,
        Gigabytes = 3
    }

    public class SizeOptions
    {
        private long CalcMultiplier()
        {
            int value = Convert.ToInt32(Multiplier);
            if (value == 0) return 1;

            double result = Math.Pow(1024, value);
            return Convert.ToInt64(result);
        }

        public SizeOptions()
        {
            Options = SearchOptions.DontCare;
            Value = 0;
            Multiplier = SizeMultiplier.Bytes;
        }
        public SizeOptions(SearchOptions options, long value, SizeMultiplier multiplier)
        {
            Options = options;
            Value = value;
            Multiplier = multiplier;
        }

        public SizeMultiplier Multiplier
        {
            get;
            set;
        }
        public SearchOptions Options
        {
            get;
            set;
        }
        public long Size
        {
            get
            {
                return Value * CalcMultiplier();
            }
        }
        public long Value
        {
            get;
            set;
        }

        public bool IsValidFile(FileInfo fileInfo)
        {
            switch (Options)
            {
                case SearchOptions.Equal:
                    {
                        return fileInfo.Length == Size;
                    }
                case SearchOptions.LargerThan:
                    {
                        return fileInfo.Length > Size;
                    }
                case SearchOptions.LessThan:
                    {
                        return fileInfo.Length < Size;
                    }
                default: // SearchOptions.DontCare
                    {
                        return true;
                    }
            }
        }
    }

    public class DateOptions
    {
        public DateOptions()
        {
            Options = SearchOptions.DontCare;
            Date = DateTime.Now;
        }
        public DateOptions(SearchOptions options, DateTime date)
        {
            Options = options;
            Date = date;
        }

        public DateTime Date
        {
            get;
            set;
        }
        public SearchOptions Options
        {
            get;
            set;
        }

        public bool IsValidFile(FileInfo fileInfo)
        {
            switch (Options)
            {
                case SearchOptions.Equal:
                    {
                        return fileInfo.LastWriteTime == Date;
                    }
                case SearchOptions.LargerThan:
                    {
                        return fileInfo.LastWriteTime > Date;
                    }
                case SearchOptions.LessThan:
                    {
                        return fileInfo.LastWriteTime < Date;
                    }
                default: // SearchOptions.DontCare
                    {
                        return true;
                    }
            }
        }
    }

    public class SearchController
    {
        public SearchController()
        {
            SizeOptions = new SizeOptions();
            DateOptions = new DateOptions();
            FileMask = @"*";
            Folder = "";
            SearchSubs = false;
        }

        public DateOptions DateOptions
        {
            get;
            set;
        }
        public string FileMask
        {
            get;
            set;
        }
        public string Folder
        {
            get;
            set;
        }
        public bool SearchSubs
        {
            get;
            set;
        }
        public SizeOptions SizeOptions
        {
            get;
            set;
        }
    }
}
