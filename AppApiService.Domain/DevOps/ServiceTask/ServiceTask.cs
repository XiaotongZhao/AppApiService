using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppApiService.Domain.DevOps.ServiceTask;

public class ServiceTask : EntityBase<int>
{
    public string TaskName { get; set; }
    public string TaskDescription { get; set; }
    public int StepNo { get; set; }
    public string Log { get; set; }
}
