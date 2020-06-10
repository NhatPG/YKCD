using System;
using System.Collections.Generic;
using System.Linq;
using PetaPoco;
using YKCD.SubAgency.Business.EntityBase;
using YKCD.SubAgency.Business.Services;
using Framework.Helper;

namespace YKCD.SubAgency.Business.Entities
{
    [Serializable]
    [TableName("Requests")]
    [PrimaryKey("RequestID", AutoIncrement = true)]
    public class Request : RequestBase
    {
        [Ignore]
        public List<Perform> Performs => PerformServices.GetList(RequestID).Where(item => item.Department != null || item.User != null).ToList();

        [Ignore]
        public List<Department> Departments => Performs.Where(p => p.DepartmentID > 0).Select(item => item.Department).ToList();

        [Ignore]
        public List<User> Users => Performs.Where(p => p.UserID > 0).Select(item => item.User).ToList();

        [Ignore]
        public Document Document => DocumentID > 0 ? DocumentServices.GetById(DocumentID) : new Document();

        [Ignore]
        public string RequestStatusLabel => Status.ToLabel();

        [Ignore]
        public string RequestStatusString => Status.ToText();

        [Ignore]
        public string PerformStatusString => CommonServices.ShowPerformObjects(RequestID);

        [Ignore]
        public List<Tracking> Trackings => TrackingServices.GetList(RequestID);

        [Ignore]
        public List<User> Trackers => TrackingServices.GetList(RequestID)?.Where(t => t != null).Select(t => t.Tracker).ToList();

        [Ignore]
        public string TrackerString => Trackers?.Where(item => item != null).Distinct().Select(item => item?.FullName).ToList().DisplayInBreakLine();

        [Ignore]
        public List<Report> Reports => ReportServices.GetList(RequestID)?.OrderBy(report => report.CreatedBy)?.ThenBy(report => report.CreatedTime)?.ToList();

        [Ignore]
        public bool CoQuyenBaoCao => CommonServices.CheckReportPermission(RequestID);

        /// <summary>
        /// Kiểm tra quyền cập nhật ý kiến chỉ đạo
        /// </summary>
        [Ignore]
        public bool CoQuyenCapNhat => CommonServices.CheckUpdatePermission(requestID: RequestID);

        [Ignore]
        public bool CoQuyenXacNhan => CommonServices.CheckConfirmPermission(requestID: RequestID);


        [ResultColumn]
        public string DocumentCode { get; set; }

        [ResultColumn]
        public string MainContent { get; set; }
    }
}
