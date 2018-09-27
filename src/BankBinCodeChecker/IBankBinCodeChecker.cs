using System.Threading.Tasks;
using BankBinCodeChecker.Models;

namespace BankBinCodeChecker
{
    public interface IBankBinCodeChecker
    {
        Task<BankBinCodeResult> CheckBankBinCodeAsync(int bankBınCode);
        BankBinCodeResult CheckBankBinCode(int bankBınCode);
    }
}