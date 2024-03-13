using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditScore.Service.Contracts
{
    public interface IAgeCapScore
    {
        Task<int> GetAgeCapScores(int ageInYears);
    }
}
