using Formula1App.Services;
using Formula1App.Models;
using Formula1App.ModelsExt
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1App.ViewModels
{
    public class DriverStandingsViewModel : ViewModelsBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly F1IntService intService;
    }
}
