using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace SQLCrypt.FunctionalClasses
{
    internal class SearchManager
    {

        public ScintillaNET.Scintilla TextArea { get; set; }
        private string LastSearch { get; set; } = "";
        private int LastSearchIndex { get; set; }
                

        public int Find(bool next, bool incremental, int start, string toFind)
        {
            bool first = LastSearch != toFind;

            if (start >= 0)
                LastSearchIndex = start;

            LastSearch = toFind;
            if (LastSearch.Length > 0)
            {

                if (next)
                {

                    // SEARCH FOR THE NEXT OCCURANCE

                    // Search the document at the last search index
                    TextArea.TargetStart = LastSearchIndex - 1;
                    TextArea.TargetEnd = LastSearchIndex + (LastSearch.Length + 1);
                    TextArea.SearchFlags = SearchFlags.None;

                    // Search, and if not found..
                    if (!incremental || TextArea.SearchInTarget(LastSearch) == -1)
                    {

                        // Search the document from the caret onwards
                        TextArea.TargetStart = TextArea.CurrentPosition;
                        TextArea.TargetEnd = TextArea.TextLength;
                        TextArea.SearchFlags = SearchFlags.None;

                        // Search, and if not found..
                        if (TextArea.SearchInTarget(LastSearch) == -1)
                        {

                            // Search again from top
                            TextArea.TargetStart = 0;
                            TextArea.TargetEnd = TextArea.TextLength;

                            // Search, and if not found..
                            if (TextArea.SearchInTarget(LastSearch) == -1)
                            {

                                // clear selection and exit
                                TextArea.ClearSelections();

                                return -1;
                            }
                        }

                    }

                }
                else
                {

                    // SEARCH FOR THE PREVIOUS OCCURANCE

                    // Search the document from the beginning to the caret
                    TextArea.TargetStart = 0;
                    TextArea.TargetEnd = TextArea.CurrentPosition;
                    TextArea.SearchFlags = SearchFlags.None;

                    // Search, and if not found..
                    if (TextArea.SearchInTarget(LastSearch) == -1)
                    {

                        // Search again from the caret onwards
                        TextArea.TargetStart = TextArea.CurrentPosition;
                        TextArea.TargetEnd = TextArea.TextLength;

                        // Search, and if not found..
                        if (TextArea.SearchInTarget(LastSearch) == -1)
                        {

                            // clear selection and exit
                            TextArea.ClearSelections();
                            return -1;
                        }
                    }

                }

                // Select the occurance
                LastSearchIndex = TextArea.TargetStart;
                TextArea.SetSelection(TextArea.TargetEnd, TextArea.TargetStart);
                TextArea.ScrollCaret();
                return TextArea.TargetStart;

            }
            return -1;
        }


    }
}
