using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using umbraco.cms.businesslogic.web;

namespace Imuless.Extensions
{
    public static class Extensions
    {
        public static bool DetectIsJson(this string input)
        {
            input = input.Trim();
            return input.StartsWith("{") && input.EndsWith("}")
                   || input.StartsWith("[") && input.EndsWith("]");
        }

        public static IEnumerable<string> GetDomains(this IContent content)
        {
            var domainList = new List<string>();
            var domains = Domain.GetDomainsById(content.Ancestors().Where(x => x.Level == 2).FirstOrDefault().Id);

            foreach (var domain in domains)
            {
                domainList.Add(domain.Name);
            }

            return domainList;
        }
    }
}