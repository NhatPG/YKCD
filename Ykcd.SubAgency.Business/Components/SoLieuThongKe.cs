namespace YKCD.SubAgency.Business.Components
{
    public class SoLieuThongKe
    {
        public int ObjectID { get; set; }
        public string ObjectName { get; set; }
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public int NotPerform { get; set; }
        public int NotPerformInTerm { get; set; }
        public int NotPerformOutTerm { get; set; }
        public int Performing { get; set; }
        public int PerformingInTerm { get; set; }
        public int PerformingOutTerm { get; set; }
        public int Done { get; set; }
        public int DoneInTerm { get; set; }
        public int DoneOutTerm { get; set; }
        public int WaitToConfirm { get; set; }
        public int Total { get; set; }
    }
}