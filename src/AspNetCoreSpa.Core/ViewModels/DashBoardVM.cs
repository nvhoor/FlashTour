using System.Collections.Generic;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class DashBoardVM
    {
        public ICollection<ChartDataVM> Revenues { get; set; }
        public ICollection<ChartDataVM> Tourists { get; set; }
    }
}