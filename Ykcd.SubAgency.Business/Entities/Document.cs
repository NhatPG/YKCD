using System;
using System.Collections.Generic;
using PetaPoco;
using YKCD.SubAgency.Business.EntityBase;
using YKCD.SubAgency.Business.Services;

namespace YKCD.SubAgency.Business.Entities
{
    [Serializable]
    public class Document : DocumentBase
    {
        [Ignore]
        public User Signer => SignerID > 0 ? UserServices.GetById(SignerID) : new User();

        [Ignore]
        public User Writer => WriterID > 0 ? UserServices.GetById(WriterID) : new User();

        [Ignore]
        public List<File> Files => FileServices.GetList(documentId: DocumentID);

        [Ignore]
        public string LinkFiles => Files?.ShowFiles();

        [Ignore]
        public List<Request> Requests => RequestServices.GetList(MaVanBan: DocumentID);

        [Ignore]
        public string WriterName => Writer?.FullName;

        [Ignore]
        public bool CoQuyenCapNhat => CommonServices.CheckUpdatePermission(documentId: DocumentID);
    }
}
