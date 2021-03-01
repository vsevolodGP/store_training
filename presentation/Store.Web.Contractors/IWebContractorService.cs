using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Web.Contractors
{
    public interface IWebContractorService
    {
        string Name { get; }

        Uri StartSession(IReadOnlyDictionary<string, string> options, Uri returnUri);

        Task<Uri> StartSessionAsync(IReadOnlyDictionary<string, string> options, Uri returnUri);
    }
}
