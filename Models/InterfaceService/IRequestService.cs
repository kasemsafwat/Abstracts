using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracts.Models.InterfaceService
{
    public interface IRequestService
    {
        IEnumerable<Refactored.Requests> Read();
        int Create(Requests requests);
        void Update(Requests requests);

        IEnumerable<Refactored.RequestDetails> ReadDetails(long RequestID);
        long CreateDetails(RequestDetails requestDetails,Requests requests);
        void UpdateDetails(RequestDetails requestDetails);
    }
}
