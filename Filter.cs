using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPLTools.Filter
{
    public class Filter
    {
        /// <summary>
        /// Will return the number of segments. Operation is not case sensitive.
        /// </summary>
        /// <param name="from">The string from which the segment count is determined</param>
        /// <param name="seperator">The string seperator used when counting segments</param>
        /// <returns>The number of segments in the string</returns>
        public static int Count(string from, string seperator)
        {
            return Count(from, seperator, false);
        }

        /// <summary>
        /// Will return the number of segments. Operation is not case sensitive.
        /// </summary>
        /// <param name="from">The string from which the segment count is determined</param>
        /// <param name="seperator">The string seperator used when counting segments</param>
        /// <param name="caseSensitive">Determines if the operation is to be treated as case sensitive</param>
        /// <returns>The number of segments in the string</returns>
        public static int Count(string from, string seperator, bool caseSensitive)
        {
            if (seperator == "")
            {
                throw new ArgumentException("Seperator cannot be empty");
            }

            int result = 0;

            if (!caseSensitive)
            {
                from = from.ToUpper();
                seperator = seperator.ToUpper();
            }

            if (from.Length > 0)
            {
                result = 1;
                for (int ic = 0; ic < from.Length - seperator.Length; ic++)
                {
                    if (from.Substring(ic, seperator.Length) == seperator)
                    {
                        result++;
                    }
                }

            }
            return result;
        }

        /// <summary>
        /// Will return string segment. Operation is not case sensitive.
        /// </summary>
        /// <param name="from">Source string from which to extract segment</param>
        /// <param name="index">Segment index to return (Index starts at 1). If the segment cannot be found, an empty string will be returned.</param>
        /// <param name="seperator">The string seperator used when extracting segment</param>
        /// <returns>The string segment asked for</returns>
        public static string Get(string from, int index, string seperator)
        {
            return Get(from, index, seperator, false);
        }

        /// <summary>
        /// Will return string segment.
        /// </summary>
        /// <param name="from">Source string from which to extract segment</param>
        /// <param name="index">Segment index to return (Index starts at 1). If the segment cannot be found, an empty string will be returned.</param>
        /// <param name="seperator">The string seperator used when extracting segment</param>
        /// <param name="caseSensitive">Determines if the operation is to be treated as case sensitive</param>
        /// <returns>The string segment asked for</returns>
        public static string Get(string from, int index, string seperator, bool caseSensitive)
        {
            if (seperator == "")
                throw new ArgumentException("seperator cannot be empty");
            
            string result = "";
            string aFrom = from;
            if (!caseSensitive)
            {
                aFrom = aFrom.ToUpper();
                seperator = seperator.ToUpper();
            }

            if (index > Count(aFrom, seperator, caseSensitive))
            {
                return result;
            }

            if (aFrom.Length > 0)
            {
                if ((aFrom.Substring(0, seperator.Length) == seperator) && (index == 1))
                {
                    return result;
                }

                int CStart = 0;
                int CSlut = 0;
                int ATF = 1;
                int LastRecorded = 0;
                if (index == 1)
                {
                    CStart = 0;
                }

                for (int IC = 0; IC < aFrom.Length - seperator.Length; IC++)
                {
                    LastRecorded = IC;
                    if (aFrom.Substring(IC, seperator.Length) == seperator)
                    {
                        ATF++;
                        if (ATF == index)
                        {
                            CStart = IC + seperator.Length;
                        }
                        if (ATF == index + 1)
                        {
                            CSlut = IC - 1;
                        }
                    }
                }
                if ((CStart != 0) && (CSlut == 0))
                {
                    CSlut = LastRecorded + seperator.Length;
                }
                result = from.Substring(CStart, CSlut - CStart + 1);
            }
            return result;
        }

        /// <summary>
        /// Deletes a segment from the string. Operation is not Case Sensitive.
        /// </summary>
        /// <param name="from">Source string from which to remove segment</param>
        /// <param name="index">Segment index to delete (Index starts at 1). If the segment cannot be found, the original source string will be returned.</param>
        /// <param name="seperator">The string seperator used when extracting segment</param>
        /// <returns>The source string with the specified segment removed</returns>
        public static string Delete(string from, int index, string seperator)
        {
            return Delete(from, index, seperator, false);
        }

        /// <summary>
        /// Deletes a segment from the string.
        /// </summary>
        /// <param name="from">Source string from which to remove segment</param>
        /// <param name="index">Segment index to delete (Index starts at 1). If the segment cannot be found, the original source string will be returned.</param>
        /// <param name="seperator">The string seperator used when extracting segment</param>
        /// <param name="caseSensitive">Determines if the operation is to be treated as case sensitive</param>
        /// <returns>The source string with the specified segment removed</returns>
        public static string Delete(string from, int index, string seperator, bool caseSensitive)
        {
            string Finish = "";
            string TFrom = from;
            if (from.Length == 0) return Finish;
            if (!caseSensitive)
            {
                TFrom = TFrom.ToUpper();
                seperator = seperator.ToUpper();
            }
            int At = 1;
            int Start = 1;
            if (At == index) Start = 0;
            int theCount = 0;
            int Mat = 0;
            if ((Count(TFrom, seperator) - 1) == 0)
            {
                return Finish;
            }
            for (int ICount = 0; ICount < TFrom.Length - seperator.Length; ICount++)
            {
                if (TFrom.Substring(ICount, seperator.Length) == seperator)
                {
                    At++;
                    if (At == index) Start = ICount;
                    if (At == index + 1) theCount = ICount;
                }
                Mat = ICount;
            }
            if (At == index) theCount = Mat;
            if (theCount == 0) Finish = from;
            if (index == 1)
            {
                theCount = theCount + seperator.Length;
                Finish = from.Substring(theCount, from.Length - theCount);
            }
            if (index > 1)
            {
                Finish = from.Substring(0, Start) + from.Substring(theCount, from.Length - theCount);
            }
            if (index == Count(from, seperator))
            {
                Finish = from.Substring(0, Start);
            }
            return Finish;
        }

        /// <summary>
        /// Replace a string segment with another string segment. Operation is not Case-Sensitive.
        /// </summary>
        /// <param name="from">Source string to replace segment within.</param>
        /// <param name="index">Segment index to replace (Index starts at 1). If the segmeng cannot be found, the original source string will be returned.</param>
        /// <param name="replaceWith">Replace the segment with this</param>
        /// <param name="seperator">The string seperator used when replacing segment</param>
        /// <returns>The source string with the replaced segment</returns>
        public static string Replace(string from, int index, string replaceWith, string seperator)
        {
            return Replace(from, index, replaceWith, seperator, false);
        }

        /// <summary>
        /// Replace a string segment with another string segment.
        /// </summary>
        /// <param name="from">Source string to replace segment within.</param>
        /// <param name="index">Segment index to replace (Index starts at 1). If the segmeng cannot be found, the original source string will be returned.</param>
        /// <param name="replaceWith">Replace the segment with this</param>
        /// <param name="seperator">The string seperator used when replacing segment</param>
        /// <param name="caseSensitive">Determines if the operation is to be treated as case sensitive</param>
        /// <returns>The source string with the replaced segment</returns>
        public static string Replace(string from, int index, string replaceWith, string seperator, bool caseSensitive)
        {
            string preStr = "";
            string postStr = "";
            string TFrom = from;
            if (!caseSensitive)
            {
                TFrom = TFrom.ToUpper();
                seperator = seperator.ToUpper();
            }
            if (index > Count(TFrom, seperator, caseSensitive))
            {
                return from;
            }

            for (int i = 1; i != index; i++)
            {
                preStr = preStr + seperator + Get(from, i, seperator, caseSensitive);
            }
            for (int i = (index + 1); i <= Count(TFrom, seperator, caseSensitive); i++)
            {
                postStr = postStr + seperator + Get(from, i, seperator, caseSensitive);
            }

            string TempStr = preStr + seperator + replaceWith + postStr;
            if (TempStr.Length > 0)
            {
                if (TempStr.Substring(0, seperator.Length) == seperator)
                {
                    TempStr = TempStr.Substring(seperator.Length, TempStr.Length - seperator.Length);
                }
            }
            return TempStr;
        }

        public static List<string> Segments(string from, string seperator, bool caseSensitive)
        {
            List<string> result = new List<string>();

            for (int i = 1; i <= Filter.Count(from, seperator, caseSensitive); i++)
            {
                result.Add(Filter.Get(from, i, seperator, caseSensitive));
            }

            return result;
        }
    }
}
