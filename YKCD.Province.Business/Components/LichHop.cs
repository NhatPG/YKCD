using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace YKCD.Province.Business.Components
{
    [Serializable]
    [TableName("tblLichtuan")]
    [PrimaryKey("iLichtuanId", AutoIncrement = true)]
    public class LichHop
    {
        /// <summary>
        /// Mã cuộc họp
        /// </summary>
        [Column(Name = "iLichtuanId")]
        public int MettingID { get; set; }

        /// <summary>
        /// Nội dung cuộc họp
        /// </summary>
        [Column(Name = "cNoidung")]
        public string MettingContent { get; set; }

        /// <summary>
        /// Mã lãnh đạo chủ trì họp
        /// </summary>
        [Column(Name = "NguoiChutri")]
        public string PresidentUserName { get; set; }

        /// <summary>
        /// Mã chuyên viên theo dõi họp
        /// </summary>
        [Column(Name = "NguoiDangky")]
        public string StaffUserName { get; set; }

        /// <summary>
        /// Thời gian họp
        /// </summary>
        [Column(Name = "dNgayHop")]
        public DateTime Time { get; set; }

        /// <summary>
        /// Trạng thái cuộc họp
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// Mã văn bản thông báo kết luận
        /// </summary>
        public long DocumentID { get; set; } = 0;

        /// <summary>
        /// Đã xóa
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreatedTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Thời gian cập nhật
        /// </summary>
        public DateTime UpdatedTime { get; set; } = DateTime.Now;
    }
}