namespace TPCv3.Models{
    public class JqInViewModel{
        // no. of records to fetch

        // the page index

        #region Public Properties

        public int page { get; set; }

        public int rows { get; set; }

        // sort column name
        public string sidx { get; set; }

        // sort order "asc" or "desc"
        public string sord { get; set; }

        #endregion
    }
}