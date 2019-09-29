
using System;
using System.Collections.Generic;

namespace SampleAPI.DataAcccess.Utils
{
    public static class QueryStatementHelper
    {
        /// <summary>
        /// Crate an after WHERE statement 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="columnNameToCompare"></param>
        /// <param name="useWildCards"></param>
        /// <returns></returns>
        public static string ToOrLikeComparison(this List<string> items, string columnNameToCompare, bool useWildCards = true)
        {
            var query = string.Empty;
            var wildcard = useWildCards ? "%" : string.Empty;
            var orComparison = string.Empty;

            items.ForEach(x =>
            {
                if (string.IsNullOrEmpty(orComparison))
                    orComparison = string.IsNullOrEmpty(query) ? string.Empty : " OR ";

                query += $" {orComparison} {columnNameToCompare} LIKE '{wildcard}{x.Trim()}{wildcard}'";
            });

            return query;
        }

        /// <summary>
        /// Include uncommited as islation level and remove unnecessary blank spaces
        /// </summary>
        /// <param name="query"></param>
        public static string AddUncommitedStatement(this string query)
        {
            var result =
                ($@"
                SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                {query}
                COMMIT;")
                .Trim()
                .Replace(Environment.NewLine, string.Empty);

            return System.Text.RegularExpressions.Regex.Replace(result, @"\s+", " ");
        }
    }
}