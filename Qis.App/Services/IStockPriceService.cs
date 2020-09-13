using Qis.App.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qis.App.Services
{
    public interface IStockPriceService
    {
        Task<IEnumerable<Stock>> GetStockPricessByDateAsync(DateTime date);
    }
}