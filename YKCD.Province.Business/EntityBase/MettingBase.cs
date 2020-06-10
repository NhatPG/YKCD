using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace YKCD.Province.Business.EntityBase
{
    /// <summary>
    /// Thông tin cuộc họp của lãnh đạo UBND tỉnh
    /// </summary>
    [Serializable]
    [TableName("Mettings")]
    [PrimaryKey("MettingID", AutoIncrement = false)]
    public class MettingBase
    {
        /// <summary>
        /// Mã cuộc họp
        /// </summary>
        public int MettingID { get; set; }

        /// <summary>
        /// Nội dung cuộc họp
        /// </summary>
        public string MettingContent { get; set; }

        /// <summary>
        /// Mã lãnh đạo chủ trì họp
        /// </summary>
        public int PresidentID { get; set; }

        /// <summary>
        /// Mã chuyên viên theo dõi họp
        /// </summary>
        public int StaffID { get; set; }

        /// <summary>
        /// Thời gian họp
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Trạng thái cuộc họp
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Mã văn bản thông báo kết luận
        /// </summary>
        public long DocumentID { get; set; }

        /// <summary>
        /// Đã xóa
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Thời gian cập nhật
        /// </summary>
        public DateTime UpdatedTime { get; set; }
    }
}