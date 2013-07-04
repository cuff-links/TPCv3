using System;

namespace TPCv3.Models{
    public class PagingInfo{
        #region Public Properties

        public int CurrentPage { get; set; }

        public int ItemsPerPage { get; set; }

        public int TotalItems { get; set; }

        public string CurrentCategory { get; set; }

        public string CurrentTag { get; set; }

        public string CurrentSearchString { get; set; }

        public int TotalPages{
            get { return (int) Math.Ceiling((decimal) TotalItems/ItemsPerPage); }
        }

        #endregion
    }
}