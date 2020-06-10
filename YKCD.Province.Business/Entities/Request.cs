using System;
using System.Collections.Generic;
using System.Linq;
using PetaPoco;
using YKCD.Province.Business.EntityBase;
using YKCD.Province.Business.Services;
using Framework.Helper;

namespace YKCD.Province.Business.Entities
{
    [Serializable]
    public class Request : RequestBase
    {
        [Ignore]
        public List<Perform> Performs => PerformServices.GetList(RequestID);

        [Ignore]
        public List<Agency> Agencies => Performs.Select(item => item.Agency).ToList();

        [Ignore]
        public Document Document => DocumentServices.GetById(DocumentID);

        [Ignore]
        public string RequestStatusString => Status.RequestStatusToLabel();

        [Ignore]
        public string PerformStatusString => CommonServices.ShowPerformObjects(RequestID);

        [Ignore]
        public List<Tracking> Trackings => TrackingServices.GetList(RequestID);

        [Ignore]
        public List<User> Trackers => Trackings.Select(tracking => tracking.Tracker).ToList();

        [Ignore]
        public string TrackerString => Trackers.Select(item => item.FullName).Distinct().ToList().DisplayInBreakLine();

        [Ignore]
        public List<Report> Reports => ReportServices.GetList(RequestID);

        [Ignore]
        public bool CoQuyenBaoCao => CommonServices.CheckReportPermission(RequestID);

        [Ignore]
        public bool CoQuyenCapNhat => CommonServices.CheckUpdatePermission(RequestID);

        [ResultColumn]
        public int PerformID { get; set; }

        [ResultColumn]
        public bool? IsSynced { get; set; } = false;

        [ResultColumn]
        public string DocumentCode { get; set; }

        [ResultColumn]
        public string MainContent { get; set; }
    }
}
