using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formula1App.Services;
using Formula1App.Models;

namespace Formula1App.ViewModels
{
    public class StandingsViewModel : ViewModelsBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly F1ExtService extService;
    }
}
