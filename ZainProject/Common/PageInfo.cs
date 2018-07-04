using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZainProject.Common
{
    public class PageInfo<T> where T : new()
    {
        int _pageSize = 15;
        int _pageIndex = 1;
        /// <summary>
        /// 每页记录条数
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value < 1 ? 1 : value; }
        }
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get { return (TotalCount + PageSize - 1) / PageSize; } }
        /// <summary>
        /// 查询结果集
        /// </summary>
        public List<T> Records { get; set; }

    }
}
