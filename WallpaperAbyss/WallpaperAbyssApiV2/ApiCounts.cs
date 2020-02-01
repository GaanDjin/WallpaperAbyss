using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WallpaperAbyssApiV2
{
    /// <summary>
    ///  the counts associated with the usage of an API key.
    /// </summary>
    [DataContract]
    public class ApiCounts
    {
        /// <summary>
        /// Number of queries run this month.
        /// </summary>
        [DataMember(Name = "month_count")]
        public int Count { get; set; }

        /// <summary>
        /// The price for the current month.
        /// </summary>
        [DataMember(Name = "month_price")]
        public float Price { get; set; }

        /// <summary>
        /// Number of queries run the last month.
        /// </summary>
        [DataMember(Name = "last_month_count")]
        public int LastMonthCount { get; set; }

        /// <summary>
        /// The price for the last month.
        /// </summary>
        [DataMember(Name = "last_month_price")]
        public float LastMonthPrice;
    }
}
