using System;
using PetaPoco;
using System.Collections.Generic;
using YKCD.Province.Business.EntityBase;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Business.Entities
{
    [Serializable]
    public class Document : DocumentBase
    {
        [Ignore]
        public User Signer => UserServices.GetById(SignerID);

        [Ignore]
        public User Writer => UserServices.GetById(WriterID);

        [Ignore]
        public List<File> Files => FileServices.GetList(documentId: DocumentID);


        [Ignore]
        public string LinkFiles => Files.ShowFiles();

        [Ignore]
        public List<Request> Requests => RequestServices.GetList(MaVanBan: DocumentID);

        [Ignore]
        public string WriterName => Writer?.FullName;
    }
}
